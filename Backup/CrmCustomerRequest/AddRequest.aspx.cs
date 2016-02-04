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

namespace CrmCustomerRequest
{
    public partial class AddRequest : System.Web.UI.Page
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

        #region Method
        private void LoadcmbType()
        {
            cmbType.Items.Clear();
            RetrieveAttributeRequest req = new RetrieveAttributeRequest();
            req.EntityLogicalName = "incident";
            req.LogicalName = "casetypecode";
            req.RetrieveAsIfPublished = true;

            RetrieveAttributeResponse attributeResponse = (RetrieveAttributeResponse)crmService.Execute(req);
            PicklistAttributeMetadata retrievedPicklistAttributeMetadata = (Microsoft.Xrm.Sdk.Metadata.PicklistAttributeMetadata)attributeResponse.AttributeMetadata;
            OptionMetadata[] optionList = retrievedPicklistAttributeMetadata.OptionSet.Options.ToArray();

            foreach (OptionMetadata oMD in optionList)
            {
                foreach (var item in oMD.Label.LocalizedLabels)
                {
                    if (item.LanguageCode == 1065)
                    {
                        ListItem _item = new ListItem(item.Label, oMD.Value.Value.ToString());
                        cmbType.Items.Add(_item);
                        break;
                    }
                }
            }
        }
        private void LoadCT1()
        {
            cmbCT1.Items.Clear();
            try
            {
                QueryExpression _query = new QueryExpression("helpdesk_ticketsubjectlevel1");
                _query.ColumnSet = new ColumnSet("helpdesk_name", "helpdesk_ticketsubjectlevel1id");
                EntityCollection _CT1List = crmService.RetrieveMultiple(_query);
                if (_CT1List != null && _CT1List.Entities.Count > 0)
                {
                    for (int i = 0; i < _CT1List.Entities.Count; i++)
                    {
                        ListItem _item = new ListItem(_CT1List.Entities[i]["helpdesk_name"].ToString(), _CT1List.Entities[i]["helpdesk_ticketsubjectlevel1id"].ToString());
                        cmbCT1.Items.Add(_item);
                    }
                }
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                string strError = ex.Message.Replace("'", "");
                string scriptstring = "alert('" + strError + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

                //MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                string strError = ex.Message.Replace("'", "");
                string scriptstring = "alert('" + strError + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

                //MessageBox.Show(ex.Message);
            }
        }
        private void LoadCT2()
        {
            cmbCT2.Items.Clear();
            try
            {
                ListItem noneitem = new ListItem("(None)", new Guid().ToString());
                cmbCT2.Items.Add(noneitem);
                if (!string.IsNullOrEmpty(cmbCT1.SelectedValue) && cmbCT1.SelectedValue != new Guid().ToString())
                {
                    QueryByAttribute _query = new QueryByAttribute("helpdesk_ticketsubjectlevel2");
                    _query.ColumnSet = new ColumnSet("helpdesk_name", "helpdesk_ticketsubjectlevel2id");
                    _query.AddAttributeValue("helpdesk_ticketsubjectlevel1", new Guid(cmbCT1.SelectedValue));
                    EntityCollection _CT2List = crmService.RetrieveMultiple(_query);
                    if (_CT2List != null && _CT2List.Entities.Count > 0)
                    {
                        for (int i = 0; i < _CT2List.Entities.Count; i++)
                        {
                            ListItem _item = new ListItem(_CT2List.Entities[i]["helpdesk_name"].ToString(), _CT2List.Entities[i]["helpdesk_ticketsubjectlevel2id"].ToString());
                            cmbCT2.Items.Add(_item);
                        }
                    }
                }
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                string strError = ex.Message.Replace("'", "");
                string scriptstring = "alert('" + strError + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

                //MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                string strError = ex.Message.Replace("'", "");
                string scriptstring = "alert('" + strError + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

                //MessageBox.Show(ex.Message);
            }
        }
        private void LoadCT3()
        {
            cmbCT3.Items.Clear();
            try
            {
                ListItem noneitem = new ListItem("(None)", new Guid().ToString());
                cmbCT3.Items.Add(noneitem);
                if (!string.IsNullOrEmpty(cmbCT2.SelectedValue) && cmbCT2.SelectedValue != new Guid().ToString())
                {
                    QueryByAttribute _query = new QueryByAttribute("helpdesk_ticketsubjectlevel3");
                    _query.ColumnSet = new ColumnSet("helpdesk_name", "helpdesk_ticketsubjectlevel3id");
                    _query.AddAttributeValue("helpdesk_ticketsubjectlevel2", new Guid(cmbCT2.SelectedValue));
                    EntityCollection _CT3List = crmService.RetrieveMultiple(_query);
                    if (_CT3List != null && _CT3List.Entities.Count > 0)
                    {
                        for (int i = 0; i < _CT3List.Entities.Count; i++)
                        {
                            ListItem _item = new ListItem(_CT3List.Entities[i]["helpdesk_name"].ToString(), _CT3List.Entities[i]["helpdesk_ticketsubjectlevel3id"].ToString());
                            cmbCT3.Items.Add(_item);
                        }
                    }
                }
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                string strError = ex.Message.Replace("'", "");
                string scriptstring = "alert('" + strError + "');"; 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

                //MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                string strError = ex.Message.Replace("'", "");
                string scriptstring = "alert('" + strError + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

                //MessageBox.Show(ex.Message);
            }
        }
        private void LoadCT4()
        {
            cmbCT4.Items.Clear();
            try
            {
                ListItem noneitem = new ListItem("(None)", new Guid().ToString());
                cmbCT4.Items.Add(noneitem);
                if (!string.IsNullOrEmpty(cmbCT3.SelectedValue) && cmbCT3.SelectedValue != new Guid().ToString())
                {
                    QueryByAttribute _query = new QueryByAttribute("helpdesk_ticketsubjectlevel4");
                    _query.ColumnSet = new ColumnSet("helpdesk_name", "helpdesk_ticketsubjectlevel4id");
                    _query.AddAttributeValue("helpdesk_ticketsubjectlevel3", new Guid(cmbCT3.SelectedValue));
                    EntityCollection _CT4List = crmService.RetrieveMultiple(_query);
                    if (_CT4List != null && _CT4List.Entities.Count > 0)
                    {
                        for (int i = 0; i < _CT4List.Entities.Count; i++)
                        {
                            ListItem _item = new ListItem(_CT4List.Entities[i]["helpdesk_name"].ToString(), _CT4List.Entities[i]["helpdesk_ticketsubjectlevel4id"].ToString());
                            cmbCT4.Items.Add(_item);
                        }
                    }
                }
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                string strError = ex.Message.Replace("'", "");
                string scriptstring = "alert('" + strError + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

                //MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                string strError = ex.Message.Replace("'", "");
                string scriptstring = "alert('" + strError + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertscript", scriptstring, true);

                //MessageBox.Show(ex.Message);
            }
        }
        private void LoadFrom()
        {
            cmbFrom.Items.Clear();
            ListItem noneitem = new ListItem("(None)", new Guid().ToString());
            cmbFrom.Items.Add(noneitem);
            QueryExpression _query = new QueryExpression("contact");
            _query.ColumnSet = new ColumnSet("contactid", "fullname");
            EntityCollection _From = crmService.RetrieveMultiple(_query);
            if (_From != null && _From.Entities.Count > 0)
            {
                for (int i = 0; i < _From.Entities.Count; i++)
                {
                    if (_From[i]["contactid"].ToString() != Session["Personnelid"].ToString())
                    {
                        ListItem _item = new ListItem(_From.Entities[i]["fullname"].ToString(), _From.Entities[i]["contactid"].ToString());
                        cmbFrom.Items.Add(_item);
                    }
                }
            }
        }
        #endregion

        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Personnelid"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                LoadcmbType();
                LoadCT1();
                LoadCT2();
                LoadCT3();
                LoadCT4();
                LoadFrom();
                Entity _Contact = new Entity("contact");
                _Contact = crmService.Retrieve("contact", new Guid(Session["Personnelid"].ToString()), new ColumnSet(true));
                //-----------------------------------------------------
                LabelPersonnel.Text = _Contact["fullname"].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            #region Check Value
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileContent.Length > Convert.ToInt32(ConfigurationManager.AppSettings["LimitedSize"]))
                {
                    MessageBox.Show("اندازه فايلهای انتخاب شده بايد كمتر از " + ConfigurationManager.AppSettings["LimitedSize"].ToString() + " بايت باشد");
                    return;
                }
            }
            if (FileUpload2.HasFile)
            {
                if (FileUpload2.FileContent.Length > Convert.ToInt32(ConfigurationManager.AppSettings["LimitedSize"]))
                {
                    MessageBox.Show("اندازه فايلهای انتخاب شده بايد كمتر از " + ConfigurationManager.AppSettings["LimitedSize"].ToString() + " بايت باشد");
                    return;
                }
            }
            if (FileUpload3.HasFile)
            {
                if (FileUpload3.FileContent.Length > Convert.ToInt32(ConfigurationManager.AppSettings["LimitedSize"]))
                {
                    MessageBox.Show("اندازه فايلهای انتخاب شده بايد كمتر از " + ConfigurationManager.AppSettings["LimitedSize"].ToString() + " بايت باشد");
                    return;
                }
            }
            if (string.IsNullOrEmpty(cmbCT1.SelectedValue) || cmbCT1.SelectedValue == new Guid().ToString())
            {
                MessageBox.Show("لطفا عنوان درخواست را وارد كنيد.");
                return;
            }
            #endregion

            //--------------Add Case------------------------------
            #region Add Case
            Entity _Contact = new Entity("contact");
            _Contact = crmService.Retrieve("contact", new Guid(Session["Personnelid"].ToString()), new ColumnSet(true));
            Entity _incident = new Entity("incident");
            string subj = "";
            if (!string.IsNullOrEmpty(cmbCT1.SelectedValue) && cmbCT1.SelectedValue != new Guid().ToString())
            {
                _incident["helpdesk_ticketsubjectlevel1"] = new EntityReference("helpdesk_ticketsubjectlevel1", new Guid(cmbCT1.SelectedValue));
                subj += cmbCT1.SelectedItem.Text + " ";
            }
            if (!string.IsNullOrEmpty(cmbCT2.SelectedValue) && cmbCT2.SelectedValue != new Guid().ToString())
            {
                _incident["helpdesk_ticketsubjectlevel2"] = new EntityReference("helpdesk_ticketsubjectlevel2", new Guid(cmbCT2.SelectedValue));
                subj += cmbCT2.SelectedItem.Text + " ";
            }
            if (!string.IsNullOrEmpty(cmbCT3.SelectedValue) && cmbCT3.SelectedValue != new Guid().ToString())
            {
                _incident["helpdesk_ticketsubjectlevel3"] = new EntityReference("helpdesk_ticketsubjectlevel3", new Guid(cmbCT3.SelectedValue));
                subj += cmbCT3.SelectedItem.Text + " ";
            }
            if (!string.IsNullOrEmpty(cmbCT4.SelectedValue) && cmbCT4.SelectedValue != new Guid().ToString())
            {
                _incident["helpdesk_ticketsubjectlevel4"] = new EntityReference("helpdesk_ticketsubjectlevel4", new Guid(cmbCT4.SelectedValue));
                subj += cmbCT4.SelectedItem.Text + " ";
            }
            _incident["casetypecode"] = new OptionSetValue(Convert.ToInt32(cmbType.SelectedValue));
            _incident["description"] = txtTitle.Text;
            _incident["customerid"] = new EntityReference("contact", new Guid(Session["Personnelid"].ToString()));
            _incident["caseorigincode"] = new OptionSetValue(1);
            _incident["title"] = "درخواست کننده : " + _Contact["fullname"].ToString() + " موضوع درخواست : " + subj;

            if (!string.IsNullOrEmpty(cmbFrom.SelectedValue) && cmbFrom.SelectedValue != new Guid().ToString())
            {
                _incident["helpdesk_requestfrom"] = new EntityReference("contact", new Guid(cmbFrom.SelectedValue));
            }
            _incident["helpdesk_statusdescription"] = new OptionSetValue(1);
            _incident["prioritycode"] = new OptionSetValue(Convert.ToInt32(cmbPriority.SelectedValue));
            if (FarsiDatePicker5.Value != new DateTime())
                _incident["followupby"] = FarsiDatePicker5.Value;
            _incident["statecode"] = new OptionSetValue(0);
            _incident["statuscode"] = new OptionSetValue(522040001);
            Guid incidentId = crmService.Create(_incident);
            #endregion

            //--------------Add Case Attachment------------------------------
            #region Add Case Attachment
            if (FileUpload1.HasFile)
            {
                Entity _annotation = new Entity("annotation");
                _annotation["documentbody"] = Convert.ToBase64String(FileUpload1.FileBytes);
                _annotation["mimetype"] = "application/ms-infopath.xml";
                _annotation["isdocument"] = true;
                _annotation["filename"] = FileUpload1.FileName;
                _annotation["notetext"] = "";
                _annotation["objecttypecode"] = "incident";
                _annotation["objectid"] = new EntityReference("incident", incidentId);


                crmService.Create(_annotation);
            }
            if (FileUpload2.HasFile)
            {
                Entity _annotation = new Entity("annotation");
                _annotation["documentbody"] = Convert.ToBase64String(FileUpload2.FileBytes);
                _annotation["mimetype"] = "application/ms-infopath.xml";
                _annotation["isdocument"] = true;
                _annotation["filename"] = FileUpload2.FileName;
                _annotation["notetext"] = "";
                _annotation["objecttypecode"] = "incident";
                _annotation["objectid"] = new EntityReference("incident", incidentId);
                crmService.Create(_annotation);
            }
            if (FileUpload3.HasFile)
            {
                Entity _annotation = new Entity("annotation");
                _annotation["documentbody"] = Convert.ToBase64String(FileUpload3.FileBytes);
                _annotation["mimetype"] = "application/ms-infopath.xml";
                _annotation["isdocument"] = true;
                _annotation["filename"] = FileUpload3.FileName;
                _annotation["notetext"] = "";
                _annotation["objecttypecode"] = "incident";
                _annotation["objectid"] = new EntityReference("incident", incidentId);
                crmService.Create(_annotation);
            }
            #endregion

            Response.Redirect("RequestConfirm.aspx?CaseId=" + incidentId.ToString());
        }

        protected void LinkRequestList_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestList.aspx");
        }
        protected void LinkSignOut_Click(object sender, EventArgs e)
        {
            Session["Personnelid"] = null;
            HttpResponse.RemoveOutputCacheItem("/AddRequest.aspx");
            HttpResponse.RemoveOutputCacheItem("/RequestConfirm.aspx");
            HttpResponse.RemoveOutputCacheItem("/RequestList.aspx");
            Response.Redirect("Login.aspx");
        }

        protected void cmbCT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCT3();
            LoadCT4();
        }
        protected void cmbCT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCT2();
            LoadCT3();
            LoadCT4();
        }
        protected void cmbCT3_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCT4();
        }
        #endregion
    }
}