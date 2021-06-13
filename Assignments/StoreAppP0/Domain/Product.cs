using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double? ProductPrice { get; set; }
    }
}
