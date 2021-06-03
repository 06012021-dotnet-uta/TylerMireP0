using System;

namespace RockPaperScissors1.RpsGame
{
    public class PlayerCPU : Player
    {
        Random random;
        //Might need it later?
        public PlayerCPU() : base()
        {
            random = new Random();
        }

        public override void PlayRound()
        {
            currentMove = (Move)random.Next(1, 4);
        }
    }
}