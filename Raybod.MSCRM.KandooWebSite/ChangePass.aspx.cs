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
    public partial class ChangePass : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Personnelid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "")
                MessageBox.Show("لطفا رمز عبور قبلی را وارد كنيد");
            if (txtPassNew.Text.Trim() == "")
                MessageBox.Show("لطفا رمز عبور جدید را وارد كنيد");
            if (txtPassConfirm.Text.Trim() == "")
                MessageBox.Show("لطفا تأییدیه رمز عبور جدید را وارد كنيد");
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
                    //Entity _User = new Entity("systemuser");
                    Entity _User = _orgService.Retrieve("systemuser", new Guid(Session["Personnelid"].ToString()), new ColumnSet(new string[] { "systemuserid", "new_password" }));
                    string oldPass = (string)_User["new_password"];
                    if (string.Compare(oldPass, txtPassword.Text) != 0)
                        MessageBox.Show("رمز عبور قبلی وارد شده صحیح نمی باشد");
                    else
                    {
                        string newPass = txtPassNew.Text;
                        string confirmPass = txtPassConfirm.Text;
                        if (string.Compare(newPass, confirmPass) != 0)
                            MessageBox.Show("عدم مطابقت رمز عبور جدید");
                        else
                        {
                            _User["new_password"] = txtPassNew.Text;
                            _orgService.Update(_User);
                            Response.Redirect("ClassList.aspx?TeacherId=" + _User["systemuserid"].ToString().Trim());
                        }
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