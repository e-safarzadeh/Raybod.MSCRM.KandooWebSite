using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Configuration;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk;
using System.ServiceModel;

namespace Raybod.MSCRM5.SDK
{
    public class Service
    {
        #region Methods
        public static OrganizationServiceProxy GetOrgService(string organizationName, string organizationUrl, bool useDefaultCredentials, System.Net.NetworkCredential networkCredential)
        {
            if (!organizationUrl.EndsWith("/"))
                organizationUrl += "/";
            Uri orgUri = new Uri(string.Format("{0}{1}/XRMServices/2011/Organization.svc", organizationUrl, organizationName));

            ClientCredentials credentials = new ClientCredentials();
            if (!useDefaultCredentials)
                credentials.Windows.ClientCredential = networkCredential;

            OrganizationServiceProxy orgService = new OrganizationServiceProxy(orgUri, null, credentials, null);
            orgService.Timeout = new TimeSpan(0, 5, 0);
            return orgService;
        }
        #endregion
    }
}
