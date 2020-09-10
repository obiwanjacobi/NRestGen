using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
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
