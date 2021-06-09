using Application.Location;
using MediatR;
using System;

namespace ClientApp
{
    public class Store
    {
        enum StoreState{
            Welcome,
            Login,
            ChooseLocation,
            ViewLocation,
            Checkout,
            Closed
        }

        private StoreState storeState;

        private readonly IMediator mediator;

        public string userData;

        public Store(IMediator mediator)
        {
            this.mediator = mediator;
            storeState = StoreState.Welcome;
        }

        private void welcome()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Welcome To the stuff store!");
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
                userResponse = getFormattedUserInput();

                switch (userResponse)
                {
                    case "L":
                        #region //Login code
                        bool successfulLogin = true;
                        string usernamePrompt;
                        string passwordPrompt;

                        do
                        {
                            Console.Write("Please enter username: ");
                            usernamePrompt = getFormattedUserInput();

                            Console.Write("Please enter password: ");
                            passwordPrompt = getFormattedUserInput();

                            //Place login user function here
                            //successfulLogin = mediator.send(new User.Login()

                        } while (successfulLogin == false);

                        storeState = StoreState.ChooseLocation;

                        break;
                    #endregion
                    case "R":
                        #region //Register code
                        string registerUsername = "";
                        string registerPassword = "";
                        string registerPasswordConfirm = "";

                        bool uniqueUsername = true;

                        do
                        {
                            Console.Write("Enter new account username: ");
                            registerUsername = getFormattedUserInput();

                            //Verify username is unique --> uniqueUsername = mediator.send(new UniqueUsername.Query(){username = registerUsername}

                        } while (uniqueUsername != true);

                        do
                        {
                            Console.Write("Enter new account password: ");
                            registerPassword = getFormattedUserInput();
                            Console.Write("Confirm new account password: ");
                            registerPasswordConfirm = getFormattedUserInput();

                            if (registerPassword.Length < 1 || registerPassword != registerPasswordConfirm)
                            {
                                Console.WriteLine("Invalid input.");
                            }

                        } while (registerPassword != registerPasswordConfirm && registerPassword.Length > 0);

                        storeState = StoreState.ChooseLocation;
                        
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
            Console.WriteLine("Location chosen!");
            storeState = StoreState.Closed;
        }

        //Grabs user input from console and formats for single letter responses
        private string getFormattedUserInput()
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
                }
            }
        }
    }
}
