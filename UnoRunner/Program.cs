using System;
using System.Collections.Generic;
using Uno;
using Uno.BasicPlayer;

namespace UnoRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's Uno!");

            List<TournamentEntrant> players = new List<TournamentEntrant>();
            players.Add(new TournamentEntrant("Alice", (n, s)=> new BasicPlayer(n, s)));
            players.Add(new TournamentEntrant("Bob", (n, s)=> new BasicPlayer(n, s)));
            players.Add(new TournamentEntrant("Charlie", (n, s)=> new BasicPlayer(n, s)));

            Tournament t = new Tournament();
            List<TournamentEntrant> records = t.RunTournament(players, 100000);

            records.Sort((l, r) => l.score - r.score);

            foreach(TournamentEntrant entrant in records)
            {
                Console.WriteLine(string.Format("{0} totaled {1} points", entrant, entrant.score.ToString("n0")));
            }
        }
    }
}
