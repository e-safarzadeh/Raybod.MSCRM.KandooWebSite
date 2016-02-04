using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using System.Net;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Globalization;
using WebControlUI;

namespace Raybod.MSCRM.KandooWebSite
{
    public partial class TuitionRequest : System.Web.UI.Page
    {
        IOrganizationService crmService = null;

        #region Method
        private void getOrgService()
        {
            string _userName = ConfigurationManager.AppSettings["username"].ToString();
            string _userPass = ConfigurationManager.AppSettings["Password"].ToString();
            string _userDomain = ConfigurationManager.AppSettings["Domain"].ToString();
            string _userOrganization = ConfigurationManager.AppSettings["Organization"].ToString();
            string _userUrl = ConfigurationManager.AppSettings["Url"].ToString();
            NetworkCredential _networkCredential = new NetworkCredential(_userName, _userPass, _userDomain);
            IOrganizationService _orgService = Raybod.MSCRM5.SDK.Service.GetOrgService(_userOrganization, _userUrl, false, _networkCredential);
            crmService = _orgService;
        }

        private string GetOptionsSetTextOnValue(IOrganizationService service, string entityName, string attributeName, int selectedValue, int LanCode)
        {
            RetrieveAttributeRequest retrieveAttributeRequest = new
            RetrieveAttributeRequest
            {

                EntityLogicalName = entityName,
                LogicalName = attributeName,
                RetrieveAsIfPublished = true
            };
            RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)service.Execute(retrieveAttributeRequest);
            PicklistAttributeMetadata retrievedPicklistAttributeMetadata = (PicklistAttributeMetadata)retrieveAttributeResponse.AttributeMetadata;
            OptionMetadata[] optionList = retrievedPicklistAttributeMetadata.OptionSet.Options.ToArray();

            string selectedOptionLabel = string.Empty;
            foreach (OptionMetadata oMD in optionList)
            {
                if (oMD.Value == selectedValue)
                {
                    foreach (var item in oMD.Label.LocalizedLabels)
                    {
                        if (item.LanguageCode == LanCode)
                        {
                            selectedOptionLabel = item.Label;
                        }
                    }
                }
            }
            return selectedOptionLabel;
        }

        public string gregorianToShamsi(DateTime date)
        {
            DateTime gregorian = date;
            PersianCalendar pc = new PersianCalendar();
            int pcYear = pc.GetYear(gregorian);
            int pcMonth = pc.GetMonth(gregorian);
            int pcDay = pc.GetDayOfMonth(gregorian);
            string convertedDate;

            if (pcMonth == 1 || pcMonth == 2 || pcMonth == 3 ||
                pcMonth == 4 || pcMonth == 5 || pcMonth == 6)
            {
                if (pcDay == 31)
                    convertedDate = string.Format("{0}/{1}/{2}", pcYear, pcMonth + 1, 1);
                else
                    convertedDate = string.Format("{0}/{1}/{2}", pcYear, pcMonth, pcDay + 1);

            }
            else
            {
                if (pcDay == 30)
                    convertedDate = string.Format("{0}/{1}/{2}", pcYear, pcMonth + 1, 1);
                else
                    convertedDate = string.Format("{0}/{1}/{2}", pcYear, pcMonth, pcDay + 1);
            }
            return convertedDate;
        }

        public DateTime ShamsiTogregorian(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            GregorianCalendar gcalendar = new GregorianCalendar();
            DateTime eDate = pc.ToDateTime(
                   gcalendar.GetYear(date),
                   gcalendar.GetMonth(date),
                   gcalendar.GetDayOfMonth(date),
                   gcalendar.GetHour(date),
                   gcalendar.GetMinute(date),
                   gcalendar.GetSecond(date), 0);
            return eDate;
        }

        public string ClassId
        {
            get
            {
                return Request.QueryString["ClassId"];
            }
        }

