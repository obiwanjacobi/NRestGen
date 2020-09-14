// This code is generated by NRestGen v1.0.0.
// Any changes to this file will be overwritten when regenerated.
// Generated at 2020-09-14 13:22:00
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NRestGen.Web.ResourceModel;

namespace NRestGen.Web.Controllers
{
    /// <summary>
    /// Handles resource requests for the Order type.
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    //[Route("api/v{v:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public partial class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpHeadGet("{id?}")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken, ODataQueryOptions<Order> options, int? id = null)
        {
            var request = new GetRequest<Order>(options, id.GetValueOrDefault())
            {
                Context = NewRequestContext()
            };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        
        [HttpHeadGet("{id}/OrderLines")]
        public async Task<IActionResult> GetOrderLines(CancellationToken cancellationToken, ODataQueryOptions<OrderLine> options, int id)
        {
            var request = new GetRequest<OrderLine>(options)
            {
                ParentId = new ResourceIdentifier(typeof(Order), id),
                Context = NewRequestContext()
            };
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response.Results);
        }

        private RequestContext NewRequestContext()
        {
            return new RequestContext(Request.Method, Request.Path, Request.QueryString.Value);
        }
    }
}
