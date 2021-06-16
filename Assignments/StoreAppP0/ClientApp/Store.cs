using Application;
using System;
using Persistence;
using System.Text;
using Domain;
using System.Collections.Generic;

namespace ClientApp
{
    public class Store
    {
        enum StoreState{
            Welcome,
            Login,
            ChooseLocation,
            ViewCart,
            ViewLocation,
            OrderOptions,
            EditCartOrder,
            OrderHistory,
            Checkout,
            Closed
        }

        private readonly BusinessApplicaiton _businessApplicaiton;

        private Customer loggedInCustomer;
        private Location selectedLocation;
        private Product selectedProduct;
        private LocationProductInventoryJunction selectedLocationProductDetails;
        private List<Order> cart;
        private Order selectedOrder;

        private StoreState storeState;

        public string userData;

        public Store(BusinessApplicaiton businessApplicaiton)
        {
            loggedInCustomer = null;
            selectedLocation = null;
            selectedProduct = null;
            cart = new List<Order>();
            storeState = StoreState.Welcome;
            _businessApplicaiton = businessApplicaiton;
        }

        private void welcome()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Welcome To the store application!");
            Console.WriteLine("---------------------------");
            storeState = StoreState.Login;
        }

        private void login()
        {
            do{
                string userResponse = "";
                Console.WriteLine("\n***************************");
                Console.WriteLine("           Login");
                Console.WriteLine("***************************");

                Console.Write("Please login or register a new account (L - login / R - register / Q - quit): ");
                userResponse = getMenuUserInput();

                switch (userResponse)
                {
                    case "L":
                        #region //Login code
                        string usernamePrompt;
                        string passwordPrompt;

                        do
                        {
                            string operationResponse;

                            Console.Write("Please enter username: ");
                            usernamePrompt = getUserInput();

                            Console.Write("Please enter password: ");
                            passwordPrompt = getUserInput();

                            //Attempt to log in user and output error message if unsuccessful
                            loggedInCustomer = _businessApplicaiton.LoginCustomer(usernamePrompt, passwordPrompt, out operationResponse);
                            if(operationResponse.Length > 0)
                            {
                                Console.WriteLine(operationResponse);
                            }
                        } while (loggedInCustomer == null);

                        Console.WriteLine($"\nWelcome back {loggedInCustomer.Username}");

                        if (loggedInCustomer.DefaultLocationId != null)
                        {
                            selectedLocation = _businessApplicaiton.GetLocation((Guid)loggedInCustomer.DefaultLocationId);
                            storeState = StoreState.ViewLocation;
                        }
                        else
                            storeState = StoreState.ChooseLocation;

                        break;
                    #endregion
                    case "R":
                        #region //Register code
                        string registerUsername = "";
                        string registerPassword = "";
                        string registerPasswordConfirm = "";
                        bool registrationSuccess;

                        do
                        {
                            registrationSuccess = true;

                            //Prompt user for new account info
                            Console.Write("Enter new account username: ");
                            registerUsername = getUserInput();
                            Console.Write("Enter new account password: ");
                            registerPassword = getUserInput();
                            Console.Write("Confirm new account password: ");
                            registerPasswordConfirm = getUserInput();

                            if (registerPassword.Length < 5)
                            {
                                Console.WriteLine("Invalid input. Password must be at least 5 characters.");
                                registrationSuccess = false;
                            }

                            if(registerPassword != registerPasswordConfirm)
                            {
                                Console.WriteLine("Invalid input. Passwords do not match");
                                registrationSuccess = false;
                            }

                            if(registrationSuccess)
                            {
                                string operationResponse = "";
                                registrationSuccess = _businessApplicaiton.RegisterCustomer(
                                    new Customer { Username = registerUsername },
                                    registerPassword,
                                    out operationResponse
                                );

                                if (!registrationSuccess)
                                    Console.WriteLine(operationResponse);

                            }

                        } while (!registrationSuccess);
                        

                        storeState = StoreState.ChooseLocation;
                        
                        break;
                        #endregion
                    case "Q":
                        storeState = StoreState.Closed;
                        break;
                    default:
                        Console.Write("Invalid entry.\n");
                        userResponse = "";
                        break;
                }

                
            }while(storeState == StoreState.Login);
        }

