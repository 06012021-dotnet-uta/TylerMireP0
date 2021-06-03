using System;
using System.Collections.Generic;

/*
    Assignment Goals RPS V1:
    X 1. Start game - intro
    X 2. Prompt for choice           - x3
    X 3. Press enter                 - x3
    X 4. Print result of each round  - x3
    X 5. Print the winner
    X 6. Prompt user for additional game
    X 7. Get Name from player and print to console for victory
    X 8. Track wins and print results screen 

    --Also do the coding challanges for this week--
*/

namespace RockPaperScissors1
{
    partial class Program
    {
        static RPSChoice GetUserChoice(int round)
        {
            bool successfulConversion = false;
            int playerChoiceInt = -1;

            while(successfulConversion == false)
            {
                string playerChoice;

                Console.WriteLine("\n------------------------------");
                Console.WriteLine($"Round {round}");
                Console.WriteLine("1-Rock, 2-Paper, 3-Scissors");
                Console.WriteLine("------------------------------");
                Console.Write("\nPlease enter choice: ");

                playerChoice = Console.ReadLine();
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

            return (RPSChoice)playerChoiceInt;
        }

        static bool DoesPlayerWin(RPSChoice cpu, RPSChoice player)
        {
            bool playerWin = false;

            switch(cpu)
            {
                case RPSChoice.Rock:
                    if(player == RPSChoice.Paper) playerWin = true;
                    else playerWin = false;
                    break;
                case RPSChoice.Paper:
                    if(player == RPSChoice.Scissors) playerWin = true;
                    else playerWin = false;
                    break;
                case RPSChoice.Scissors:
                    if(player == RPSChoice.Rock) playerWin = true;
                    else playerWin = false;
                    break;
            }

            return playerWin;
        }

        static void Main(string[] args)
        {
            List<bool> playerWins = new List<bool>(); //Records wins for each round
            Random rand = new Random(); //RNG
            int round = 1; //Records current round
            int playerWinTotal = 0; //Records total wins so winner can be determined
            bool gameIsRunning = true; //Game loop flag
            string playerName; //Player name
            
            Console.WriteLine("-----Welcome to rock-paper-scissors-----");
            Console.Write("\nPlease enter name: ");
            playerName = Console.ReadLine();

            
            while(gameIsRunning)
            {
                //Get choices from player and CPU
                RPSChoice playerChoice = GetUserChoice(round);
                RPSChoice cpuChoice = (RPSChoice)rand.Next(1, 4);
                
                //Rule out draw possibility
                while(cpuChoice == playerChoice)
                {
                    Console.WriteLine("Draw. Enter again");
                    cpuChoice = (RPSChoice)rand.Next(3);
                    playerChoice = GetUserChoice(round);
                }

                //Print cpu result
                Console.WriteLine($"CPU chose [{cpuChoice.ToString()}]");
                Console.WriteLine($"You chose [{playerChoice.ToString()}]");

                //Determine if player wins
                if(DoesPlayerWin(cpuChoice, playerChoice))
                {
                    Console.WriteLine($"You win round {round}!");
                    playerWinTotal++;
                    playerWins.Add(true);
                } 
                else
                {
                    Console.WriteLine($"You lost round {round}!");
                    playerWins.Add(false);
                } 

                //Increment round if less than 3 and prompt user to end game if 3 rounds were completed
                if(round < 3) round++;
                else
                {
                    string playAgainResponse = "";
                    string playerResults = "";
                    string cpuResults = "";

                    if(playerWinTotal > 1)
                        Console.WriteLine($"\n-----Congratulations! You won the game {playerName}!-----");
                    else
                        Console.WriteLine($"\n-----You lost the game {playerName}!-----");

                    //Prints win results into a table
                    #region 
                    Console.WriteLine("Round\t1\t2\t3");

                    playerResults += playerName + "\t";
                    cpuResults += "CPU\t";

                    for(int j = 0; j < playerWins.Count; j++)
                    {
                        if(playerWins[j] == true) playerResults += "Win\t";
                        else playerResults += "Lose\t";
                    }

                    for(int j = 0; j < playerWins.Count; j++)
                    {
                        if(playerWins[j] == false) cpuResults += "Win\t";
                        else cpuResults += "Lose\t";
                    }

                    Console.WriteLine(playerResults);
                    Console.WriteLine(cpuResults);
                    #endregion

                    while(playAgainResponse != "y" && playAgainResponse != "n")
                    {
                        if(playAgainResponse != "") Console.WriteLine($"Invalid answer. You entered {playAgainResponse}.");
                        Console.Write("\nPlay again? (y/n): ");
                        playAgainResponse = Console.ReadLine();
                    }

                    if(playAgainResponse == "y")
                    {
                        round = 1;
                        playerWins.Clear();
                    } 
                    else gameIsRunning = false;
                }
            }

            Console.WriteLine("\n-----Thank you for playing!-----");
        }
    }
}
