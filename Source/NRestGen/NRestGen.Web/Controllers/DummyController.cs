using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using NRestGen.Web.ResourceModel;

namespace NRestGen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        [HttpGet("customers")]
        public IActionResult Get(ODataQueryOptions<Customer> options)
        {
            return Ok();
        }
    }
}
