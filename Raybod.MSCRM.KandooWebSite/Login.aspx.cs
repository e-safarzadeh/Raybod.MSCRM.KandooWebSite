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

namespace Raybod.MSCRM.KandooWebSite
{
    public partial class Login : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
                MessageBox.Show("لطفا نام کاربری خود را وارد كنيد");
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("لطفا رمز عبور را وارد كنيد");
            else if (!CaptchaControl1.IsValid)
                MessageBox.Show("كد امنيتي صحيح نمي باشد");
            else
            {
                string _userName = ConfigurationManager.AppSettings["username"].ToString();
                string _userPass = ConfigurationManager.AppSettings["Password"].ToString();
                string _userDomain = ConfigurationManager.AppSettings["Domain"].ToString();
                string _userOrganization = ConfigurationManager.AppSettings["Organization"].ToString();
                string _userUrl = ConfigurationManager.AppSettings["Url"].ToString();
                NetworkCredential _networkCredential = new NetworkCredential(_userName, _userPass, _userDomain);
                IOrganizationService _orgService = Raybod.MSCRM5.SDK.Service.GetOrgService(_userOrganization, _userUrl, false, _networkCredential);
                try
                {
                    QueryExpression _userquery = new QueryExpression("systemuser");
                    _userquery.ColumnSet = new ColumnSet("systemuserid", "new_username", "new_password", "new_loginwebsite");
                    _userquery.Criteria = new FilterExpression(LogicalOperator.And);
                    _userquery.Criteria.AddCondition("new_username", ConditionOperator.Equal, txtUserName.Text);
                    _userquery.Criteria.AddCondition("new_loginwebsite", ConditionOperator.Equal, true);

                    EntityCollection list = _orgService.RetrieveMultiple(_userquery);
                    if (list == null || list.Entities.Count == 0)
                    {
                        MessageBox.Show("نام کاربری وارد شده در سیستم وجود ندارد");
                    }
                    else
                    {
                        if (list.Entities[0].Contains("new_password") && list.Entities[0].Attributes["new_password"] != null)
                        {
                            string pass = list.Entities[0].Attributes["new_password"].ToString();
                            if (string.Compare(pass, txtPassword.Text) != 0)
                                MessageBox.Show("رمز عبور وارد شده صحیح نمی باشد");
                            else
                            {
                                Session["Personnelid"] = list.Entities[0].Attributes["systemuserid"].ToString();
                                Response.Redirect("ClassList.aspx?TeacherId=" + list.Entities[0].Attributes["systemuserid"].ToString().Trim());
                            }
                        }
                        else
                            MessageBox.Show("رمز عبور وارد شده صحیح نیست");
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
        #endregion
    }
}