using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraphQLConsumer
{
    public class GraphQLQuery
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
    }
}