using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System.Web.Configuration;
using Ciklum.ATEA.Finance.Service.Utilities.Logging;
using System.Web.Http.Cors;
using System.Web.Cors;

namespace Ciklum.ATEA.Finance.Service.WebApi.Providers
{
    
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        #region " Global Variable "
        private static IFinanceLogging lgLogging = FinanceLoggingFactory.GetLoggingHandler(FinanceLoggingFactory.LoggingType.NLog);
        String userADEmail = String.Empty;
        String userADName = String.Empty;
        String userADPhone = String.Empty;
        #endregion

        #region " GrantResourceOwnerCredentials Function "
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
           
            try
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                if (Utilities.ADAuthentication.AuthenticateUser.UserExist(context.UserName, ref userADEmail, ref userADName, ref userADPhone))
                {
                    Boolean isAuthorized = Utilities.ADAuthentication.AuthenticateUser.IsUserAuthorized(context.UserName, context.Password);
                    if (isAuthorized)
                    {
                        var id = new ClaimsIdentity("Embedded");
                        id.AddClaim(new Claim("sub", context.UserName));
                        id.AddClaim(new Claim("role", "user"));
                        context.Validated(id);
                        return Task.FromResult<int>(0);
                    }
                    else
                    {
                        context.SetError("The Password is incorrect.", "The password is incorrect.");
                        context.Rejected();
                        return Task.FromResult<int>(0);
                    }
                }
                else
                {
                    context.SetError("The user name is incorrect", "The user name is incorrect.");
                    context.Rejected();
                    return Task.FromResult<int>(0);
                }
            }
            catch (Exception ex)
            {
                lgLogging.LogFatalException(ex);
                if (ex.InnerException != null && ex.InnerException.ToString() != String.Empty)
                {
                    context.SetError("invalid_grant", "Problem occured while authenticating user. Problem: " + ex.InnerException.ToString());
                    context.Rejected();
                }
                else
                {
                    context.SetError("invalid_grant", "Problem occured while authenticating user. Problem: ");
                    context.Rejected();
                }
                return Task.FromResult<int>(0);

            }
            // create identity
        }
        #endregion

        #region " TokenEndpoint Function "
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {

            #region " Getting time to update service country and lease time "
            Int32 UpdateTimeInMinutes = 5;
            if (WebConfigurationManager.AppSettings["UpdateTimeInMinutes"] != null && WebConfigurationManager.AppSettings["UpdateTimeInMinutes"].Trim().Length > 0)
            {
                UpdateTimeInMinutes = Convert.ToInt32(WebConfigurationManager.AppSettings["UpdateTimeInMinutes"].Trim());
            }
            #endregion

            #region " Getting Token Expiry Time from Config file "
            Int32 days = 0;
            if (WebConfigurationManager.AppSettings["ExpiryDateTimeInDays"] != null && WebConfigurationManager.AppSettings["ExpiryDateTimeInDays"].Trim().Length > 0)
            {
                days = Convert.ToInt32(WebConfigurationManager.AppSettings["ExpiryDateTimeInDays"].Trim());
            } 
            #endregion

            #region " Assigning Token Expiry Time "
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                //.expires
                if (property.Key == ".expires")
                {
                    context.AdditionalResponseParameters.Add(property.Key, DateTime.Today.AddDays(days));
                }
                else if (property.Key == "expires_in")
                {
                    int seconds = days * 60 * 60;
                    context.AdditionalResponseParameters.Add(property.Key, seconds);
                }
                else
                {
                    context.AdditionalResponseParameters.Add(property.Key, property.Value);
                }

            } 
            #endregion

            #region " Writting values in Response object "
            context.AdditionalResponseParameters.Add("userName", userADName);
            context.AdditionalResponseParameters.Add("userEmail", userADEmail);
            context.AdditionalResponseParameters.Add("userPhone", userADPhone);
            context.AdditionalResponseParameters.Add("UpdateTimeInMinutes", UpdateTimeInMinutes); 
            #endregion
            return Task.FromResult<object>(null);
        }
        #endregion

        #region " ValidateClientAuthentication Function "
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
        #endregion
    }
}