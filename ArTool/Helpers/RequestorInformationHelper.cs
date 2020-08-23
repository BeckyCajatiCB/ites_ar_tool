using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArTool.Models;
using HttpContext.Extensions.Core;

namespace ArTool.Helpers
{
    public class RequestorInformationHelper : IRequestorInformationHelper
    {
        public RequestorInformation GetRequestorInformation(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            return new RequestorInformation
            {
                ImpersonatedUserId = httpContext.GetImpersonatedUserId(),
                RequestId = httpContext.GetRequestId(),
                UserId = httpContext.GetUserId() ?? "unknown"
            };
        }
    }
}
