namespace RockPaperScissors1
{
    public class PlayerDerivedClass : PersonBaseClass
    {
        public PlayerDerivedClass(){}

        public PlayerDerivedClass(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }
}