        #endregion

        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            getOrgService();
            if (Session["Personnelid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                getOrgService();

                Entity _User = new Entity("systemuser");
                _User = crmService.Retrieve("systemuser", new Guid(Session["Personnelid"].ToString()), new ColumnSet(true));
                LabelPersonnel.Text = _User["fullname"].ToString();
                //-----------------------------------------------------

                if (ClassId != null)
                {
                    Entity _class = crmService.Retrieve("new_class", new Guid(ClassId), new ColumnSet(new string[] { "new_classid", "new_name", "new_startdate", "new_terminationdate" }));
                    txtClass.Text = _class["new_name"].ToString();
                    if (_class.Contains("new_startdate"))
                        DatePicker1.DatePersian = gregorianToShamsi(((DateTime)_class["new_startdate"]).Date);
                    if (_class.Contains("new_terminationdate"))
                        DatePicker2.DatePersian = gregorianToShamsi(((DateTime)_class["new_terminationdate"]).Date);
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        protected void LinkSignOut_Click(object sender, EventArgs e)
        {
            Session["Personnelid"] = null;
            HttpResponse.RemoveOutputCacheItem("/ClassDetail.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassList.aspx");
            Response.Redirect("Login.aspx");
        }
        protected void linkRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassDetail.aspx?id=" + ClassId.ToString());
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //--------------Add TeacherPaymentRequest------------------------------
            #region Add TeacherPaymentRequest
            if (ClassId != null)
            {
                Entity _class = crmService.Retrieve("new_class", new Guid(ClassId), new ColumnSet(new string[] { "new_classid", "new_name", "new_typeofclass", "new_classcode",
                                                                                                                 "new_teacherid", "new_numberofhours_ds", "new_relatedaccountid",
                                                                                                                 "new_numberofhourspersession","new_numberofteachercancel", "new_teachersalary1"}));
                Entity _teacherrqst = new Entity("new_teacherpaymentrequest");
                _teacherrqst["new_name"] = "درخواست حق التدریس: " + "new_name";
                _teacherrqst["new_startdate"] = DatePicker1.Date;
                _teacherrqst["new_enddate"] = DatePicker2.Date;
                _teacherrqst["new_descriptionteaching"] = txtDescription.Text;
                _teacherrqst["new_teacherid"] = new EntityReference("systemuser", new Guid(Session["Personnelid"].ToString()));
                _teacherrqst["new_classid"] = new EntityReference("new_class", new Guid(ClassId));
                if (_class.Contains("new_typeofclass"))
                    _teacherrqst["new_classtype"] = _class["new_typeofclass"];
                if (_class.Contains("new_classcode"))
                    _teacherrqst["new_relatedclasscode"] = _class["new_classcode"];

                _teacherrqst["new_requestdate"] = DateTime.Now;
                if (_class.Contains("new_numberofhours_ds"))
                    _teacherrqst["new_numberofhours"] = _class["new_numberofhours_ds"];
                if (_class.Contains("new_relatedaccountid"))
                    _teacherrqst["new_relatedaccount"] = _class["new_relatedaccountid"];
                if (_class.Contains("new_numberofhourspersession"))
                    _teacherrqst["new_numberofhourspersession"] = _class["new_numberofhourspersession"];
                if (_class.Contains("new_numberofteachercancel"))
                    _teacherrqst["new_totalcancelhours"] = Convert.ToDecimal(_class["new_numberofteachercancel"]);
                _teacherrqst["transactioncurrencyid"] = new EntityReference("transactioncurrency", new Guid("1748FA23-0F7B-E011-A351-00155D0A3A19"));
                _teacherrqst["new_teachersalary"] = _class["new_teachersalary1"];
                Guid teacherrqstId = crmService.Create(_teacherrqst);
                Response.Redirect("RequestConfirmTeacher.aspx?TeacherrqstId=" + teacherrqstId.ToString());
            }
            #endregion
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        #endregion
    }
}