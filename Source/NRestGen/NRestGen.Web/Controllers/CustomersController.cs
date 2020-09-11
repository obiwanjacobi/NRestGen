using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NRestGen.Web.ResourceModel;

namespace NRestGen.Web.Controllers
{
    /// <summary>
    /// Implementation of the Customer controller handlers.
    /// </summary>
    public partial class CustomersController : ControllerBase
    {
        private Task<Response<Customer>> Handle(Request<Customer> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        private Task<Response<Order>> Handle(Request<Order> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
