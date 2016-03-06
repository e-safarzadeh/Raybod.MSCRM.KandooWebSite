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
    public partial class MeetingDetail : System.Web.UI.Page
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

        public string meetingId
        {
            get
            {
                return Request.QueryString["meetingId"];
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

        private void FillStudentList()
        {
            #region Create Table
            DataTable dtb = new DataTable();
            dtb.Columns.Add("gendercode");
            dtb.Columns.Add("studentnumber");
            dtb.Columns.Add("name");
            dtb.Columns.Add("relatedclass");
            dtb.Columns.Add("relatedcompanyid");
            dtb.Columns.Add("attendanceitemsid");
            #endregion

            #region Get Record From Crm
            QueryExpression query = new QueryExpression("new_attendanceitems");
            query.ColumnSet = new ColumnSet(new string[] { "new_name", "new_relatedclass", "new_relatedcompanyid", "new_relatedregistrationid",
                                                           "new_attendanceitemsid", "createdon", "new_relatedattendanceid", "new_studentid", "new_present"});
            query.Criteria.AddCondition("new_relatedattendanceid", ConditionOperator.Equal, new Guid(meetingId));
            OrderExpression _Order = new OrderExpression("new_studentid", OrderType.Ascending);
            query.Orders.Add(_Order);

            EntityCollection retrieved = crmService.RetrieveMultiple(query);
            #endregion

            #region Fill DataTable
            if (retrieved != null && retrieved.Entities.Count > 0)
            {
                for (int i = 0; i < retrieved.Entities.Count; i++)
                {
                    DataRow dr = dtb.NewRow();
                    Entity student = crmService.Retrieve("contact", ((EntityReference)retrieved.Entities[i].Attributes["new_studentid"]).Id, new ColumnSet(new string[] { "contactid", "gendercode", "new_studentnumber" }));
                    if (student.Contains("gendercode") && student["gendercode"] != null)
                        dr["gendercode"] = GetOptionsSetTextOnValue(crmService, "contact", "gendercode", ((OptionSetValue)student["gendercode"]).Value, 1065);
                    if (student.Contains("new_studentnumber") && student["new_studentnumber"] != null)
                        dr["studentnumber"] = (string)student["new_studentnumber"];
                    if (retrieved.Entities[i].Contains("new_studentid"))
                        dr["name"] = ((EntityReference)retrieved.Entities[i].Attributes["new_studentid"]).Name;
                    if (retrieved.Entities[i].Contains("new_relatedclass"))
                        dr["relatedclass"] = ((EntityReference)retrieved.Entities[i].Attributes["new_relatedclass"]).Name;
                    if (retrieved.Entities[i].Contains("new_relatedcompanyid") && (EntityReference)retrieved.Entities[i].Attributes["new_relatedcompanyid"] != null)
                        dr["relatedcompanyid"] = ((EntityReference)retrieved.Entities[i].Attributes["new_relatedcompanyid"]).Name;
                    if (retrieved.Entities[i].Contains("new_attendanceitemsid"))
                        dr["attendanceitemsid"] = (retrieved.Entities[i].Attributes["new_attendanceitemsid"]).ToString();
                    dtb.Rows.Add(dr);
                }
            }
            #endregion

            RequestMeetingDetailView.DataSource = dtb;
            RequestMeetingDetailView.DataBind();
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

                if (meetingId != null)
                {
                    //TimeSpan time = new TimeSpan(3, 30, 0);
                    //محل پر کردن آیتم ها جزئیات جلسه
                    Entity _meetingEn = crmService.Retrieve("new_attendancelist", new Guid("{" + meetingId + "}"), new ColumnSet(true));
                    txtClassName.Text = ((EntityReference)_meetingEn["new_relatedclassid"]).Name;
                    txtMeetingName.Text = _meetingEn["new_name"].ToString();
                    txtStatus.Text = GetOptionsSetTextOnValue(crmService, "new_attendancelist", "new_status", ((OptionSetValue)_meetingEn["new_status"]).Value, 1065);
                    cmbStarttime.SelectedValue = (((OptionSetValue)_meetingEn["new_starttime1"]).Value).ToString();
                    //cmbMinutesStart.SelectedValue = (((OptionSetValue)_meetingEn["new_minutesstart"]).Value).ToString();
                    cmbEndTime.SelectedValue = (((OptionSetValue)_meetingEn["new_endtime1"]).Value).ToString();
                    //cmdMinutesEnd.SelectedValue = (((OptionSetValue)_meetingEn["new_minutesend"]).Value).ToString();

                    //پر نمودن گرید
                    FillStudentList();
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
            HttpResponse.RemoveOutputCacheItem("/MeetingDetail.aspx");
            HttpResponse.RemoveOutputCacheItem("/MeetingsList.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassDetail.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassList.aspx");
            Response.Redirect("Login.aspx");
        }
        protected void RefreshBtn_Click(object sender, ImageClickEventArgs e)
        {
            FillStudentList();
        }
        protected void LinkMeetingList_Click(object sender, EventArgs e)
        {
            Entity _meetinglst = crmService.Retrieve("new_attendancelist", new Guid("{" + meetingId + "}"), new ColumnSet(true));
            Guid classId = ((EntityReference)_meetinglst["new_relatedclassid"]).Id;
            Response.Redirect("MeetingsList.aspx?ClassId=" + classId.ToString());
        }
        protected void RequestMeetingDetailView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Open")
            {
                Response.Redirect("Registration.aspx?meetingDetailId=" + e.CommandArgument.ToString());
            }
        }
        protected void RequestMeetingDetailView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click last column for selecting this row.";

                string id = RequestMeetingDetailView.DataKeys[e.Row.RowIndex]["attendanceitemsid"].ToString();
                if (id != null)
                {
                    Entity attendanceItem = crmService.Retrieve("new_attendanceitems", new Guid("{" + id + "}"), new ColumnSet(true));
                    RadioButtonList rb = (RadioButtonList)e.Row.FindControl("RadioButtonList1");
                    if (attendanceItem.Contains("new_present") && ((OptionSetValue)attendanceItem["new_present"]).Value == 1)
                        rb.SelectedValue = "1";
                    if (attendanceItem.Contains("new_present") && ((OptionSetValue)attendanceItem["new_present"]).Value == 2)
                        rb.SelectedValue = "2";
                }
            }

        }

        //protected void RequestMeetingDetailView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    RequestMeetingDetailView.PageIndex = e.NewPageIndex;
        //    FillStudentList();
        //}
        protected void RequestMeetingDetailView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in RequestMeetingDetailView.Rows)
            {
                RadioButtonList ralist = RequestMeetingDetailView.Rows[gr.RowIndex].FindControl("RadioButtonList1") as RadioButtonList;
                string id = RequestMeetingDetailView.DataKeys[gr.RowIndex]["attendanceitemsid"].ToString();
                if (id != null)
                {
                    if(ralist.SelectedIndex == -1)
                    {
                        MessageBox.Show("لطفاً حضور و غیاب تمامی دانشجویان را ثبت نمایید");
                        return;
                    }
                    Entity attendanceItem = crmService.Retrieve("new_attendanceitems", new Guid("{" + id + "}"), new ColumnSet(true));
                    if (ralist.SelectedIndex == 0)
                        attendanceItem["new_present"] = new OptionSetValue(1);
                    else if (ralist.SelectedIndex == 1)
                        attendanceItem["new_present"] = new OptionSetValue(2);
                    crmService.Update(attendanceItem);

                    
                }
            }

            // ست نمودن مقدار جدید ساعت شروع و پایان در سی آر ام
            Entity _meetinglst = crmService.Retrieve("new_attendancelist", new Guid("{" + meetingId + "}"), new ColumnSet(true));

            _meetinglst["new_starttime1"] = new OptionSetValue(Convert.ToInt32(cmbStarttime.SelectedValue));
            _meetinglst["new_minutesstart"] = new OptionSetValue(Convert.ToInt32(cmbMinutesStart.SelectedValue));
            _meetinglst["new_endtime1"] = new OptionSetValue(Convert.ToInt32(cmbEndTime.SelectedValue));
            _meetinglst["new_minutesend"] = new OptionSetValue(Convert.ToInt32(cmdMinutesEnd.SelectedValue));
            _meetinglst["new_status"] = new OptionSetValue(100000001);
            crmService.Update(_meetinglst);


            Guid classId = ((EntityReference)_meetinglst["new_relatedclassid"]).Id;
            Response.Redirect("MeetingsList.aspx?ClassId=" + classId.ToString());
        }
        #endregion
    }
}