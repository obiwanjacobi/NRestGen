using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using NRestGen.Web.ResourceModel;

namespace NRestGen.Web.Controllers
{
    /// <summary>
    /// Implementation of the Customer controller handlers.
    /// </summary>
    public partial class CustomersController : ControllerBase
    {
        public Task<Response<Customer>> Handle(Request<Customer> request)
        {
            throw new NotImplementedException();
        }
        
        public Task<Response<Order>> Handle(Request<Order> request)
        {
            throw new NotImplementedException();
        }
    }
}
