using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NRestGen.Web.ResourceModel;

namespace NRestGen.Web.Controllers
{
    /// <summary>
    /// Implementation of the OrderLine controller handlers.
    /// </summary>
    public partial class OrderLinesController : ControllerBase
    {
        public Task<Response<OrderLine>> Handle(Request<OrderLine> request)
        {
            throw new NotImplementedException();
        }
    }
}
