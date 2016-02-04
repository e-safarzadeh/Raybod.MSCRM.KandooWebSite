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
    public partial class Registration : System.Web.UI.Page
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
        #endregion

        #region Methods
        private void FillDepartment()
        {
            Department.Items.Clear();
            try
            {
                QueryByAttribute _query = new QueryByAttribute("account");
                _query.ColumnSet = new ColumnSet("name", "accountid");
                _query.AddAttributeValue("statecode", 0);
                EntityCollection _AccountList = crmService.RetrieveMultiple(_query);
                if (_AccountList != null && _AccountList.Entities.Count > 0)
                {
                    for (int i = 0; i < _AccountList.Entities.Count; i++)
                    {
                        ListItem _item = new ListItem(_AccountList.Entities[i]["name"].ToString(), _AccountList.Entities[i]["accountid"].ToString());
                        Department.Items.Add(_item);
                    }
                }
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CheckEmail()
        {
            bool result = false;
            try
            {
                QueryByAttribute _query = new QueryByAttribute("contact");
                _query.ColumnSet = new ColumnSet("emailaddress1", "accountid");
                _query.AddAttributeValue("emailaddress1",txtEmail.Text.Trim());
                EntityCollection _ContactList = crmService.RetrieveMultiple(_query);
                if (_ContactList != null && _ContactList.Entities.Count > 0)
                {
                    result = false;
                }
                else
                    result = true;
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillDepartment();
            }
        }

        protected void LinkRequestList_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtPassword1.Text)
            {
                MessageBox.Show("رمز عبور و تکرار رمز عبور، یکسان نمی باشد");
                return;
            }
            if (!CheckEmail())
            {
                MessageBox.Show("آدرس پست الکترونیک در سیستم وجود دارد");
                return;
            }
            Entity Contact = new Entity("contact");
            Contact["firstname"] = txtName.Text.Trim();
            Contact["lastname"] = txtFamily.Text.Trim();
            Contact["helpdesk_personnelcode"] = txtEmployeeNo.Text.Trim();
            Contact["helpdesk_salutaion"] = new OptionSetValue(Convert.ToInt32(Salutation.SelectedValue));
            Contact["parentcustomerid"] = new EntityReference("account", new Guid(Department.SelectedValue.ToString()));
            Contact["emailaddress1"] = txtEmail.Text.Trim();
            Contact["mobilephone"] = txtMobile.Text.Trim();
            Contact["helpdesk_password"] = txtPassword.Text;
            Contact["helpdesk_requestforuser"] = CheckBoxSms.Checked;
            Contact["helpdesk_usesms"] = CheckBoxCrm.Checked;
            crmService.Create(Contact);
            Response.Redirect("RegistrationConfirm.aspx");
        }

        protected void CheckBoxSms_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxSms.Checked)
            {
                RequiredFieldValidator9.Enabled = true;
            }
            else
            {
                RequiredFieldValidator9.Enabled = false;
            }
        }
        #endregion
    }
}