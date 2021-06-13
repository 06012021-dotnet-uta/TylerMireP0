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
            Checkout,
            Closed
        }

        private readonly BusinessApplicaiton _businessApplicaiton;

        private Customer loggedInCustomer;
        private Location selectedLocation;

        private StoreState storeState;

        public string userData;

        public Store(BusinessApplicaiton businessApplicaiton)
        {
            loggedInCustomer = null;
            selectedLocation = null;
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

                            //Place login user function here
                            loggedInCustomer = _businessApplicaiton.LoginCustomer(usernamePrompt, passwordPrompt, out operationResponse);
                            if(operationResponse.Length > 0)
                            {
                                Console.WriteLine(operationResponse);
                            }

                        } while (loggedInCustomer == null);

                        Console.WriteLine($"Welcome back {loggedInCustomer.Username}");

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
                    case "N":
                        #region //Add location
                        
                        Console.Write("Enter new location: ");
                        string newLocation = Console.ReadLine();

                        //await mediator.Send(new Application.Locations.Add.Query{ locationName = newLocation});

                        
                        break;
                    #endregion
                    case "M":
                        #region
                        //var locations = await mediator.Send(new Application.Locations.List.Query());
                        Console.WriteLine();

                        

                        
                        
                        break;
                        #endregion
                    case "Q":
                        #region //Quit app
                        storeState = StoreState.Closed;
                        break;
                        #endregion
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
            Console.WriteLine("       Locations/Cart");
            Console.WriteLine("***************************");
            List<Location> locations = _businessApplicaiton.GetLocationList();
            if (locations.Count > 0)
            {
                for (int i = 0; i < locations.Count; i++)
                {
                    Console.WriteLine($"{i - 1} - {locations[i].LocationName}");
                }
            }
            else
            {
                Console.WriteLine("No locations available.");
            }

            do 
            {
                bool selectionSuccess;
                Console.Write("Please select location or enter 'c' to view your cart (q to quit): ");
                userSelection = getMenuUserInput();

                if(userSelection == "Q")
                {
                    storeState = StoreState.Closed;
                }
                else if(userSelection == "C")
                {
                    Console.WriteLine("CART GOES HERE");
                    // storeState = StoreState.ViewCart;
                }
                else
                {
                    int index;
                    selectionSuccess = int.TryParse(userSelection, out index) && index < locations.Count;
                    if (selectionSuccess)
                    {
                        selectedLocation = _businessApplicaiton.GetLocation(index);
                        Console.WriteLine(selectedLocation.LocationName);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }

            
            } while (selectedLocation == null && storeState != StoreState.Closed);
        }

        private void locationDetails()
        {

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
                }
            }
        }
    }
}
