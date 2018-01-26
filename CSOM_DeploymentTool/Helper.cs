using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CSOM_DeploymentTool
{
    public class Helper
    {
        internal static ClientContext GetClientContext()
        {

            //Console.WriteLine("Enter UserName");
            //string userName = Console.ReadLine();

            //  //For On-Premise

            //CredentialCache cc = new CredentialCache();
            //cc.Add(new Uri("SITEURL"), "NTLM", CredentialCache.DefaultNetworkCredentials);


            //ClientContext context = new ClientContext("SITEURL")
            //{
            //    Credentials = cc,

            //    AuthenticationMode = ClientAuthenticationMode.Default

            //      // with Windows Authentication
            //    Credentials = CredentialCache.DefaultNetworkCredentials,

            //       // With custom Credential with Domain
            //    Credentials = new NetworkCredential("USERNAME", "PASSWORD", "DOMAINNAME")

            //       // With custom Credential without Domain
            //    Credentials = new NetworkCredential("USERNAMEt", "PASSWORD")

            //      // Form based Authentication
            //      AuthenticationMode = ClientAuthenticationMode.FormsAuthentication,
            //      FormsAuthenticationLoginInfo = new FormsAuthenticationLoginInfo("USERNAME", "PASSWORD")

            //};

            //  //For Office 365 - SharePoint Online

            ClientContext context = new ClientContext("https://mazige.sharepoint.com/sites/dev/")
            {
                Credentials = new SharePointOnlineCredentials("abhishek@mazige.onmicrosoft.com", GetPassword()),
            };

            return context;
        }

        private static SecureString GetPassword()
        {
            //Console.WriteLine("Enter Password");
            //string password = string.Empty;
            string password = "Admin@123";
            //ConsoleKeyInfo info = Console.ReadKey(true);
            //while (info.Key != ConsoleKey.Enter)
            //{
            //    if (info.Key != ConsoleKey.Backspace)
            //    {
            //        password += info.KeyChar;
            //        info = Console.ReadKey(true);
            //    }
            //    else if (info.Key == ConsoleKey.Backspace)
            //    {
            //        if (!string.IsNullOrEmpty(password))
            //        {
            //            password = password.Substring
            //            (0, password.Length - 1);
            //        }
            //        info = Console.ReadKey(true);
            //    }
            //}
            //for (int i = 0; i < password.Length; i++)
            //    Console.Write("*");

            SecureString s = new SecureString();
            foreach (char c in password)
            {
                s.AppendChar(c);
            }

            return s;
        }
    }
}
