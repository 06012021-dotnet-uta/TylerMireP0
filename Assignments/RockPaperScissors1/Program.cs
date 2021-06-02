using System;

/*
    Assignment Goals RPS V1:
    1. Start game - intro
    2. Prompt for choice           - x3
    3. Press enter                 - x3
    4. Print result of each round  - x3
    5. Print the winner
    6. Prompt user for additional game
    7. Get Name from player and print to console for victory
    8. Track wins and print results screen 

    --Also do the coding challange for this week--
*/

namespace RockPaperScissors1
{
    enum Move{
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }
    class Program
    {
        static Move GetUserChoice()
        {
            bool successfulConversion = false;
            int playerChoiceInt = -1;

            while(successfulConversion == false)
            {
                Console.WriteLine("1-Rock, 2-Paper, 3-Scissors");
                Console.WriteLine("Please enter choice:");

                string playerChoice = Console.ReadLine();
                successfulConversion = int.TryParse(playerChoice, out playerChoiceInt);

                //Check to see if input is within enum range
                if(playerChoiceInt > 3 || playerChoiceInt < 1)
                {
                    successfulConversion = false;
                }
                
                //Notify user that input was not valid
                if(successfulConversion == false)
                {
                    Console.WriteLine($"Invalid answer. You entered {playerChoice}.");
                }
            }

            return (Move)playerChoiceInt;
        }

        static bool DoesPlayerWin(Move cpu, Move player)
        {
            bool playerWin = false;

            switch(cpu)
            {
                case Move.Rock:
                    if(player == Move.Paper) playerWin = true;
                    else playerWin = false;
                    break;
                case Move.Paper:
                    if(player == Move.Scissors) playerWin = true;
                    else playerWin = false;
                    break;
                case Move.Scissors:
                    if(player == Move.Rock) playerWin = true;
                    else playerWin = false;
                    break;
            }

            return playerWin;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to rock-paper-scissors");

            Random rand = new Random();
            
            int round = 1;
            int playerWinTotal = 0;

            bool gameIsRunning = true;
            while(gameIsRunning)
            {
                //Get choices from player and CPU
                Move playerChoice = GetUserChoice();
                Move cpuChoice = (Move)rand.Next(0, 4);
                
                //Rule out draw possibility
                while(cpuChoice == playerChoice)
                {
                    Console.WriteLine("Draw. Enter again");
                    cpuChoice = (Move)rand.Next(3);
                    playerChoice = GetUserChoice();
                }

                //Determine if player wins
                if(DoesPlayerWin(cpuChoice, playerChoice))
                {
                    Console.WriteLine($"You win round {round}!");
                    playerWinTotal++;
                } 
                else Console.WriteLine($"You lost round {round}!");

                //Increment round if less than 3 and prompt user to end game if 3 rounds were completed
                if(round < 3) round++;
                else
                {
                    if(playerWinTotal > 2)
                        Console.WriteLine("-----You won the game!-----");
                    else
                        Console.WriteLine("-----You lost the game!-----");

                    string playAgainResponse = "";
                    Console.WriteLine(playAgainResponse.Length);

                    while(!String.Equals(playAgainResponse.ToUpper(), "Y") || !String.Equals(playAgainResponse.ToUpper(), "N"))
                    {
                        if(playAgainResponse != "") Console.WriteLine($"Invalid answer. You entered {playAgainResponse}.");
                        Console.WriteLine("Play again? (y/n):");
                        playAgainResponse = Console.ReadLine();
                    }

                    if(playAgainResponse == "y") round = 1;
                    else gameIsRunning = false;
                }
            }

            Console.WriteLine("Thank you for playing!");
        }
    }
}
