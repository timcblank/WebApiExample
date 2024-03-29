﻿using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// Initial startup/entry of the web api
        /// </summary>
        protected void Application_Start()
        {
            AutofacConfig.Initialize();
            SwaggerConfig.Register();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
