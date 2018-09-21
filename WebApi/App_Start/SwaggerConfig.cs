using System;
using System.Web.Http;
using Swashbuckle.Application;

namespace WebApi
{
    public class SwaggerConfig
    {
        /// <summary>
        /// Gets the XML comment path to be used by swagger when the solution is compiled
        /// </summary>
        /// <returns></returns>
        protected static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\WebApiSwagger.XML", AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>
        /// Register Swagger/Swashbuckle at startup
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "WebApi");
                        c.PrettyPrint();
                        c.IncludeXmlComments(GetXmlCommentsPath());
                    })
                .EnableSwaggerUi();
        }
    }
}