        private void chooseLocation()
        {
            string userSelection = "";

            Console.WriteLine("\n***************************");
            Console.WriteLine("   Locations/Cart/History");
            Console.WriteLine("***************************");
            List<Location> locations = _businessApplicaiton.GetLocationList();
            if (locations.Count > 0)
            {
                for (int i = 0; i < locations.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {locations[i].LocationName}");
                }
            }
            else
            {
                Console.WriteLine("No locations available.");
            }

            do 
            {
                
                Console.Write("\nPlease select location (Q - quit / C - view cart / H - order history / L - logout): ");
                userSelection = getMenuUserInput();

                switch (userSelection)
                {
                    case "Q":
                        storeState = StoreState.Closed;
                        break;
                    case "C":
                        storeState = StoreState.ViewCart;
                        break;
                    case "H":
                        storeState = StoreState.OrderHistory;
                        break;
                    case "L":
                        loggedInCustomer = null;
                        storeState = StoreState.Login;
                        break;
                    default:
                        bool selectionSuccess;
                        int index;
                        selectionSuccess = int.TryParse(userSelection, out index) && (index < locations.Count + 1 && index > 0);
                        if (selectionSuccess)
                        {
                            index -= 1;
                            selectedLocation = _businessApplicaiton.GetLocation(index);

                            storeState = StoreState.ViewLocation;
                        }
                        else
                            Console.WriteLine("Invalid input.");
                        break;
                }
            } while (storeState == StoreState.ChooseLocation);
        }

        private void orderHistory()
        {
            List<Order> customerOrders = _businessApplicaiton.GetCustomerHistory(loggedInCustomer.CustomerId);
            if (customerOrders != null)
            {
                foreach (Order o in customerOrders)
                {
                    Product p = _businessApplicaiton.GetProductDetails(o.ProductId);
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine($"Order item: {p.ProductName}");
                    Console.WriteLine($"Description: {p.ProductDescription}");
                    Console.WriteLine($"Total items ordered: {o.TotalItems}");
                    Console.WriteLine($"Total items cost: ${MathF.Round((float)o.Total, 2)}");
                    Console.WriteLine($"Order Date: {o.OrderCreationDate}");
                }

                do
                {
                    Console.Write("Enter B to go back: ");
                    string userInput = getMenuUserInput();
                    if (userInput == "B")
                        storeState = StoreState.ChooseLocation;
                    else
                        Console.WriteLine("Invalid input.");
                } while (storeState == StoreState.OrderHistory);
            }
            else
            {
                Console.WriteLine("No order history.");
                storeState = StoreState.ChooseLocation;
            }
        }

        //Print location inventory and prompt user for selection
        private void locationDetails()
        {

            Console.WriteLine($"\n********{selectedLocation.LocationName} Inventory********\n");
            
            List<LocationProductInventoryJunction> locationInventory =
                _businessApplicaiton.GetLocationProductList(selectedLocation.LocationId);

            for(int i = 0; i < locationInventory.Count; i++)
            {
                Product product = _businessApplicaiton.GetProductDetails(locationInventory[i].ProductId);
                Console.WriteLine($"{i + 1} - ({product.ProductName}) \n" +
                                  $"    Description: {product.ProductDescription}");
                if (locationInventory[i].TotalItems == 0)
                    Console.WriteLine("    Count: (Out of stock)\n");
                else
                    Console.WriteLine($"    Count: {locationInventory[i].TotalItems}\n");
            }

            do
            {
                Console.Write("Select a product or D to make this location your default (b to go back):");
                string userResponse = getMenuUserInput();

                switch (userResponse)
                {
                    case "B":
                        storeState = StoreState.ChooseLocation;
                        break;
                    case "Q":
                        storeState = StoreState.Closed;
                        break;
                    case "D":
                        loggedInCustomer.DefaultLocationId = selectedLocation.LocationId;
                        Console.WriteLine($"\n{selectedLocation.LocationName} is now your default.\n");
                        break;
                    default:
                        bool selectionSuccess;
                        int index;
                        selectionSuccess = int.TryParse(userResponse, out index) && (index < locationInventory.Count + 1 && index > 0);
                        if (selectionSuccess)
                        {
                            index -= 1;
                            selectedProduct = _businessApplicaiton.GetProductDetails(locationInventory[index].ProductId);
                            selectedLocationProductDetails = locationInventory[index];
                            storeState = StoreState.OrderOptions;
                        }
                        else
                            Console.WriteLine("Inavlid input.");
                        break;
                }
            } while (storeState == StoreState.ViewLocation);
        }

