using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class LocationProductInventoryJunction
    {
        public Guid LocationId { get; set; }
        public Guid ProductId { get; set; }
        public double? SaleDiscount { get; set; }
        public int? ItemsPerOrder { get; set; }
        public int? TotalItems { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
    }
}
