// This code is generated by NRestGen v1.0.0.
// Any changes to this file will be overwritten when regenerated.
// Generated at 2020-09-10 12:51:04
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using NRestGen.Web.ResourceModel;

namespace NRestGen.Web.Controllers
{
    /// <summary>
    /// Handles resource requests for the OrderLine type.
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    //[Route("api/v{v:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public partial class OrderLinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderLinesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpHeadGet("{id?}")]
        public async Task<IActionResult> Get(ODataQueryOptions<OrderLine> options, int? id = null)
        {
            var request = new GetRequest<OrderLine>(options, id.GetValueOrDefault());
            var response = await _mediator.Send(request);
            return Ok(response.Results);
        }
    }
}
