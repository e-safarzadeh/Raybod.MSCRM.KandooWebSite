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

namespace CrmCustomerRequest
{
    public partial class RequestConfirm : System.Web.UI.Page
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
        public string CaseId
        {
            get
            {
                return Request.QueryString["CaseId"];
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
                Entity _Contact = new Entity("contact");
                _Contact = crmService.Retrieve("contact", new Guid(Session["Personnelid"].ToString()), new ColumnSet(true));
                //-----------------------------------------------------
                LabelPersonnel.Text = _Contact["fullname"].ToString();
                //-----------------------------------------------------
                Entity _incident = crmService.Retrieve("incident", new Guid("{" + CaseId + "}"), new ColumnSet(true));
                if (_incident != null)
                {
                    if (_incident.Attributes["ticketnumber"] != null)
                        txtCaseNumber.Text = _incident.Attributes["ticketnumber"].ToString();
                }
            }
        }

        protected void LinkAddRequest_Click(object sender, EventArgs e)
        {
           Response.Redirect("AddRequest.aspx");
        }
        protected void LinkRequestList_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestList.aspx");
        }
        protected void LinkSignOut_Click(object sender, EventArgs e)
        {
            Session["Personnelid"] = null;
            Response.Redirect("Login.aspx");
        }
        #endregion
    }
}