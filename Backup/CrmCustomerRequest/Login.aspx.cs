using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using System.Configuration;
using Microsoft.Xrm.Sdk.Discovery;
using System.Net;
using System.ServiceModel;

namespace CrmCustomerRequest
{
    public partial class Login : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
                MessageBox.Show("لطفا آدرس پست الکترونیک را وارد كنيد");
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("لطفا رمز عبور را وارد كنيد");
            else
            {
                string _userName = ConfigurationManager.AppSettings["username"].ToString();
                string _userPass = ConfigurationManager.AppSettings["Password"].ToString();
                string _userDomain = ConfigurationManager.AppSettings["Domain"].ToString();
                string _userOrganization = ConfigurationManager.AppSettings["Organization"].ToString();
                string _userUrl = ConfigurationManager.AppSettings["Url"].ToString();
                NetworkCredential _networkCredential = new NetworkCredential(_userName, _userPass, _userDomain);
                IOrganizationService _orgService = Ayco.MSCRM5.SDK.Service.GetOrgService(_userOrganization, _userUrl, false, _networkCredential);
                try
                {
                    QueryExpression _userquery = new QueryExpression("contact");
                    _userquery.ColumnSet = new ColumnSet("contactid", "emailaddress1");
                    _userquery.Criteria = new FilterExpression(LogicalOperator.And);
                    _userquery.Criteria.AddCondition("emailaddress1", ConditionOperator.Equal, txtUsername.Text);
                    _userquery.Criteria.AddCondition("helpdesk_password", ConditionOperator.Equal, txtPassword.Text);
                    EntityCollection _contactlist = _orgService.RetrieveMultiple(_userquery);
                    if (_contactlist == null || _contactlist.Entities.Count==0)
                    {
                        MessageBox.Show("کاربر در سیستم وجود ندارد");
                    }
                    else
                    {
                        Session["Personnelid"] = _contactlist.Entities[0].Attributes["contactid"].ToString();
                        Response.Redirect("AddRequest.aspx?EmployeeId=" + _contactlist.Entities[0].Attributes["contactid"].ToString().Trim());
                    }
                }
                catch (FaultException<OrganizationServiceFault> ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        protected void LinkRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }
        #endregion
    }
}