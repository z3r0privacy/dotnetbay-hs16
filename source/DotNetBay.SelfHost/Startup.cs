using System;
using System.Threading.Tasks;
using DotNetBay.Health.Owin;
using Microsoft.Owin;
using Owin;
using System.Web;
using System.Web.Http;
using System.Reflection;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(DotNetBay.SelfHost.Startup))]

namespace DotNetBay.SelfHost
{
    public class Startup
    {

        // Make sure OWIN finds the Controller
        Type valuesControllerType = typeof(WebApi.StatusController);

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            var format = new JsonMediaTypeFormatter();
            //format.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            format.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Clear();
            config.Formatters.Add(format);

            app.UseWebApi(config);

            app.UseHealth("/health");


        }
    }
}
