using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Xrm.Sdk;
using System.Net;
using Microsoft.Xrm.Sdk.Query;
using System.Data;
using System.Configuration;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Globalization;

namespace Raybod.MSCRM.KandooWebSite
{
    public partial class TuitionList : System.Web.UI.Page
    {
        #region Properties
        public IOrganizationService crmService
        {
            get
            {
                string _userName = ConfigurationManager.AppSettings["username"].ToString();
                string _userPass = ConfigurationManager.AppSettings["Password"].ToString();
                string _userDomain = ConfigurationManager.AppSettings["Domain"].ToString();
                string _userOrganization = ConfigurationManager.AppSettings["Organization"].ToString();
                string _userUrl = ConfigurationManager.AppSettings["Url"].ToString();
                NetworkCredential _networkCredential = new NetworkCredential(_userName, _userPass, _userDomain);
                IOrganizationService _orgService = Raybod.MSCRM5.SDK.Service.GetOrgService(_userOrganization, _userUrl, false, _networkCredential);
                return _orgService;
            }
        }

        public string ClassId
        {
            get
            {
                return Request.QueryString["ClassId"];
            }
        }
        #endregion

        #region Methods

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

        private void FillTuitionList()
        {
            #region Create Table
            DataTable dtb = new DataTable();
            dtb.Columns.Add("requestdate");
            dtb.Columns.Add("code");
            dtb.Columns.Add("paymentstatus");
            dtb.Columns.Add("totalteachinghours_ds");
            dtb.Columns.Add("teachersalary");
            dtb.Columns.Add("deductions");
            dtb.Columns.Add("theamountpaid");
            dtb.Columns.Add("typeofpayment");
            dtb.Columns.Add("dateofpayment");
            dtb.Columns.Add("teacherpaymentrequestid");
            #endregion

            #region Get Record From Crm
            QueryExpression query = new QueryExpression("new_teacherpaymentrequest");
            query.ColumnSet = new ColumnSet(new string[] { "new_teacherpaymentrequestid", "new_requestdate", "new_code", "new_paymentstatus",
                                                           "new_totalteachinghours_ds", "new_teachersalary", "new_deductions", "new_theamountpaid",
                                                            "new_typeofpayment","new_dateofpayment","createdon" });
            query.Criteria.AddCondition("new_teacherid", ConditionOperator.Equal, new Guid("{" + Session["Personnelid"].ToString() + "}"));
            query.Criteria.AddCondition("new_classid", ConditionOperator.Equal, new Guid(ClassId));
            OrderExpression _Order = new OrderExpression("createdon", OrderType.Ascending);
            query.Orders.Add(_Order);

            EntityCollection retrieved = crmService.RetrieveMultiple(query);
            #endregion

            #region Fill DataTable
            if (retrieved != null && retrieved.Entities.Count > 0)
            {
                for (int i = 0; i < retrieved.Entities.Count; i++)
                {
                    DataRow dr = dtb.NewRow();

                    if (retrieved.Entities[i].Contains("new_requestdate") && retrieved.Entities[i]["new_requestdate"] != null)
                        dr["requestdate"] = gregorianToShamsi(((DateTime)retrieved.Entities[i].Attributes["new_requestdate"]).Date);
                    if (retrieved.Entities[i].Contains("new_code") && retrieved.Entities[i]["new_code"] != null)
                        dr["code"] = (string)retrieved.Entities[i]["new_code"];
                    if (retrieved.Entities[i].Contains("new_paymentstatus") && retrieved.Entities[i]["new_paymentstatus"] != null)
                        dr["paymentstatus"] = GetOptionsSetTextOnValue(crmService, "new_teacherpaymentrequest", "new_paymentstatus", ((OptionSetValue)retrieved.Entities[i]["new_paymentstatus"]).Value, 1065);
                    if (retrieved.Entities[i].Contains("new_totalteachinghours_ds") && retrieved.Entities[i]["new_totalteachinghours_ds"] != null)
                        dr["totalteachinghours_ds"] = ((decimal)retrieved.Entities[i]["new_totalteachinghours_ds"]).ToString("0.#####");
                    if (retrieved.Entities[i].Contains("new_teachersalary") && retrieved.Entities[i]["new_teachersalary"] != null)
                        dr["teachersalary"] = (((Money)retrieved.Entities[i]["new_teachersalary"]).Value).ToString("0.#####");
                    if (retrieved.Entities[i].Contains("new_deductions") && retrieved.Entities[i]["new_deductions"] != null)
                        dr["deductions"] = ((decimal)retrieved.Entities[i]["new_deductions"]).ToString("0.#####");
                    if (retrieved.Entities[i].Contains("new_theamountpaid") && retrieved.Entities[i]["new_theamountpaid"] != null)
                        dr["theamountpaid"] = (((Money)retrieved.Entities[i]["new_theamountpaid"]).Value).ToString("0.#####");
                    if (retrieved.Entities[i].Contains("new_typeofpayment") && retrieved.Entities[i]["new_typeofpayment"] != null)
                        dr["typeofpayment"] = GetOptionsSetTextOnValue(crmService, "new_teacherpaymentrequest", "new_typeofpayment", ((OptionSetValue)retrieved.Entities[i]["new_typeofpayment"]).Value, 1065);
                    if (retrieved.Entities[i].Contains("new_dateofpayment") && retrieved.Entities[i]["new_dateofpayment"] != null)
                        dr["dateofpayment"] = gregorianToShamsi(((DateTime)retrieved.Entities[i].Attributes["new_dateofpayment"]).Date);

                    dr["teacherpaymentrequestid"] = retrieved.Entities[i].Attributes["new_teacherpaymentrequestid"].ToString();
                    dtb.Rows.Add(dr);
                }
            }
            #endregion

            RequestTuitionListView.DataSource = dtb;
            RequestTuitionListView.DataBind();
        }

        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Personnelid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                Entity _User = new Entity("systemuser");
                _User = crmService.Retrieve("systemuser", new Guid(Session["Personnelid"].ToString()), new ColumnSet(true));
                LabelUser.Text = _User["fullname"].ToString();
                //پر نمودن گرید
                FillTuitionList();
            }
        }
        protected void LinkSignOut_Click(object sender, EventArgs e)
        {
            Session["Personnelid"] = null;
            HttpResponse.RemoveOutputCacheItem("/TuitionList.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassDetail.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassList.aspx");
            Response.Redirect("Login.aspx");
        }
        protected void RefreshBtn_Click(object sender, ImageClickEventArgs e)
        {
            FillTuitionList();
        }
        protected void linkRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassDetail.aspx?id=" + ClassId.ToString());
        }
        protected void RequestTuitionListView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click last column for selecting this row.";
            }

        }
        protected void RequestTuitionListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RequestTuitionListView.PageIndex = e.NewPageIndex;
            FillTuitionList();
        }
        protected void RequestTuitionListView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
        protected void LinkAddRequest_Click(object sender, EventArgs e)
        {
            if (ClassId != null)
                Response.Redirect("TuitionRequest.aspx?ClassId=" + ClassId);
            else
                Response.Redirect("Login.aspx");
        }
        #endregion


    }
}