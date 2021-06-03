using System;

namespace RockPaperScissors1.RpsGame
{
    public class PlayerHuman : Player
    {
        public string name;
        
        public PlayerHuman(string name) : base()
        {
            this.name = name;
        }

        public override void PlayRound(int round)
        {
            bool successfulConversion = false;
            int playerChoiceInt = 0;

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

            currentMove = (Move)playerChoiceInt;
        }
    }
}