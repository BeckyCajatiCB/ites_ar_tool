using System;
using Microsoft.Extensions.Configuration;

namespace ArTool
{
    public class StartupProduction : Startup
    {
        public StartupProduction(IConfiguration config, IServiceProvider provider) : base(config)
        {
        }
    }
}