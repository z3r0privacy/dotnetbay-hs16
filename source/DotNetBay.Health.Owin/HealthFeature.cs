using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.Health.Owin
{
    public static class HealthFeature
    {
        public static IAppBuilder UseHealth(this IAppBuilder app)
        {
            return app.Use<HealthMiddleware>();
        }

        public static IAppBuilder UseHealth(this IAppBuilder app, string path)
        {
            return app.Map(path, app2 => app2.UseHealth());
        }
    }
}
