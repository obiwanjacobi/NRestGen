// This code is generated by NRestGen v1.0.0.
// Any changes to this file will be overwritten when regenerated.
// Generated at 2020-08-25 10:33:55
using System;
using System.Collections.Generic;

namespace NRestGen.Web.ResourceModel
{
    public sealed partial class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }

    public sealed partial class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
    }

    public sealed partial class OrderLine
    {
        public int OrderId { get; set; }
        public int OrderLineId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
