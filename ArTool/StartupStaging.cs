using Microsoft.Extensions.Configuration;

namespace ArTool
{
    public class StartupStaging : Startup
    {
        public StartupStaging(IConfiguration config) : base(config)
        {
        }
    }
}