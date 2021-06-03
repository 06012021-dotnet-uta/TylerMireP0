using System;
using System.Collections.Generic;

namespace RockPaperScissors1.RpsGame
{
    public class Game
    {
        const int TOTAL_ROUNDS = 3;
        private int currentRound;
        private bool isRunning;
        private List<Result> playerRoundResults;
        PlayerHuman playerHuman;
        PlayerCPU playerCPU;

        public Game()
        {
            currentRound = 1;
            isRunning = true;
            playerRoundResults = new List<Result>();
        }

        public void Init()
        {
            string playerName;

            Console.WriteLine("-----Welcome to rock-paper-scissors-----");
            Console.Write("\nPlease enter name: ");
            playerName = Console.ReadLine();

            playerHuman = new PlayerHuman(playerName);
            playerCPU = new PlayerCPU();
        }

        public void Play()
        {
            while(isRunning)
            {
                do{
                    //Get choices from player and CPU
                    playerHuman.PlayRound(currentRound);
                    playerCPU.PlayRound();

                    //Determine winner and add to scoreboard
                    Result playerHumanResult = playerHuman.DetermineVictory(playerCPU.currentMove);
                    playerRoundResults.Add(playerHumanResult);

                    //Print cpu result
                    Console.WriteLine($"CPU chose [{playerCPU.currentMove.ToString()}]");
                    Console.WriteLine($"You chose [{playerHuman.currentMove.ToString()}]");

                    if(playerHuman.currentMove == playerCPU.currentMove)
                        Console.WriteLine("--Draw! Play again!--");

                }while(playerHuman.currentMove == playerCPU.currentMove);



                #region 
                //Increment round if less than 3 and prompt user to end game if 3 rounds were completed
                if(currentRound < 3) currentRound++;
                else
                {
                    string playAgainResponse = "";
                    string playerResults = "";
                    string cpuResults = "";

                    if(playerHuman.totalWins > 1)
                        Console.WriteLine($"\n-----Congratulations! You won the game {playerHuman.name}!-----");
                    else
                        Console.WriteLine($"\n-----You lost the game {playerHuman.name}!-----");

                    //Prints win results into a table
                    #region 
                    Console.WriteLine("Round\t1\t2\t3");

                    playerResults += playerHuman.name + "\t";
                    cpuResults += "CPU\t";

                    for(int i = 0; i < playerRoundResults.Count; i++)
                    {
                        playerResults += $"{playerRoundResults[i].ToString()}\t";
                    }

                    for(int i = 0; i < playerRoundResults.Count; i++)
                    {
                        if(playerRoundResults[i] == Result.Win) cpuResults += "Lose\t";
                        else cpuResults += "Win\t";
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
                        currentRound = 1;
                        playerRoundResults.Clear();
                    } 
                    else isRunning = false;
                }
                #endregion
            }
        }

        public void Exit()
        {
            Console.WriteLine("\n-----Thank you for playing!-----");
        }

    }
}