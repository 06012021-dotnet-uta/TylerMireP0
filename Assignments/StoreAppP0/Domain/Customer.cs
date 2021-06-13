using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public Guid CustomerId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public Guid? DefaultLocationId { get; set; }
        public DateTime AccountCreationDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
