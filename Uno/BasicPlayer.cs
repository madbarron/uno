using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    class BasicPlayer: Player
    {
        public BasicPlayer(string name) : base(name)
        {
            // Setup logic goes here.
        }

        public override bool DrawOption(Card topCard)
        {
            // I never want to draw a card unless I have to
            return false;
        }

        protected override Card PickCardToPlay(Card topCard)
        {
            // I'll play the first card that is legal
            Card choice = getFirstLegalCard(topCard);

            // Make sure we pick a color for wild cards
            if (DeckInfo.Cards[choice].IsWild)
            {
                return new Card(choice.Value, CardColor.Blue);
            }
            else
            {
                return choice;
            }
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

            throw new Exception("The game should not have asked me what card to play if I cannot play a card.");
        }
    }
}
