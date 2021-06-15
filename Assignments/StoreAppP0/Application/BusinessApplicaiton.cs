using Persistence;
using Application.Handler;
using System.Collections.Generic;
using System;
using Domain;
using System.Text;

namespace Application
{
    public class BusinessApplicaiton
    {
        private readonly LocationHandler _locationHandler;
        private readonly CustomerHandler _customerHandler;
        private readonly ProductHandler _productHandler;
        private readonly DataContext _context;

        public BusinessApplicaiton(DataContext context)
        {
            _locationHandler = new LocationHandler(context);
            _customerHandler = new CustomerHandler(context);
            _productHandler = new ProductHandler(context);
            _context = context;
        }

        #region //Customer login/register
        public bool RegisterCustomer(Customer customer, string password, out string response)
        {
            response = "";

            //Add password hash to customer
            customer.PasswordHash = GetPasswordHash(password);

            //Check to see if username is already taken
            List < Customer > DBCustomerList = _customerHandler.List();
            foreach(Customer c in DBCustomerList)
            {
                if(customer.Username == c.Username)
                {
                    response = "Username already in use.";
                    return false;
                }
            }

            try{
                _customerHandler.Create(customer);
                return true;
            }
            catch(Exception e)
            {
                response = e.Message;
                return false;
            }
        }

        private Byte[] GetPasswordHash(string password)
        {
            Byte[] passwordHash = new Byte[64];
            passwordHash = Encoding.ASCII.GetBytes(password);
            return passwordHash;
        }

        public Customer LoginCustomer(string username, string password, out string response)
        {
            response = "";

            List<Customer> DBCustomerList = _customerHandler.List();
            foreach(Customer c in DBCustomerList)
            {
                if(c.Username == username)
                {
                    string passwordHashA = BitConverter.ToString(GetPasswordHash(password));
                    string passwordHashB = BitConverter.ToString(c.PasswordHash).Substring(0, passwordHashA.Length);
                    if ( passwordHashA == passwordHashB)
                    {
                        return c;
                    }
                    else
                    {
                        response = "Incorrect password.";
                        return null;
                    }
                }
            }

            response = $"No account found for {username}";
            return null;
        }
        #endregion

        public List<Location> GetLocationList()
        {
            return _locationHandler.List();
        }

        public Location GetLocation(int locationIndex)
        {
            if (locationIndex < _locationHandler.List().Count && locationIndex >= 0)
                return _locationHandler.List()[locationIndex];
            else
                return null;
        }

        public List<LocationProductInventoryJunction> GetLocationProductList(Location location)
        {
            List<LocationProductInventoryJunction> locationProductInventories =
                _locationHandler.ListLocationInventory(location.LocationId);

            return locationProductInventories;
        }

        public Product GetProductDetails(Guid? productId)
        {
            if (productId != null)
            {
                return _productHandler.Read((Guid)productId);
            }
            else
                return null;
        }

        public bool Checkout(List<Order> orders)
        {
            _context.AddRange(orders);
            bool success = _context.SaveChanges() > 0;
            return success;
        }
    }
}
