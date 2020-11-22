using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public class GameResult
    {
        public PlayerGameResult[] seats;
        public int winnerSeat;
        public string winnerName;
        public int pointsWon;

        public List<GameAction> actions;

        public GameResult(List<Player> players)
        {
            actions = new List<GameAction>();

            seats = new PlayerGameResult[players.Count];

            for(int i = 0; i < players.Count; i++)
            {
                seats[i] = new PlayerGameResult();
                seats[i].name = players[i].ToString();
            }
        }
    }
}
