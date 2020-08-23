using ArTool.Models;

namespace ArTool.Helpers
{
    public interface IRequestorInformationHelper
    {
        RequestorInformation GetRequestorInformation(Microsoft.AspNetCore.Http.HttpContext httpContext);

    }
}
