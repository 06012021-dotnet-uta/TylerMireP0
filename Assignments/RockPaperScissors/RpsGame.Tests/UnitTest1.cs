using System;
using RockPaperScissors1.RpsGame;
using Xunit;

namespace RpsGame.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            int x = 5;
            int y  = 6;

            //Act
            int z = x + y;

            //Assert
            Assert.Equal(11, z);
        }

        [Theory]
        public void TestingPlayRound()
        {
            //Arrange
            PlayerHuman playerHuman = new PlayerHuman("Test");

            //Act
            playerHuman.PlayRound(1);

            //Assert
            Assert.Equal(Move.Paper, playerHuman.currentMove);
        }
    }
}
