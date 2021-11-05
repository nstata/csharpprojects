namespace FizzBuzzGame
{
    public class Game
    {
        public string PlayGame(int i)
        {
            if (i % 3 == 0 & i % 5 != 0)
            {
                return "Fizz";
            }

            else if (i % 5 == 0 & i % 3 != 0)
            {

                return "Buzz";
            }

            else if (i % 3 == 0 & i % 5 == 0)
            {

                return "FizzBuzz";
            }
            else
            {
                return i.ToString();
            }
        }
    }
}
