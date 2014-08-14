using Ciklum.ATEA.Finance.Service.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciklum.ATEA.Finance.Service.Utilities.ADAuthentication
{
    public class AuthenticateUser
    {
        #region " Global Variable "
        // private static IExceptionLogger exceptionLogging = ExceptionLoggerFactory.Instance.GetEventViewerExceptionLogger();
        private static IFinanceLogging lgLogging = FinanceLoggingFactory.GetLoggingHandler(Logging.FinanceLoggingFactory.LoggingType.NLog);
        #endregion 

        #region " UserExist Function "
        public static bool UserExist(string username, ref String userADEmail, ref String userADName, ref String userADPhone)
        {
            lgLogging.LogInformation("IsUserExit starts here. Username is = " + username);
            bool IsExist = false;

            try
            {
                using (DirectoryEntry myLdapConnection = new DirectoryEntry())
                {
                    DirectorySearcher search = new DirectorySearcher(myLdapConnection);
                    search.Filter = "(cn=" + username + ")";
                    search.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + username + "))";
                    search.SearchScope = System.DirectoryServices.SearchScope.Subtree;
                    SearchResult result = search.FindOne();
                    userADEmail = String.Empty;
                    userADName = String.Empty;
                    userADPhone = String.Empty;


                    if (result != null)
                    {
                        #region " Getting User Email From AD "
                        if (result.Properties != null && result.Properties["mail"] != null && result.Properties["mail"].Count > 0)
                        {
                            userADEmail = result.Properties["mail"][0].ToString();
                        }
                       
                        #endregion

                        #region " Getting User Name From AD "
                        if (result.Properties != null && result.Properties["name"] != null && result.Properties["name"].Count > 0)
                        {
                            userADName = result.Properties["name"][0].ToString();
                        }
                        
                        #endregion
                        
                        #region " Getting User Phone From AD "
                        if (result.Properties != null && result.Properties["mobile"] != null && result.Properties["mobile"].Count > 0)
                        {
                            userADPhone = result.Properties["mobile"][0].ToString();
                        }
                        #endregion
                        IsExist = true;
                    }
                    else
                    {
                        IsExist = false;
                        
                    }
                    search.Dispose();
                }
            }

            catch (Exception ex)
            {
                IsExist = false;
                //exceptionLogging.Log(ex);
                //lgLogging.LogInformation("Exception occured: " + ex.ToString());
                lgLogging.LogFatalException(ex);
            }

            return IsExist;
        }
        #endregion

        #region " IsUserAuthorized Function "
        public static Boolean IsUserAuthorized(string username, string password)
        {
            lgLogging.LogInformation("IsUserAuthorized called");
            Boolean isAuthorized = false;
            try
            {
                System.DirectoryServices.ActiveDirectory.Domain domainName = Domain.GetCurrentDomain();
                string[] lst = domainName.Name.Split('.');
                domainName.Dispose();
                StringBuilder strDomain = new StringBuilder();
                foreach (var item in lst)
                {
                    strDomain.Append(@"DC=" + item + ",");
                }

                string str_dcName = "LDAP://" + strDomain.ToString().TrimEnd(',');

                using (DirectoryEntry myLdapConnection = new DirectoryEntry(str_dcName, username, password))
                {
                    DirectorySearcher search = new DirectorySearcher(myLdapConnection);
                    search.Filter = "(cn=" + username + ")";
                    search.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + username + "))";
                    search.SearchScope = System.DirectoryServices.SearchScope.Subtree;
                    SearchResult result = search.FindOne();
                    if (result != null)
                    {
                        isAuthorized = true;
                    }
                    else
                    {
                        isAuthorized = false;
                    }
                    result = null;
                    search.Dispose();
                }
            }
            catch (Exception ex)
            {
                isAuthorized = false;
                //exceptionLogging.Log(ex);
                //lgLogging.LogInformation("Exception occured in IsUserAuthorized function " + ex.ToString());
                lgLogging.LogFatalException(ex);
            }

            return isAuthorized;
        }
        #endregion
    }
}