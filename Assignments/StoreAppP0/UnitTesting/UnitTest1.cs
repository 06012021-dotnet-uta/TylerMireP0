using Microsoft.EntityFrameworkCore;
using Persistence;
using Application;
using Xunit;
using Domain;
using System.Linq;
using System;
using System.Collections.Generic;

namespace UnitTesting
{
    public class UnitTest1
    {

        
        
        DbContextOptions<DataContext> options;

        public UnitTest1()
        {
            options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RegisterCustomerTest")
                .Options;
        }

        [Fact]
        public void RegisterCustomerTest()
        {
            using(var context = new DataContext(options))
            {
                //Arrange
                var businessApplication = new BusinessApplicaiton(context);
                var newCustomer = new Customer
                {
                    Username = "testusername",
                    FirstName = "test",
                    LastName = "testLast",
                    Email = "test@test.com",
                    AddressStreet = "123 street",
                    AddressCity = "test town",
                    AddressState = "EX"
                };

                //Act
                string response;
                businessApplication.RegisterCustomer(newCustomer, "testPassword", out response);

                //Assert
                Assert.NotNull(context.Customers.ToList()[0]);
                Assert.Equal(newCustomer.Username, context.Customers.ToList()[0].Username);
            }
        }

        [Fact]
        public void LoginCustomerTest()
        {
            
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RegisterCustomerTest")
                .Options;

            using(var context = new DataContext(options))
            {
                //Arrange
                var businessApplication = new BusinessApplicaiton(context);
                var newCustomer = new Customer
                {
                    Username = "testusername",
                    FirstName = "test",
                    LastName = "testLast",
                    Email = "test@test.com",
                    AddressStreet = "123 street",
                    AddressCity = "test town",
                    AddressState = "EX"
                };
                string response;
                businessApplication.RegisterCustomer(newCustomer, "testPassword", out response);

                //Act
                Customer c = businessApplication.LoginCustomer(newCustomer.Username, "testPassword", out response);

                //Assert
                Assert.NotNull(c);
                Assert.Equal("", response);
                Assert.Equal(newCustomer.Username, c.Username);
            }
        }

        [Fact]
        public void GetCustomerHistoryTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RegisterCustomerTest")
                .Options;

            using (var context = new DataContext(options))
            {
                var businessApp = new BusinessApplicaiton(context);
                Guid customerId = Guid.NewGuid();
                //Arrange
                context.Add<Order>(new Order()
                {
                    LocationId = Guid.NewGuid(),
                    CustomerId = customerId,
                    TotalItems = 10,
                    ProductId = Guid.NewGuid()
                });
                context.Add<Order>(new Order()
                {
                    LocationId = Guid.NewGuid(),
                    CustomerId = customerId,
                    TotalItems = 10,
                    ProductId = Guid.NewGuid()
                });
                context.Add<Order>(new Order()
                {
                    LocationId = Guid.NewGuid(),
                    CustomerId = customerId,
                    TotalItems = 10,
                    ProductId = Guid.NewGuid()
                });

                context.SaveChanges();

                //Act
                var orders = businessApp.GetCustomerHistory(customerId);

                //Assert
                Assert.NotNull(orders);
                Assert.Equal(customerId, orders[0].CustomerId);
            }
        }

        [Fact]
        public void CheckoutTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RegisterCustomerTest")
                .Options;

