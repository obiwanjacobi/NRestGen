// This code is generated by NRestGen v1.0.0.
// Any changes to this file will be overwritten when regenerated.
// Generated at 2020-09-18 12:13:14
using MediatR;
using NRestGen.OData;
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
            //options.Validate(new ODataValidationSettings());
            var request = new GetRequest<Order>(options, id.GetValueOrDefault());
            var response = await _mediator.Send(request, cancellationToken);
            ResolveLinks(options, response);
            return Ok(response);
        }
        
        [HttpHeadGet("{id}/OrderLines")]
        public async Task<IActionResult> GetOrderLines(CancellationToken cancellationToken, ODataQueryOptions<OrderLine> options, int id)
        {
            //options.Validate(new ODataValidationSettings());
            var request = new GetRequest<OrderLine>(options)
            {
                ParentId = new ResourceIdentifier(typeof(Order), id),
            };
            var response = await _mediator.Send(request, cancellationToken);
            ResolveLinks(options, response);
            return Ok(response);
        }

        private void ResolveLinks<T>(ODataQueryOptions options, Response<T> response)
            where T : class
        {
            var resolver = new LinkTableResolver();
            response.Links = resolver.Resolve(Request, options, response.Links);
        }
    }
}
