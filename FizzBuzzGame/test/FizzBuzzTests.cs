using NUnit.Framework;


namespace FizzBuzzGame.Tests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [TestCase (3, "Fizz")]
        [TestCase( 1, "1")]
        [TestCase( 5, "Buzz")]
        [TestCase( 15, "FizzBuzz")]
        public void TestFizzBuzzPositiveCases( int inputNumber, string expected )
        {
            Game game = new Game();
            string actual = game.PlayGame(inputNumber);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(3, "Fizz")]
        [TestCase(1, "2")]
        [TestCase(-1, "1")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        public void TestFizzBuzzNegativeCases(int inputNumber, string expected)
        {
            Game game = new Game();
            string actual = game.PlayGame(inputNumber);
            Assert.AreNotEqual(expected, actual);
        }
    }
}
