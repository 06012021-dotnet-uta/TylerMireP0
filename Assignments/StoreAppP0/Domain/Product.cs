using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class Product
    {
        public Product()
        {
            LocationProductInventoryJunctions = new HashSet<LocationProductInventoryJunction>();
            Orders = new HashSet<Order>();
        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double? ProductPrice { get; set; }

        public virtual ICollection<LocationProductInventoryJunction> LocationProductInventoryJunctions { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
