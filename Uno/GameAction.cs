using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public enum GameActionType { Draw, Play, ForceDraw, ShuffleDiscard, TopCard }

    public class GameAction
    {
        public int? Seat = null;
        public string Player;
        public GameActionType type;
        public Card card;

        public GameAction(GameActionType type)
        {
            this.type = type;
        }

        public GameAction(GameActionType type, int seat, string playerName)
        {
            this.type = type;
            this.Seat = seat;
            this.Player = playerName;
        }

        public override string ToString()
        {
            switch (type)
            {
                case GameActionType.Draw:
                    return string.Format("{0} drew a card.", Player);

                case GameActionType.Play:
                    return string.Format("{0} played a {1}.", Player, card);

                case GameActionType.ShuffleDiscard:
                    return "The discard pile is shuffled into a new draw deck.";

                case GameActionType.TopCard:
                    return string.Format("The top card is a {0}.", card);

                case GameActionType.ForceDraw:
                    return string.Format("{0} was forced to draw a card.", Player);

                default:
                    throw new NotImplementedException();
            }

        }
    }
}
