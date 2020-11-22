using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public class BasicPlayer: Player
    {
        public BasicPlayer(string name) : base(name)
        {
            // Setup logic goes here.
        }

        protected override GameAction TakeTurn(Card topCard)
        {
            // Draw if I must draw
            if (!CanPlayOn(topCard))
            {
                return GameAction.Draw(ToString());
            }

            // I'll play the first card that is legal
            Card choice = getFirstLegalCard(topCard);

            // Make sure we pick a color for wild cards
            if (DeckInfo.Cards[choice].IsWild)
            {
                choice = new Card(choice.Value, CardColor.Blue);
            }

            return GameAction.PlayCard(ToString(), choice);
        }

        protected Card getFirstLegalCard(Card topCard)
        {
            foreach (Card card in hand)
            {
                if (card.CanPlayOn(topCard))
                {
                    return card;
                }
            }

            throw new Exception("I should not get here if I cannot play a card.");
        }
    }
}
