﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using DotNetBay.Health.Owin;

[assembly: OwinStartup(typeof(DotNetBay.SelfHost.Startup))]

namespace DotNetBay.SelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseHealth("/health");
        }
    }
}
