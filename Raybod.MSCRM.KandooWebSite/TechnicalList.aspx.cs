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
    public partial class TechnicalList : System.Web.UI.Page
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
            string convertedDate = string.Format("{0}/{1}/{2}", pcYear, pcMonth, pcDay);
            return convertedDate;
        }

        private void FillTechnicalList()
        {
            #region Create Table
            DataTable dtb = new DataTable();
            dtb.Columns.Add("name");
            dtb.Columns.Add("requestdate");
            dtb.Columns.Add("owner");
            dtb.Columns.Add("done1");
            dtb.Columns.Add("requestdescription");
            dtb.Columns.Add("donedescription");
            dtb.Columns.Add("classstechnicalrequestid");
            #endregion

            #region Get Record From Crm

            QueryExpression query = new QueryExpression("new_classstechnicalrequest");
            query.ColumnSet = new ColumnSet(new string[] { "new_name", "createdon", "ownerid", "new_done1",
                                                           "new_requestdescription", "new_donedescription", "new_classstechnicalrequestid" });
            query.Criteria.AddCondition("new_relatedclassid", ConditionOperator.Equal, new Guid(ClassId));
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
                    if (retrieved.Entities[i].Contains("new_name"))
                        dr["name"] = retrieved.Entities[i].Attributes["new_name"].ToString();
                    if (retrieved.Entities[i].Contains("createdon"))
                        dr["requestdate"] = gregorianToShamsi(((DateTime)retrieved.Entities[i].Attributes["createdon"]).Date);
                    if (retrieved.Entities[i].Contains("ownerid"))
                        dr["owner"] = ((EntityReference)retrieved.Entities[i]["ownerid"]).Name;
                    if (retrieved.Entities[i].Contains("new_done1"))
                        dr["done1"] = GetOptionsSetTextOnValue(crmService, "new_classstechnicalrequest", "new_done1", ((OptionSetValue)retrieved.Entities[i].Attributes["new_done1"]).Value, 1065);
                    if (retrieved.Entities[i].Contains("new_requestdescription"))
                        dr["requestdescription"] = retrieved.Entities[i].Attributes["new_requestdescription"].ToString();
                    if (retrieved.Entities[i].Contains("new_donedescription"))
                        dr["donedescription"] = retrieved.Entities[i].Attributes["new_donedescription"].ToString();
                    if (retrieved.Entities[i].Contains("new_classstechnicalrequestid"))
                        dr["classstechnicalrequestid"] = retrieved.Entities[i].Attributes["new_classstechnicalrequestid"].ToString();

                    dtb.Rows.Add(dr);
                }
            }
            #endregion

            TechnicalListView.DataSource = dtb;
            TechnicalListView.DataBind();
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
                FillTechnicalList();
            }
        }
        protected void LinkSignOut_Click(object sender, EventArgs e)
        {
            Session["Personnelid"] = null;
            HttpResponse.RemoveOutputCacheItem("/TechnicalList.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassDetail.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassList.aspx");
            Response.Redirect("Login.aspx");
        }
        protected void RefreshBtn_Click(object sender, ImageClickEventArgs e)
        {
            FillTechnicalList();
        }
        protected void linkRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassDetail.aspx?id=" + ClassId.ToString());
        }

        protected void TechnicalListView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click last column for selecting this row.";
            }
        }
        protected void TechnicalListView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TechnicalListView.PageIndex = e.NewPageIndex;
            FillTechnicalList();
        }
        protected void TechnicalListView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
        protected void LinkAddRequest_Click(object sender, EventArgs e)
        {
            if (ClassId != null)
                Response.Redirect("TechnicalRequest.aspx?ClassId=" + ClassId);
            else
                Response.Redirect("Login.aspx");
        }
        #endregion
    }
}