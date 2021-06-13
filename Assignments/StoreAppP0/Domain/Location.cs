using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class Location
    {
        public Location()
        {
            Orders = new HashSet<Order>();
        }

        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
