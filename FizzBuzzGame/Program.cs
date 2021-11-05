using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzBuzzGame
{
    public class FizzBuzz
    {
        public static void Main( string[] args)
        {
            Game game = new Game();

            for (int i = 1; i < 20; i++)
            {
                string s = game.PlayGame(i);
                Console.WriteLine(s);
            }
                
        }
    }
}
