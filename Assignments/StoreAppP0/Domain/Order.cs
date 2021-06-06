using System;
using System.Collections.Generic;

namespace Domain
{
    public class Order
    {
        public string id;
        public string customerId;
        public DateTime orderDate;
        public List<IPurchaseable> items;
    }
}