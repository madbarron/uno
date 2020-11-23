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

            List<TournamentEntrant> players = new List<TournamentEntrant>();
            players.Add(new TournamentEntrant("Alice", (n)=> new BasicPlayer(n)));
            players.Add(new TournamentEntrant("Bob", (n)=> new BasicPlayer(n)));
            players.Add(new TournamentEntrant("Charlie", (n)=> new BasicPlayer(n)));

            Tournament t = new Tournament();
            List<TournamentEntrant> records = t.RunTournament(players, 100000);

            records.Sort((l, r) => l.score - r.score);

            foreach(TournamentEntrant entrant in records)
            {
                Console.WriteLine(string.Format("{0} totaled {1} points", entrant, entrant.score.ToString("n0")));
            }

            //List<Player> players = new List<Player>();
            //players.Add(new BasicPlayer("Alice"));
            //players.Add(new BasicPlayer("Bob"));
            //players.Add(new BasicPlayer("Charlie"));

            //Game game = new Game(players);
            //game.debug = false;
            //GameResult result = game.PlayGame();

            //Console.WriteLine(string.Format("{0} won and scored {1} points!", result.winnerName, result.pointsWon));
        }
    }
}
