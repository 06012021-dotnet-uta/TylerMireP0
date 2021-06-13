﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class Order
    {
        public Guid OrderId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime OrderCreationDate { get; set; }
        public DateTime LastOrderDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
    }
}
