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

namespace CrmCustomerRequest
{
    public partial class RequestList : System.Web.UI.Page
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
                IOrganizationService _orgService = Ayco.MSCRM5.SDK.Service.GetOrgService(_userOrganization, _userUrl, false, _networkCredential);
                return _orgService;
            }
        }
        #endregion

        #region Methods
        private string getStatuscode(int code)
        {
            string Result = "";
            Result = GetStatusTextOnValue(crmService, "incident", "statuscode", code, 1065);
            return Result;
        }
        private string getStatusDesccode(int code)
        {
            string Result = "";
            Result = GetOptionsSetTextOnValue(crmService, "incident", "helpdesk_statusdescription", code, 1065);
            return Result;
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
        private string GetStatusTextOnValue(IOrganizationService service, string entityName, string attributeName, int selectedValue, int LanCode)
        {
            RetrieveAttributeRequest retrieveAttributeRequest = new
            RetrieveAttributeRequest
            {

                EntityLogicalName = entityName,
                LogicalName = attributeName,
                RetrieveAsIfPublished = true
            };

            RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)service.Execute(retrieveAttributeRequest);
            StatusAttributeMetadata retrievedPicklistAttributeMetadata = (StatusAttributeMetadata)retrieveAttributeResponse.AttributeMetadata;
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
        private string GetStateTextOnValue(IOrganizationService service, string entityName, string attributeName, int selectedValue, int LanCode)
        {
            RetrieveAttributeRequest retrieveAttributeRequest = new
            RetrieveAttributeRequest
            {

                EntityLogicalName = entityName,
                LogicalName = attributeName,
                RetrieveAsIfPublished = true
            };
            RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)service.Execute(retrieveAttributeRequest);
            StateAttributeMetadata retrievedPicklistAttributeMetadata = (StateAttributeMetadata)retrieveAttributeResponse.AttributeMetadata;
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

        private void FillRequestList()
        {
            #region Create Table
            DataTable dtb = new DataTable();
            dtb.Columns.Add("ticketnumber");
            dtb.Columns.Add("title");
            dtb.Columns.Add("casetypecode");
            dtb.Columns.Add("description");
            dtb.Columns.Add("statecode");
            dtb.Columns.Add("state");
            dtb.Columns.Add("incidentid");
            dtb.Columns.Add("helpdesk_requesterconfirm");
            dtb.Columns.Add("helpdesk_statusdescription");
            #endregion

            #region Get Record From Crm
            QueryExpression query = new QueryExpression("incident");
            query.ColumnSet = new ColumnSet(true);
            OrderExpression _Order = new OrderExpression("createdon", OrderType.Descending);
            query.Orders.Add(_Order);
            FilterExpression _filterCustomer = new FilterExpression(LogicalOperator.And);
            _filterCustomer.AddCondition("customerid", ConditionOperator.Equal, new Guid("{" + Session["Personnelid"].ToString() + "}"));

            query.Criteria = new FilterExpression(LogicalOperator.And);
            query.Criteria.AddFilter(_filterCustomer);

            if (SelectType.SelectedValue.Trim() == "2" || SelectType.SelectedValue.Trim() == "3")
            {
                FilterExpression _filterState = new FilterExpression();
                if (SelectType.SelectedValue.Trim() == "2")
                {
                    _filterState.FilterOperator = LogicalOperator.And;
                    _filterState.AddCondition("statecode", ConditionOperator.Equal, 0);
                }
                else if (SelectType.SelectedValue.Trim() == "3")
                {
                    _filterState.FilterOperator = LogicalOperator.Or;
                    _filterState.AddCondition("statecode", ConditionOperator.Equal, 1);
                    _filterState.AddCondition("statecode", ConditionOperator.Equal, 2);
                }
                query.Criteria.AddFilter(_filterState);
            }
            EntityCollection retrieved = crmService.RetrieveMultiple(query);
            #endregion

            #region Fill DataTable
            if (retrieved != null && retrieved.Entities.Count > 0)
            {
                for (int i = 0; i < retrieved.Entities.Count; i++)
                {
                    DataRow dr = dtb.NewRow();
                    dr["ticketnumber"] = retrieved.Entities[i].Attributes["ticketnumber"].ToString();
                    if (retrieved.Entities[i].Contains("helpdesk_requesterconfirm") && retrieved.Entities[i].Attributes["helpdesk_requesterconfirm"] != null)
                        dr["helpdesk_requesterconfirm"] = retrieved.Entities[i].Attributes["helpdesk_requesterconfirm"].ToString();
                    else
                        dr["helpdesk_requesterconfirm"] = false;
                    if (retrieved.Entities[i].Attributes.Contains("title"))
                        dr["title"] = retrieved.Entities[i].Attributes["title"].ToString();

                    int CaseType = 0;
                    if (retrieved.Entities[i].Attributes.Contains("casetypecode"))
                    {
                        CaseType = ((OptionSetValue)retrieved.Entities[i].Attributes["casetypecode"]).Value;
                    }
                    dr["casetypecode"] = GetOptionsSetTextOnValue(crmService, "incident", "casetypecode", CaseType, 1065);
                    if (retrieved.Entities[i].Attributes.Contains("description"))
                        dr["description"] = retrieved.Entities[i].Attributes["description"].ToString();
                    if (((OptionSetValue)retrieved.Entities[i].Attributes["statecode"]).Value == 0)
                    {
                        dr["statecode"] = getStatuscode(((OptionSetValue)retrieved.Entities[i].Attributes["statuscode"]).Value);
                        dr["state"] = 0;
                    }
                    else
                    {
                        dr["statecode"] = GetStateTextOnValue(crmService, "incident", "statecode", ((OptionSetValue)retrieved.Entities[i].Attributes["statecode"]).Value, 1065);
                        dr["state"] = ((OptionSetValue)retrieved.Entities[i].Attributes["statecode"]).Value;
                    }
                    dr["incidentid"] = retrieved.Entities[i].Attributes["incidentid"].ToString();
                    if (retrieved.Entities[i].Contains("helpdesk_statusdescription") && retrieved.Entities[i].Attributes["helpdesk_statusdescription"] != null)
                        dr["helpdesk_statusdescription"] = getStatusDesccode(((OptionSetValue)retrieved.Entities[i].Attributes["helpdesk_statusdescription"]).Value);
                    if (((OptionSetValue)retrieved.Entities[i].Attributes["statecode"]).Value == 1)
                        dr["helpdesk_statusdescription"] = getStatusDesccode(3);
                    dtb.Rows.Add(dr);
                }
            }
            #endregion

            RequestView.DataSource = dtb;
            RequestView.DataBind();
        }
        public string GetIcon(string stateCode)
        {
            string src = "";
            if (stateCode == "0")
                src = "images/shield_yellow.png";
            else if (stateCode == "1")
                src = "images/shield_green.png";
            else if (stateCode == "2")
                src = "images/shield_red.png";
            return src;
        }
        private void UpdateConfirm(string incidentid)
        {
            SetStateRequest _request = new SetStateRequest();
            _request.EntityMoniker = new EntityReference("incident", new Guid(incidentid));
            _request.State = new OptionSetValue(0);
            _request.Status = new OptionSetValue(522040002);
            SetStateResponse _SetStateResponse = (SetStateResponse)crmService.Execute(_request);

            Entity _incident = crmService.Retrieve("incident", new Guid(incidentid), new ColumnSet("incidentid", "helpdesk_statusdescription"));
            _incident["helpdesk_statusdescription"] = new OptionSetValue(11);
            crmService.Update(_incident);
            FillRequestList();
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
                Entity _Contact = new Entity("contact");
                _Contact = crmService.Retrieve("contact", new Guid(Session["Personnelid"].ToString()), new ColumnSet(true));
                //-----------------------------------------------------
                LabelPersonnel.Text = _Contact["fullname"].ToString();
                //-----------------------------------------------------
                FillRequestList();
            }
        }

        protected void LinkAddRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddRequest.aspx");
        }
        protected void LinkSignOut_Click(object sender, EventArgs e)
        {
            Session["Personnelid"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void RequestView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Confirm")
            {
                UpdateConfirm(e.CommandArgument.ToString());
            }
            if (e.CommandName == "Cancel")
            {
                Entity _req = new Entity("incident");
                _req["incidentid"] = new Guid(e.CommandArgument.ToString());
                _req["helpdesk_statusdescription"] = null;
                crmService.Update(_req);

                    SetStateRequest _request = new SetStateRequest();
                    _request.EntityMoniker = new EntityReference("incident", new Guid(e.CommandArgument.ToString()));
                    _request.State = new OptionSetValue(2);
                    _request.Status = new OptionSetValue(6);
                    SetStateResponse _SetStateResponse = (SetStateResponse)crmService.Execute(_request);
                    FillRequestList();

                
            }
        }
        protected void RequestView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Button myButton = e.Row.FindControl("Confirm") as Button;
            Button myButton1 = e.Row.FindControl("Cancel") as Button;
            if (myButton != null)
            {
                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "state")) == 1)
                {
                    myButton.Enabled = true;
                    myButton.Text = "بازبینی مجدد";
                }
                else
                {
                    myButton.Enabled = false;
                    myButton.Text = "بازبینی مجدد";
                }
            }
            if (myButton1 != null)
            {
                if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "state")) != 0)
                {
                    myButton1.Enabled = false;
                    myButton1.Text = "لغو درخواست";
                }
                else
                {
                    QueryExpression query = new QueryExpression { EntityName = "activitypointer", ColumnSet = new ColumnSet(new string[] { "activitytypecode" }) };
                    query.Criteria.AddCondition("regardingobjectid", ConditionOperator.Equal, new Guid(DataBinder.Eval(e.Row.DataItem, "incidentid").ToString()));
                    query.Criteria.AddCondition("statecode", ConditionOperator.NotEqual, 1);  //ignore completed
                    query.Criteria.AddCondition("statecode", ConditionOperator.NotEqual, 2);  //ignore completed
                    EntityCollection collection = crmService.RetrieveMultiple(query);
                    if (collection.Entities.Count > 0)
                    {
                        myButton1.Enabled = false;
                        myButton1.Text = "لغو درخواست";
                    }
                    else
                    {
                        myButton1.Enabled = true;
                        myButton1.Text = "لغو درخواست";
                    }
                }
            }
        }
        protected void RequestView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RequestView.PageIndex = e.NewPageIndex;
            FillRequestList();
        }
        #endregion

        protected void RefreshBtn_Click(object sender, ImageClickEventArgs e)
        {
            FillRequestList();
        }

        protected void SelectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillRequestList();
        }

        protected void RequestView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
    }
}