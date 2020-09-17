using System;
using System.Linq;
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

            var customers = new[]
            {
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer(),
                CreateCustomer()
            };

            // typically these vars will drive the (database) query.
            var skip = request.Options?.Skip?.Value ?? 0;
            var take = request.Options?.Top?.Value ?? Int32.MaxValue;
            var count = (request.Options?.Count?.Value).GetValueOrDefault() ? (int?)customers.Count() : null;

            var builder = new LinkTableBuilder()
                .AddGetSelf();

            return Task.FromResult(new Response<Customer>(customers.Skip(skip).Take(take), count)
            {
                Links = builder.ToList()
            });
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
