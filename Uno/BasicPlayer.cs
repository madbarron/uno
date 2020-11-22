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

        protected override PlayerGameAction TakeTurn(Card topCard)
        {
            // Draw if I must draw
            if (!CanPlayOn(topCard))
            {
                return PlayerGameAction.Draw();
            }

            // I'll play the first card that is legal
            Card choice = getFirstLegalCard(topCard);

            // Make sure we pick a color for wild cards
            if (DeckInfo.Cards[choice].IsWild)
            {
                choice = new Card(choice.Value, CardColor.Blue);
            }

            return PlayerGameAction.PlayCard(choice);
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

        public override void OnGameAction(GameAction action)
        {
            switch (action.type)
            {
                case GameActionType.Draw:
                    // Somebody drew a card
                    break;

                case GameActionType.ForceDraw:
                    // Somebody had to draw a card because of Draw 2 or Draw 4 Wild
                    break;

                case GameActionType.Play:
                    // Do we care what somebody just played?
                    break;

                case GameActionType.ShuffleDiscard:
                    // The discard pile was shuffled into the draw deck.
                    break;

                case GameActionType.TopCard:
                    // The first card in the game was announced.
                    break;
            }
        }
    }
}
