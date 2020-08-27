using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
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
