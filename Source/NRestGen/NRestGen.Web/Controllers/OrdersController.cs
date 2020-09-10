using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NRestGen.Web.ResourceModel;

namespace NRestGen.Web.Controllers
{
    /// <summary>
    /// Implementation of the Order controller handlers.
    /// </summary>
    public partial class OrdersController : ControllerBase
    {
        public Task<Response<Order>> Handle(Request<Order> request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<OrderLine>> Handle(Request<OrderLine> request)
        {
            throw new NotImplementedException();
        }
    }
}
