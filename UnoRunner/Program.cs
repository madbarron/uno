using System;
using System.Collections.Generic;
using Uno;

namespace UnoRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<Player> players = new List<Player>();
            players.Add(new BasicPlayer("Alice"));
            players.Add(new BasicPlayer("Bob"));
            players.Add(new BasicPlayer("Charlie"));

            Game game = new Game();
            game.debug = true;
            game.PlayGame(players);
        }
    }
}
