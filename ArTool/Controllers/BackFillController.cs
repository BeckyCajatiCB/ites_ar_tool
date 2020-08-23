using System.Collections.Generic;
using ArTool.Helpers;
using ArTool.Managers;
using ArTool.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArTool.Controllers
{
    [Route("api/[controller]")]
    public class BackFillController : Controller
    {
        private readonly IBackfillManager _manager;
        private readonly IRequestorInformationHelper _requestorInformationHelper;


        public BackFillController(IBackfillManager mgr, IRequestorInformationHelper helper)
        {
            _manager = mgr;
            _requestorInformationHelper = helper;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public IActionResult Post([FromBody]BackFillRequest request)
        {
            var paymentMethod = _manager.AddNewCreditCard(request,
                _requestorInformationHelper.GetRequestorInformation(HttpContext));

            if (paymentMethod == null)
                return UnprocessableEntity();

            return Ok(paymentMethod);
        }


    }
}
