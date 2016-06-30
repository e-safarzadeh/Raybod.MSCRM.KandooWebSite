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
	public partial class ClassList : System.Web.UI.Page
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

		private void FillClassList(int page)
		{
			#region Create Table
			DataTable dtb = new DataTable();
			dtb.Columns.Add("name");
			dtb.Columns.Add("typeofclass");
			dtb.Columns.Add("showtimes");
			dtb.Columns.Add("classstatus");
			dtb.Columns.Add("vmname");
			dtb.Columns.Add("teacherpassword");
			dtb.Columns.Add("studentpassword");
			dtb.Columns.Add("holdinglocationid");
			dtb.Columns.Add("relatedlabid");
			dtb.Columns.Add("startdate");
			dtb.Columns.Add("starttime");
			dtb.Columns.Add("endtime");
			dtb.Columns.Add("classid");
			#endregion

			#region Get Record From Crm
			QueryExpression query = new QueryExpression("new_class");
			query.ColumnSet = new ColumnSet(new string[] { "new_name", "new_relatedcourseid", "new_holdinglocationid", "new_relatedlabid",
														   "new_startdate", "new_starttime", "new_endtime", "new_classid", "createdon", "new_minutesstart" , "new_minutesend",
															"new_typeofclass", "new_showtimes", "new_classstatus","new_vmname","new_teacherpassword","new_studentpassword",});
			query.Criteria.AddCondition("new_teacherid", ConditionOperator.Equal, new Guid("{" + Session["Personnelid"].ToString() + "}"));
			query.Criteria.AddCondition("new_classstatus", ConditionOperator.NotEqual, 4);
			query.Criteria.AddCondition("new_classstatus", ConditionOperator.NotEqual, 5);

			OrderExpression _Order = new OrderExpression("new_startdate", OrderType.Ascending);
			query.Orders.Add(_Order);

			query.PageInfo = new PagingInfo();
			query.PageInfo.ReturnTotalRecordCount = true;
			query.PageInfo.Count = 10;
			query.PageInfo.PageNumber = page;

			query.PageInfo.PagingCookie = null;

			EntityCollection retrieved = crmService.RetrieveMultiple(query);


			#endregion

			#region Fill DataTable
			if (retrieved != null && retrieved.Entities.Count > 0)
			{
				for (int i = 0; i < retrieved.Entities.Count; i++)
				{
					DataRow dr = dtb.NewRow();
					dr["name"] = retrieved.Entities[i].Attributes["new_name"].ToString();
					if (retrieved.Entities[i].Contains("new_typeofclass") && retrieved.Entities[i]["new_typeofclass"] != null)
						dr["typeofclass"] = GetOptionsSetTextOnValue(crmService, "new_class", "new_typeofclass", ((OptionSetValue)retrieved.Entities[i].Attributes["new_typeofclass"]).Value, 1065);
					if (retrieved.Entities[i].Contains("new_showtimes") && retrieved.Entities[i]["new_showtimes"] != null)
						dr["showtimes"] = GetOptionsSetTextOnValue(crmService, "new_class", "new_showtimes", ((OptionSetValue)retrieved.Entities[i].Attributes["new_showtimes"]).Value, 1065);
					if (retrieved.Entities[i].Contains("new_classstatus") && retrieved.Entities[i]["new_classstatus"] != null)
						dr["classstatus"] = GetOptionsSetTextOnValue(crmService, "new_class", "new_classstatus", ((OptionSetValue)retrieved.Entities[i].Attributes["new_classstatus"]).Value, 1065);
					if (retrieved.Entities[i].Contains("new_vmname") && retrieved.Entities[i]["new_vmname"] != null)
						dr["vmname"] = (string)retrieved.Entities[i].Attributes["new_vmname"];
					if (retrieved.Entities[i].Contains("new_teacherpassword") && retrieved.Entities[i]["new_teacherpassword"] != null)
						dr["teacherpassword"] = (string)retrieved.Entities[i].Attributes["new_teacherpassword"];
					if (retrieved.Entities[i].Contains("new_studentpassword") && retrieved.Entities[i]["new_studentpassword"] != null)
						dr["studentpassword"] = (string)retrieved.Entities[i].Attributes["new_studentpassword"];
					if (retrieved.Entities[i].Contains("new_holdinglocationid"))
						dr["holdinglocationid"] = ((EntityReference)retrieved.Entities[i].Attributes["new_holdinglocationid"]).Name;
					if (retrieved.Entities[i].Contains("new_relatedlabid"))
						dr["relatedlabid"] = ((EntityReference)retrieved.Entities[i].Attributes["new_relatedlabid"]).Name;
					if (retrieved.Entities[i].Contains("new_startdate"))
						dr["startdate"] = gregorianToShamsi((DateTime)retrieved.Entities[i].Attributes["new_startdate"]);
					if (retrieved.Entities[i].Contains("new_starttime"))
						dr["starttime"] = GetOptionsSetTextOnValue(crmService, "new_class", "new_starttime", ((OptionSetValue)retrieved.Entities[i].Attributes["new_starttime"]).Value, 1065)
									  + ":" + GetOptionsSetTextOnValue(crmService, "new_class", "new_minutesstart", ((OptionSetValue)retrieved.Entities[i].Attributes["new_minutesstart"]).Value, 1065);
					if (retrieved.Entities[i].Contains("new_endtime"))
						dr["endtime"] = GetOptionsSetTextOnValue(crmService, "new_class", "new_endtime", ((OptionSetValue)retrieved.Entities[i].Attributes["new_endtime"]).Value, 1065)
									  + ":" + GetOptionsSetTextOnValue(crmService, "new_class", "new_minutesend", ((OptionSetValue)retrieved.Entities[i].Attributes["new_minutesend"]).Value, 1065);
					if (retrieved.Entities[i].Contains("new_classid"))
						dr["classid"] = retrieved.Entities[i].Attributes["new_classid"].ToString();

					dtb.Rows.Add(dr);
				}
			}

			#endregion

			if (retrieved.MoreRecords)
			{
				query.PageInfo.PagingCookie = retrieved.PagingCookie;
				RequestClassView.VirtualItemCount = retrieved.TotalRecordCount;
			}


			RequestClassView.PageSize = 10;
			RequestClassView.DataSource = dtb;
			RequestClassView.DataBind();
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
				_User = crmService.Retrieve("systemuser", new Guid(Session["Personnelid"].ToString()), new ColumnSet(new string[] { "systemuserid", "fullname" }));
				LabelUser.Text = _User["fullname"].ToString();
				FillClassList(1);
			}
		}
		protected void LinkSignOut_Click(object sender, EventArgs e)
		{
			Session["Personnelid"] = null;
			Response.Redirect("Login.aspx");
		}
		protected void RefreshBtn_Click(object sender, ImageClickEventArgs e)
		{
			FillClassList(1);
		}
		protected void RequestClassView_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Open")
			{
				Response.Redirect("ClassDetail.aspx?id=" + e.CommandArgument.ToString());
			}
		}
		protected void RequestClassView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
				e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
				e.Row.ToolTip = "Click last column for selecting this row.";
			}
		}
		protected void RequestClassView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			RequestClassView.PageIndex = e.NewPageIndex;
			FillClassList(e.NewPageIndex + 1);
		}
		protected void RequestClassView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{

		}
		protected void RequestClassView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
		{
			//string Id = this.RequestClassView.Rows[e.NewSelectedIndex].Cells[9].Text;
			//Response.Redirect("ClassDetail.aspx?id=" + Id);
		}

		protected void LinkChangeUser_Click(object sender, EventArgs e)
		{
			Response.Redirect("ChangeUser.aspx");
		}

		protected void LinkChangePass_Click(object sender, EventArgs e)
		{
			Response.Redirect("ChangePass.aspx");
		}
		#endregion

	}
}