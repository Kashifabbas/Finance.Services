using Ciklum.ATEA.Finance.Service.WebApi.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Ciklum.ATEA.Finance.Service.WebApi.Startup))]

namespace Ciklum.ATEA.Finance.Service.WebApi
{
    public class Startup
    {
        #region " Configuration Function "
        public void Configuration(IAppBuilder app)
        {
            // token generation
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
                {
                    // for demo purposes
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),

                    Provider = new ApplicationOAuthProvider()
                });

            // token consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        } 
        #endregion
    }
}