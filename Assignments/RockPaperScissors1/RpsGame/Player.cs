using System;
using System.Collections.Generic;

namespace RockPaperScissors1.RpsGame
{
    public class Player
    {
        public int totalWins;
        public Move currentMove;

        public Player()
        {
            totalWins = 0;
            currentMove = Move.Oops; //For debugging
        }

        public virtual void PlayRound()
        {
        }

        public virtual void PlayRound(int round)
        {
        }

        public Result DetermineVictory(Move opponentMove)
        {
            bool winA;

            if(opponentMove == currentMove) return Result.Tie;

            switch(opponentMove)
            {
                case Move.Rock:
                    if(currentMove == Move.Paper) winA = true;
                    else winA = false;
                    break;
                case Move.Paper:
                    if(currentMove == Move.Scissors) winA = true;
                    else winA = false;
                    break;
                case Move.Scissors:
                    if(currentMove == Move.Rock) winA = true;
                    else winA = false;
                    break;
                default:
                    winA = false;
                    break;
            } 
            
            if(winA)
            { 
                totalWins++;
                return Result.Win;
            }
            else return Result.Lose;
        }
    }
}