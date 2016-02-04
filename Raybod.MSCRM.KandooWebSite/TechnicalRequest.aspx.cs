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
    public partial class TechnicalRequest : System.Web.UI.Page
    {
        #region Properties
        IOrganizationService crmService = null;

        public string ClassId
        {
            get
            {
                return Request.QueryString["ClassId"];
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

                if (ClassId != null)
                {
                    Entity _class = crmService.Retrieve("new_class", new Guid(ClassId), new ColumnSet(new string[] { "new_classid", "new_name" }));
                    txtClass.Text = _class["new_name"].ToString();
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
            HttpResponse.RemoveOutputCacheItem("/TechnicalRequest.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassDetail.aspx");
            HttpResponse.RemoveOutputCacheItem("/ClassList.aspx");
            Response.Redirect("Login.aspx");
        }
        protected void linkRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassDetail.aspx?id=" + ClassId.ToString());
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region Add TeacherPaymentRequest
            if (ClassId != null)
            {
                Entity _class = crmService.Retrieve("new_class", new Guid(ClassId), new ColumnSet(new string[] { "new_classid", "new_name" }));
                Entity _technicalrqst = new Entity("new_classstechnicalrequest");
                _technicalrqst["new_name"] = "کلاس: " + _class["new_name"].ToString();
                _technicalrqst["new_requestdescription"] = txtDescription.Text;
                //_technicalrqst["new_teacherid"] = new EntityReference("systemuser", new Guid(Session["Personnelid"].ToString()));
                _technicalrqst["new_relatedclassid"] = new EntityReference("new_class", new Guid(ClassId));
                Guid technicalrqstId = crmService.Create(_technicalrqst);
                Response.Redirect("RequestConfirmTecnical.aspx?technicalrqstId=" + technicalrqstId.ToString());
            }
            #endregion
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        #endregion


    }
}