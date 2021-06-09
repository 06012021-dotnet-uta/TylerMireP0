using System;
using System.Collections.Generic;
using RockPaperScissors1.RpsGame;

namespace RockPaperScissors1
{
    partial class Program
    {

        static void Main(string[] args)
        {
            Game game = new Game();

            game.Init();
            game.Play();
            game.Exit();
        }
    }
}
