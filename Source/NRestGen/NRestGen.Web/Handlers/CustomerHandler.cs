using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NRestGen.Web.ResourceModel;

namespace NRestGen.Web.Handlers
{
    public class CustomerHandler : IRequestHandler<GetRequest<Customer>, Response<Customer>>
    {
        public Task<Response<Customer>> Handle(GetRequest<Customer> request, CancellationToken cancellationToken)
        {
            if (request.Identifier.ResourceId > 0)
            {
                return Task.FromResult(new Response<Customer>(CreateCustomer(request.Identifier.ResourceId)));
            }

            return Task.FromResult(new Response<Customer>(new[]
            {
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer()
            }));
        }

        private static Customer CreateCustomer(int? id = null)
        {
            var rnd = new Random();
            return new Customer
            {
                CustomerId = id ?? rnd.Next(1, 99999),
                Name = $"Customer{rnd.Next(99)}"
            };
        }
    }
}