        private void orderOptions()
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Selection: {selectedProduct.ProductName}");
            Console.WriteLine($"Description: {selectedProduct.ProductDescription}");
            Console.WriteLine($"Inventory: {selectedLocationProductDetails.TotalItems}");
            Console.WriteLine($"Items per order: {selectedLocationProductDetails.ItemsPerOrder}");
            Console.WriteLine($"Item price: ${MathF.Round((float)selectedProduct.ProductPrice, 2)}");
            do
            {
                Console.Write("\nEnter order amount (b to go back / q to quit): ");
                string userResponse = getMenuUserInput();
                
                switch (userResponse)
                {
                    case "B":
                        storeState = StoreState.ChooseLocation;
                        break;
                    case "Q":
                        storeState = StoreState.Closed;
                        break;
                    default:
                        bool selectionSuccess;
                        int count;
                        selectionSuccess = int.TryParse(userResponse, out count) && 
                            (count <= selectedLocationProductDetails.TotalItems && count > 0);
                        if (selectionSuccess)
                        {
                            if(count * selectedLocationProductDetails.ItemsPerOrder <= selectedLocationProductDetails.TotalItems)
                            {
                                int totalItemsOrdered = (int)(count * selectedLocationProductDetails.ItemsPerOrder);
                                cart.Add(new Order
                                {
                                    LocationId = selectedLocation.LocationId,
                                    CustomerId = loggedInCustomer.CustomerId,
                                    TotalItems = totalItemsOrdered,
                                    Total = totalItemsOrdered * selectedProduct.ProductPrice,
                                    ProductId = selectedProduct.ProductId,
                                    Product = selectedProduct,
                                    Location = selectedLocation
                                }) ;

                                selectedLocationProductDetails.TotalItems -= totalItemsOrdered;

                                Console.WriteLine("\nOrder Placed. \n");
                                storeState = StoreState.ViewLocation;
                            }
                            else
                            {
                                Console.WriteLine("Not enough items in stock to complete order");
                            }
                        }
                        else
                            Console.WriteLine("Inavlid input.");
                        break;
                }
            } while (storeState == StoreState.OrderOptions);
        }

        private void viewCart()
        {
            Console.WriteLine("\n***************************");
            Console.WriteLine("           Cart");
            Console.WriteLine("***************************");

            for(int i = 0; i < cart.Count; i++)
            {
                Order o = cart[i];
                Console.WriteLine($"{i + 1} - {o.Product.ProductName} - {o.Location.LocationName} - {o.TotalItems} - ${MathF.Round((float)o.Total, 2)}");
            }

            do
            {
                string userResponse;
                Console.Write("\n Enter item number to edit or press C to checkout (Q - quit / B - Back): ");
                userResponse = getMenuUserInput();

                switch (userResponse)
                {
                    case "C":
                        checkout();
                        break;
                    case "B":
                        storeState = StoreState.ChooseLocation;
                        break;
                    case "Q":
                        storeState = StoreState.Closed;
                        break;
                    default:
                        bool selectionSuccess;
                        int index;
                        selectionSuccess = int.TryParse(userResponse, out index) &&
                            (index < cart.Count + 1 && index > 0);

                        if(selectionSuccess)
                        {
                            index -= 1;
                            selectedOrder = cart[index];
                            selectedLocation = _businessApplicaiton.GetLocation((Guid)selectedOrder.LocationId);
                            storeState = StoreState.EditCartOrder;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }

                        break;

                }
            } while (storeState == StoreState.ViewCart);
        }

        private void editCartOrder()
        {
            selectedProduct = _businessApplicaiton.GetProductDetails(selectedOrder.ProductId);
            selectedLocation = _businessApplicaiton.GetLocation((Guid)selectedOrder.LocationId);
            selectedLocationProductDetails = _businessApplicaiton.GetLocationProductDetails(selectedLocation.LocationId, selectedProduct.ProductId);

            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Selection: {selectedProduct.ProductName}");
            Console.WriteLine($"Description: {selectedProduct.ProductDescription}");
            Console.WriteLine($"Location: {selectedLocation.LocationName}");
            Console.WriteLine($"Orders: {selectedOrder.TotalItems / selectedLocationProductDetails.ItemsPerOrder}");
            Console.WriteLine($"Total items ordered: {selectedOrder.TotalItems}");
            Console.WriteLine($"Total: ${selectedLocationProductDetails.ItemsPerOrder}");
            
            //Add item edit options here
            do
            {
                string userResponse;
                Console.WriteLine("Enter C to change order count or D to delete order (B - Back): ");
                userResponse = getMenuUserInput();

                switch (userResponse)
                {
                    case "C":
                        Console.Write("Enter updated amount: ");
                        userResponse = getUserInput();

                        bool selectionSuccess;
                        int updatedCount;
                        int orderLocationItemSum = (int)(selectedOrder.TotalItems + selectedLocationProductDetails.TotalItems);
                        
                        selectionSuccess = int.TryParse(userResponse, out updatedCount) &&
                            (updatedCount * selectedLocationProductDetails.ItemsPerOrder < orderLocationItemSum 
                                && updatedCount >= 0);

                        if (selectionSuccess)
                        {
                            //Get new order item count
                            int newOrderItemCount = (int)(updatedCount * selectedLocationProductDetails.ItemsPerOrder);
                            //put the remainder in the store inventory total
                            selectedLocationProductDetails.TotalItems = orderLocationItemSum - newOrderItemCount;

                            selectedOrder.TotalItems = newOrderItemCount;
                        }
                        else
                            Console.WriteLine("Invalid input.");

                        break;
                    case "D":
                        cart.Remove(selectedOrder);
                        selectedLocationProductDetails.TotalItems += selectedOrder.TotalItems;
                        storeState = StoreState.ViewCart;
                        break;
                    case "B":
                        storeState = StoreState.ViewCart;
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
                selectedOrder = null;
            } while (storeState == StoreState.EditCartOrder);
        }

        private void checkout()
        {
            _businessApplicaiton.Checkout(cart);
            selectedLocation = null;
            selectedProduct = null;
            selectedLocationProductDetails = null;
            cart = new List<Order>();
            selectedOrder = null;
            storeState = StoreState.ChooseLocation;
            Console.WriteLine("Checked Out!");
        }

        //Grabs user input from console and formats for single letter responses
        private string getMenuUserInput()
        {
            string userResponse;
            userResponse = Console.ReadLine();
            if (userResponse != null)
            {
                userResponse = userResponse.Trim().ToUpper();
                return userResponse;
            }
            else return "";
        }

        private string getUserInput()
        {
            string userResponse;
            userResponse = Console.ReadLine();
            if (userResponse != null)
            {
                userResponse = userResponse.Trim();
                return userResponse;
            }
            else return "";
        }

        public void Run()
        {
            while(storeState != StoreState.Closed)
            {
                switch(storeState){
                    case StoreState.Welcome:
                        welcome();
                        break;
                    case StoreState.Login:
                        login();
                        break;
                    case StoreState.ChooseLocation:
                        chooseLocation();
                        break;
                    case StoreState.ViewLocation:
                        locationDetails();
                        break;
                    case StoreState.OrderOptions:
                        orderOptions();
                        break;
                    case StoreState.ViewCart:
                        viewCart();
                        break;
                    case StoreState.EditCartOrder:
                        editCartOrder();
                        break;
                    case StoreState.OrderHistory:
                        orderHistory();
                        break;
                    default:
                        throw new NotImplementedException();
                        //break;
                }
            }

            //Cleanup for quick quit
            selectedLocation = null;
            selectedProduct = null;
            selectedLocationProductDetails = null;
            cart = null;
            selectedOrder = null;
        }

    }
}
