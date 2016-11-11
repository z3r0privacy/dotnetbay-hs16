using System.Threading.Tasks;
using Microsoft.Owin;

namespace DotNetBay.Health.Owin
{
    public class HealthMiddleware : OwinMiddleware
    {
        public HealthMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            await this.Next.Invoke(context);
        }
    }
}
