using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphQLConsumer
{
    public class OrdersVm
    {
        public List<Order> Orders { get; set; }
        public int OrdersCount { get; set; }
    }
}