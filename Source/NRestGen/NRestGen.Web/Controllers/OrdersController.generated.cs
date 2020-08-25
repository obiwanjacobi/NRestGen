// This code is generated by NRestGen v1.0.0.
// Any changes to this file will be overwritten when regenerated.
// Generated at 2020-08-25 11:52:26
using MediatR;
using NRestGen.Mediatr;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using NRestGen.Web.ResourceModel;

namespace NRestGen.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpHeadGet("{id?}")]
        public async Task<ActionResult<IQueryable<Order>>> Get(int? id = null)
        {
            var request = new GetRequest<Order>(id.GetValueOrDefault());
            var response = await _mediator.Send(request);
            return Ok(response.Collection.AsQueryable());
        }
        
        [HttpHeadGet("{id}/Orders")]
        public async Task<IActionResult> GetOrders(int id)
        {
            var request = new GetRequest<Order>()
            {
                ParentId = new ResourceIdentifier(typeof(Order), id)
            };
            var response = await _mediator.Send(request);
            return Ok(response.Collection);
        }
        
        [HttpHeadGet("{id}/OrderLines")]
        public async Task<IActionResult> GetOrderLines(int id)
        {
            var request = new GetRequest<OrderLine>()
            {
                ParentId = new ResourceIdentifier(typeof(Order), id)
            };
            var response = await _mediator.Send(request);
            return Ok(response.Collection);
        }
    }
}
