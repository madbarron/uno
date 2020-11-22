using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    sealed public class PlayerGameAction: GameAction
    {
        private PlayerGameAction(GameActionType type): base(type)
        {
        }

        private PlayerGameAction(GameActionType type, Card card): base(type)
        {
            this.card = card;
        }

        public static PlayerGameAction PlayCard(Card card)
        {
            return new PlayerGameAction(GameActionType.Play, card);
        }

        public static PlayerGameAction Draw()
        {
            return new PlayerGameAction(GameActionType.Draw);
        }
    }
}
