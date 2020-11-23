using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public class Tournament
    {
        public List<TournamentEntrant> RunTournament(List<TournamentEntrant> entrants, int targetScore)
        {
            int winnerScore = 0;
            Game game;
            GameResult result;

            while (winnerScore < targetScore)
            {
                game = new Game(GetPlayers(entrants));
                result = game.PlayGame();

                //Console.WriteLine(string.Format("{0} won and scored {1} points!", result.winnerName, result.pointsWon));

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

            //Console.WriteLine(string.Format("{0} won and scored {1} points!", result.winnerName, result.pointsWon));
            return entrants;
        }

        private List<Player> GetPlayers (List<TournamentEntrant> entrants)
        {
            List<Player> players = new List<Player>(entrants.Count);

            foreach (TournamentEntrant e in entrants)
            {
                players.Add(e.GetPlayer());
            }

            return players;
        }
    }
}
