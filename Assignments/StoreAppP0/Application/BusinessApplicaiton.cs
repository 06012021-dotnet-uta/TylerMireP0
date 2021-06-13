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
        private readonly DataContext _context;

        public BusinessApplicaiton(DataContext context)
        {
            _context = context;
            _locationHandler = new LocationHandler(context);
            _customerHandler = new CustomerHandler(context);
        }

        #region //Customer login/register
        public bool RegisterCustomer(Customer customer, string password, out string response)
        {
            bool creationSuccess = false;
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
                creationSuccess = _customerHandler.Create(customer);
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
                    if (GetPasswordHash(password).ToString() == c.PasswordHash.ToString())
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
            if (locationIndex < _locationHandler.List().Count)
                return _locationHandler.List()[locationIndex];
            else
                return null;
        }

        public List<Product> GetLocationProductList(Location location)
        {
            return new List<Product>();
        }

        public Product GetLocationProduct(Location location, int itemIndex)
        {
            return new Product();
        }

        public Order GetOrder(Location location, Product product, int count)
        {
            return new Order();
        }
    }
}
