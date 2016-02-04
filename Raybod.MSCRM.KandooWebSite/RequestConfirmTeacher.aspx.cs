using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Net;

namespace Raybod.MSCRM.KandooWebSite
{
    public partial class RequestConfirmTeacher : System.Web.UI.Page
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
        public string TeacherrqstId
        {
            get
            {
                return Request.QueryString["TeacherrqstId"];
            }
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
                //-----------------------------------------------------
                LabelPersonnel.Text = _User["fullname"].ToString();
                //-----------------------------------------------------
                Entity _teacherrqst = crmService.Retrieve("new_teacherpaymentrequest", new Guid("{" + TeacherrqstId + "}"), new ColumnSet(true));
                if (_teacherrqst != null)
                {
                    if (_teacherrqst.Contains("new_code") && _teacherrqst.Attributes["new_code"] != null)
                        txtTeacherNumber.Text = _teacherrqst.Attributes["new_code"].ToString();
                }
            }
        }

        protected void LinkAddRequest_Click(object sender, EventArgs e)
        {
            Entity _teacherrqst = crmService.Retrieve("new_teacherpaymentrequest", new Guid("{" + TeacherrqstId + "}"), new ColumnSet(true));
            Guid classId = ((EntityReference)_teacherrqst["new_classid"]).Id;
            Response.Redirect("TuitionRequest.aspx?ClassId=" + classId.ToString());
        }
        protected void LinkRequestList_Click(object sender, EventArgs e)
        {
            Entity _teacherrqst = crmService.Retrieve("new_teacherpaymentrequest", new Guid("{" + TeacherrqstId + "}"), new ColumnSet(true));
            Guid classId = ((EntityReference)_teacherrqst["new_classid"]).Id;
            Response.Redirect("ClassDetail.aspx?id=" + classId.ToString());
        }
        protected void LinkSignOut_Click(object sender, EventArgs e)
        {
            Session["Personnelid"] = null;
            HttpResponse.RemoveOutputCacheItem("/RequestConfirmTeacher.aspx");
            HttpResponse.RemoveOutputCacheItem("/TuitionRequest.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassDetail.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassList.aspx");
            Response.Redirect("Login.aspx");
        }
        #endregion
    }
}