            using (var context = new DataContext(options))
            {
                //Arrange
                var businessApp = new BusinessApplicaiton(context);
                Guid customerId = Guid.NewGuid();
                
                List<Order> orders = new List<Order>()
                {
                    new Order()
                    {
                        LocationId = Guid.NewGuid(),
                        CustomerId = customerId,
                        TotalItems = 10,
                        ProductId = Guid.NewGuid()
                    },
                    new Order()
                    {
                        LocationId = Guid.NewGuid(),
                        CustomerId = customerId,
                        TotalItems = 10,
                        ProductId = Guid.NewGuid()
                    },
                    new Order()
                    {
                        LocationId = Guid.NewGuid(),
                        CustomerId = customerId,
                        TotalItems = 10,
                        ProductId = Guid.NewGuid()
                    }
                };

                //Act
                bool success = businessApp.Checkout(orders);

                //Assert
                Assert.True(success);
            }
        }

        [Fact]
        public void GetProductDetailsTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RegisterCustomerTest")
                .Options;

            using (var context = new DataContext(options))
            {
                //Arrange
                var businessApp = new BusinessApplicaiton(context);
                Guid testId = Guid.NewGuid();


                context.Add(new Product()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "testing name 1",
                    ProductDescription = "testing description 1"
                });
                context.Add(new Product()
                {
                    ProductId = testId,
                    ProductName = "testing name 2",
                    ProductDescription = "testing description 2"
                });
                context.Add(new Product()
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "testing name 3",
                    ProductDescription = "testing description 3"
                });
                

                
                context.SaveChanges();

                //Act
                var product = businessApp.GetProductDetails(testId);

                //Assert
                Assert.Equal(testId, product.ProductId);

            }
        }

        [Fact]
        public void GetLocationProductListTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RegisterCustomerTest")
                .Options;

            using (var context = new DataContext(options))
            {
                //Arrange
                var businessApp = new BusinessApplicaiton(context);
                Guid location1 = Guid.NewGuid();
                Guid location2 = Guid.NewGuid();
                Guid location3 = Guid.NewGuid();

                context.Add(new LocationProductInventoryJunction()
                {
                    LocationId = location1,
                    ProductId = Guid.NewGuid()
                });
                context.Add(new LocationProductInventoryJunction()
                {
                    LocationId = location2,
                    ProductId = Guid.NewGuid()
                });
                context.Add(new LocationProductInventoryJunction()
                {
                    LocationId = location2,
                    ProductId = Guid.NewGuid()
                });
                context.Add(new LocationProductInventoryJunction()
                {
                    LocationId = location3,
                    ProductId = Guid.NewGuid()
                });

                context.SaveChanges();

                //Act
                var locationProducts = businessApp.GetLocationProductList(location2);

                //Assert
                Assert.Equal(location2, locationProducts[0].LocationId);
                Assert.Equal(location2, locationProducts[1].LocationId);
            }
        }

        [Fact]
        public void GetLocationProductDetailsTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RegisterCustomerTest")
                .Options;

            using (var context = new DataContext(options))
            {
                //Arrange
                var businessApp = new BusinessApplicaiton(context);
                Guid location1 = Guid.NewGuid();
                Guid location2 = Guid.NewGuid();
                Guid location3 = Guid.NewGuid();
                Guid productId = Guid.NewGuid();

                context.Add(new LocationProductInventoryJunction()
                {
                    LocationId = location1,
                    ProductId = Guid.NewGuid()
                });
                context.Add(new LocationProductInventoryJunction()
                {
                    LocationId = location2,
                    ProductId = Guid.NewGuid()
                });
                context.Add(new LocationProductInventoryJunction()
                {
                    LocationId = location2,
                    ProductId = productId
                });
                context.Add(new LocationProductInventoryJunction()
                {
                    LocationId = location3,
                    ProductId = Guid.NewGuid()
                });

                context.SaveChanges();

                //Act
                var locationProduct = businessApp.GetLocationProductDetails(location2, productId);

                //Assert
                Assert.NotNull(locationProduct);
                Assert.Equal(locationProduct.LocationId, location2);
                Assert.Equal(locationProduct.ProductId, productId);
            }
        }

        [Fact]
        public void GetLocationTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RegisterCustomerTest")
                .Options;

            using (var context = new DataContext(options))
            {
                //Arrange
                var businessApp = new BusinessApplicaiton(context);
                Guid locationId = Guid.NewGuid();

                context.Add(new Location()
                {
                    LocationId = locationId
                });
                context.Add(new Location()
                {
                    LocationId = Guid.NewGuid()
                });
                context.Add(new Location()
                {
                    LocationId = Guid.NewGuid()
                });
                context.Add(new Location()
                {
                    LocationId = Guid.NewGuid()
                });

                context.SaveChanges();

                //Act
                var location = businessApp.GetLocation(locationId);

                //Assert
                Assert.NotNull(location);
                Assert.Equal(location.LocationId, locationId);
            }
        }

        [Fact]
        public void GetLocationListTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "RegisterCustomerTest")
                .Options;

            using (var context = new DataContext(options))
            {
                //Arrange
                var businessApp = new BusinessApplicaiton(context);

                context.Add(new Location()
                {
                    LocationId = Guid.NewGuid()
                });
                context.Add(new Location()
                {
                    LocationId = Guid.NewGuid()
                });
                context.Add(new Location()
                {
                    LocationId = Guid.NewGuid()
                });
                context.Add(new Location()
                {
                    LocationId = Guid.NewGuid()
                });

                context.SaveChanges();

                //Act
                var locations = businessApp.GetLocationList();

                //Assert
                Assert.NotNull(locations);
                Assert.Equal(8, locations.Count); //Dont know if I can clear out old location list so we're gonna do 8 here
            }
        }
    }
}
