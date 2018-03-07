using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace WebApplication4_core.Controllers
{
    [Route("api/[controller]")]
    public class JiraController : Controller
    {
        private readonly ILogger _logger;

        public JiraController(ILogger<JiraController> logger)
        {
            _logger = logger;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromQuery(Name = "firstparameter")] int page, [FromBody]object value)
        {
            if(page==1)
            {
                throw new ApplicationException("This is a test error!!!!");
            }
            //string message = value.ToString();
            //_logger.LogInformation("in post !!!!!!");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
