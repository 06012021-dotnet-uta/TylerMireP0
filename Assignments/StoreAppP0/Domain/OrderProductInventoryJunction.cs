using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class OrderProductInventoryJunction
    {
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? TotalItems { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
