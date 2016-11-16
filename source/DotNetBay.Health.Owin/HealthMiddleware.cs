using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;

namespace DotNetBay.Health.Owin
{
    class HealthMiddleware : OwinMiddleware
    {
        private EFMainRepository data;

        public HealthMiddleware(OwinMiddleware next) : base(next)
        {
            data = new EFMainRepository();
        }

        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);

            context.Response.Write(
                $"<h1>Health Information</h1>" +
                $"<h2>Database Connection String</h2>" +
                $"{data.Database.Connection.ConnectionString}" +
                $"<h2>Data Health</h2>" +
                $"Number of Auctions: {data.GetAuctions().ToList().Count}"
                );
        }
    }
}
