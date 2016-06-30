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
	public partial class MeetingsList : System.Web.UI.Page
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

		private void FillMeetingList(int page)
		{
			#region Create Table
			DataTable dtb = new DataTable();
			dtb.Columns.Add("number");
			dtb.Columns.Add("name");
			dtb.Columns.Add("dateholding");
			dtb.Columns.Add("starttime");
			dtb.Columns.Add("endtime");
			dtb.Columns.Add("status");
			dtb.Columns.Add("attendancelistid");
			#endregion

			#region Get Record From Crm

			QueryExpression query = new QueryExpression("new_attendancelist");
			query.ColumnSet = new ColumnSet(new string[] { "new_name", "new_meetingnumber", "new_dateholding", "new_starttime",
														   "new_endtime", "new_status", "new_attendancelistid", "createdon", "new_starttime1", "new_minutesstart", "new_endtime1", "new_minutesend"});
			query.Criteria.AddCondition("new_relatedteacher", ConditionOperator.Equal, new Guid("{" + Session["Personnelid"].ToString() + "}"));
			query.Criteria.AddCondition("new_relatedclassid", ConditionOperator.Equal, new Guid(ClassId));
			OrderExpression _Order = new OrderExpression("new_dateholding", OrderType.Ascending);
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

				TimeSpan time = new TimeSpan(3, 30, 0);
				for (int i = 0; i < retrieved.Entities.Count; i++)
				{
					DataRow dr = dtb.NewRow();

					if (retrieved.Entities[i].Contains("new_meetingnumber"))
						dr["number"] = retrieved.Entities[i].Attributes["new_meetingnumber"].ToString();
					if (retrieved.Entities[i].Contains("new_name"))
						dr["name"] = retrieved.Entities[i].Attributes["new_name"].ToString();
					if (retrieved.Entities[i].Contains("new_dateholding"))
						dr["dateholding"] = gregorianToShamsi(((DateTime)retrieved.Entities[i].Attributes["new_dateholding"]).Date);
					if (retrieved.Entities[i].Contains("new_starttime1") && retrieved.Entities[i].Contains("new_minutesstart"))
						dr["starttime"] = GetOptionsSetTextOnValue(crmService, "new_attendancelist", "new_starttime1", ((OptionSetValue)retrieved.Entities[i]["new_starttime1"]).Value, 1065) + ":" +
										  GetOptionsSetTextOnValue(crmService, "new_attendancelist", "new_minutesstart", ((OptionSetValue)retrieved.Entities[i]["new_minutesstart"]).Value, 1065);
					if (retrieved.Entities[i].Contains("new_endtime1") && retrieved.Entities[i].Contains("new_minutesend"))
						dr["endtime"] = GetOptionsSetTextOnValue(crmService, "new_attendancelist", "new_endtime1", ((OptionSetValue)retrieved.Entities[i]["new_endtime1"]).Value, 1065) + ":" +
										  GetOptionsSetTextOnValue(crmService, "new_attendancelist", "new_minutesend", ((OptionSetValue)retrieved.Entities[i]["new_minutesend"]).Value, 1065);
					//if (retrieved.Entities[i].Contains("new_endtime"))
					//    dr["endtime"] = gregorianToShamsi(((DateTime)retrieved.Entities[i].Attributes["new_endtime"]).Date) + " " + String.Format("{0:t}", ((DateTime)retrieved.Entities[i].Attributes["new_endtime"]).Add(time));
					if (retrieved.Entities[i].Contains("new_status"))
						dr["status"] = GetOptionsSetTextOnValue(crmService, "new_attendancelist", "new_status", ((OptionSetValue)retrieved.Entities[i].Attributes["new_status"]).Value, 1065);
					if (retrieved.Entities[i].Contains("new_attendancelistid"))
						dr["attendancelistid"] = retrieved.Entities[i].Attributes["new_attendancelistid"].ToString();

					dtb.Rows.Add(dr);
				}
			}
			#endregion


			if (retrieved.MoreRecords)
			{
				query.PageInfo.PagingCookie = retrieved.PagingCookie;
				RequestMeetingView.VirtualItemCount = retrieved.TotalRecordCount;
			}
			

			
			RequestMeetingView.PageSize = 10;
			RequestMeetingView.DataSource = dtb;
			RequestMeetingView.DataBind();
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
				FillMeetingList(1);
			}
		}
		protected void LinkSignOut_Click(object sender, EventArgs e)
		{
			Session["Personnelid"] = null;
			HttpResponse.RemoveOutputCacheItem("/MeetingsList.aspx");
			HttpResponse.RemoveOutputCacheItem("/ClassDetail.aspx");
			HttpResponse.RemoveOutputCacheItem("/ClassList.aspx");
			Response.Redirect("Login.aspx");
		}
		protected void RefreshBtn_Click(object sender, ImageClickEventArgs e)
		{
			FillMeetingList(1);
		}
		protected void linkRequest_Click(object sender, EventArgs e)
		{
			Response.Redirect("ClassDetail.aspx?id=" + ClassId.ToString());
		}
		protected void RequestMeetingView_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Open")
			{
				Response.Redirect("MeetingDetail.aspx?meetingId=" + e.CommandArgument.ToString());
			}
		}
		protected void RequestMeetingView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
				e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
				e.Row.ToolTip = "Click last column for selecting this row.";
			}
		}
		protected void RequestMeetingView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			RequestMeetingView.PageIndex = e.NewPageIndex;
			FillMeetingList(e.NewPageIndex + 1);
		}
		protected void RequestMeetingView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{

		}

		#endregion

		protected void RequestMeetingView_PreRender(object sender, EventArgs e)
		{
			GridView gv = (GridView)sender;
			GridViewRow pagerRow = (GridViewRow)gv.BottomPagerRow;

			if (pagerRow != null && pagerRow.Visible == false)
				pagerRow.Visible = true;
		}
	}
}
