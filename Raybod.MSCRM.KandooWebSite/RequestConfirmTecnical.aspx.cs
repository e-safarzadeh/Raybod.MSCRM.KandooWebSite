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
    public partial class RequestConfirmTecnical : System.Web.UI.Page
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
        public string technicalId
        {
            get
            {
                return Request.QueryString["technicalrqstId"];
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
                LabelPersonnel.Text = _User["fullname"].ToString();
                Entity _technicalrqst = crmService.Retrieve("new_classstechnicalrequest", new Guid("{" + technicalId + "}"), new ColumnSet(true));
                if (_technicalrqst != null)
                {
                    if (_technicalrqst.Contains("new_code") && _technicalrqst.Attributes["new_code"] != null)
                        txtTechnicalNumber.Text = _technicalrqst.Attributes["new_code"].ToString();
                }
            }
        }

        protected void LinkAddRequest_Click(object sender, EventArgs e)
        {
            Entity _technicalrqst = crmService.Retrieve("new_classstechnicalrequest", new Guid("{" + technicalId + "}"), new ColumnSet(true));
            Guid classId = ((EntityReference)_technicalrqst["new_relatedclassid"]).Id;
            Response.Redirect("TechnicalRequest.aspx?ClassId=" + classId.ToString());
        }
        protected void LinkRequestList_Click(object sender, EventArgs e)
        {
            Entity _technicalrqst = crmService.Retrieve("new_classstechnicalrequest", new Guid("{" + technicalId + "}"), new ColumnSet(true));
            Guid classId = ((EntityReference)_technicalrqst["new_relatedclassid"]).Id;
            Response.Redirect("ClassDetail.aspx?id=" + classId.ToString());
        }
        protected void LinkSignOut_Click(object sender, EventArgs e)
        {
            Session["Personnelid"] = null;
            HttpResponse.RemoveOutputCacheItem("/RequestConfirmTecnical.aspx");
            HttpResponse.RemoveOutputCacheItem("/TechnicalRequest.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassDetail.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassList.aspx");
            Response.Redirect("Login.aspx");
        }
        #endregion
    }
}