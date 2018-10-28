using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphQLConsumer
{
    public class CreateOrderVm
    {
        public List<Customer> Customers { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Shipper> Shippers { get; set; }
        public Order Order { get; set; }
    }
}