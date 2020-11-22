using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public enum GameActionType { Draw, Play, ShuffleDiscard }

    public class GameAction
    {
        string player;
        //string description;
        public GameActionType type;
        public Card card;

        private GameAction(GameActionType type, string playerName)
        {
            this.player = playerName;
            this.type = type;
        }

        private GameAction(GameActionType type, string playerName, Card card)
        {
            this.player = playerName;
            this.type = type;
            this.card = card;
        }

        public static GameAction PlayCard(string playerName, Card card)
        {
            return new GameAction(GameActionType.Play, playerName, card);
        }

        public static GameAction Draw(string playerName)
        {
            return new GameAction(GameActionType.Draw, playerName);
        }

        public override string ToString()
        {
            switch (type)
            {
                case GameActionType.Draw:
                    return string.Format("{0} drew a card.", player);

                case GameActionType.Play:
                    return string.Format("{0} played a {1}.", player, card);

                case GameActionType.ShuffleDiscard:
                    return "The discard pile is shuffled into a new draw deck.";

                default:
                    throw new NotImplementedException();
            }

        }
    }
}
