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

namespace Raybod.MSCRM.KandooWebSite
{
    public partial class ClassDetail : System.Web.UI.Page
    {
        #region properties
        IOrganizationService crmService = null;
        public string classId
        {
            get
            {
                return Request.QueryString["id"];
            }
        }

        #endregion

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

                if (classId != null)
                {
                    //محل پر کردن آیتم ها جزئیات کلاس
                    Entity _Class = crmService.Retrieve("new_class", new Guid(classId), new ColumnSet(true));
                    txtClassName.Text = _Class["new_name"].ToString();
                    txtCourse.Text = ((EntityReference)_Class["new_relatedcourseid"]).Name;
                    txtLocation.Text = ((EntityReference)_Class["new_holdinglocationid"]).Name;
                    txtLab.Text = ((EntityReference)_Class["new_relatedlabid"]).Name;
                    txtClassType.Text = GetOptionsSetTextOnValue(crmService, "new_class", "new_typeofclass", ((OptionSetValue)_Class["new_typeofclass"]).Value, 1065);
                    txtClassStatus.Text = GetOptionsSetTextOnValue(crmService, "new_class", "new_classstatus", ((OptionSetValue)_Class["new_classstatus"]).Value, 1065);
                    txtStartdate.Text = gregorianToShamsi(((DateTime)_Class["new_startdate"]).Date);
                    txtStarttime.Text = GetOptionsSetTextOnValue(crmService, "new_class", "new_starttime", ((OptionSetValue)_Class["new_starttime"]).Value, 1065) + ":" +
                                          GetOptionsSetTextOnValue(crmService, "new_class", "new_minutesstart", ((OptionSetValue)_Class["new_minutesstart"]).Value, 1065);
                    txtEndtime.Text = GetOptionsSetTextOnValue(crmService, "new_class", "new_endtime", ((OptionSetValue)_Class["new_endtime"]).Value, 1065) + ":" +
                                          GetOptionsSetTextOnValue(crmService, "new_class", "new_minutesend", ((OptionSetValue)_Class["new_minutesend"]).Value, 1065);
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void LinkClassList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassList.aspx");
        }
        protected void LinkSignOut_Click(object sender, EventArgs e)
        {
            Session["Personnelid"] = null;
            HttpResponse.RemoveOutputCacheItem("/ClassDetail.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassList.aspx");
            Response.Redirect("Login.aspx");
        }
        protected void btnteachrqst_Click(object sender, EventArgs e)
        {
            if (classId != null)
                Response.Redirect("TuitionList.aspx?ClassId=" + classId);
            else
                Response.Redirect("Login.aspx");
        }
        protected void btnAttendancerqst_Click(object sender, EventArgs e)
        {
            if (classId != null)
                Response.Redirect("MeetingsList.aspx?ClassId=" + classId);
            else
                Response.Redirect("Login.aspx");

        }
        protected void btnTechrqst_Click(object sender, EventArgs e)
        {
            if (classId != null)
                Response.Redirect("TechnicalList.aspx?ClassId=" + classId);
            else
                Response.Redirect("Login.aspx");
        }

        #endregion


    }
}