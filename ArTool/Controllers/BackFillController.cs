using System.Collections.Generic;
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

        public BackFillController(IBackfillManager mgr)
        {
            _manager = mgr;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public IActionResult Post([FromBody]BackFillRequest request)
        {
            return Ok(_manager.AddNewCreditCard(request));
           // return Ok("123");
        }


    }
}
