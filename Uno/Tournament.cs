using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public class Tournament
    {
        private Random generator;

        public Tournament(int randomSeed = 0)
        {
            Random seedGenerator;

            if (randomSeed == 0)
            {
                seedGenerator = new Random();
                randomSeed = seedGenerator.Next();
            }

            Console.WriteLine(string.Format("Tournament Seed: {0}", randomSeed));

            generator = new Random(randomSeed);
        }

        public List<TournamentEntrant> RunTournament(List<TournamentEntrant> entrants, int targetScore)
        {
            int winnerScore = 0;
            Game game;
            GameResult result;

            while (winnerScore < targetScore)
            {
                game = new Game(GetPlayers(entrants), generator.Next());
                result = game.PlayGame();

                // Add up points for winner
                foreach (TournamentEntrant entrant in entrants)
                {
                    if (entrant.ToString() == result.winnerName)
                    {
                        entrant.score += result.pointsWon;
                        winnerScore = entrant.score;
                        break;
                    }
                }
            }

            return entrants;
        }

        /// <summary>
        /// Get a list of players in random order
        /// </summary>
        /// <param name="entrants"></param>
        /// <returns></returns>
        private List<Player> GetPlayers (List<TournamentEntrant> entrants)
        {
            List<Player> players = new List<Player>(entrants.Count);
            List<TournamentEntrant> toSeat = new List<TournamentEntrant>(entrants);
            int rand;

            for (int seat = 0; seat < entrants.Count; seat++)
            {
                rand = generator.Next(toSeat.Count);
                players.Add(toSeat[rand].GetPlayer(seat));
            }

            return players;
        }
    }
}
