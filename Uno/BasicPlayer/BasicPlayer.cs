using System;
using System.Collections.Generic;
using System.Text;

namespace Uno.BasicPlayer
{
    public class BasicPlayer: Player
    {
        public BasicPlayer(string name, int seat) : base(name, seat)
        {
            // Setup logic goes here.
        }

        /// <summary>
        /// Decide what to do on my turn.
        /// </summary>
        /// <param name="topCard"></param>
        /// <returns>PlayerGameAction with either a type of "Draw" or of "Play" plus which card to play</returns>
        protected override PlayerGameAction TakeTurn(Card topCard)
        {
            // Draw only if I must draw
            if (!CanPlayOn(topCard))
            {
                return PlayerGameAction.Draw();
            }

            // I'll play the first card that is legal
            Card choice = getFirstLegalCard(topCard);

            // Make sure we pick a color for wild cards
            if (GameInfo.Cards[choice].IsWild)
            {
                choice = new Card(choice.Value, CardColor.Blue);
            }

            return PlayerGameAction.PlayCard(choice);
        }

        /// <summary>
        /// Pick the first card in my hand that can be played on the given top card
        /// </summary>
        /// <param name="topCard"></param>
        /// <returns></returns>
        protected Card getFirstLegalCard(Card topCard)
        {
            foreach (Card card in hand)
            {
                if (card.CanPlayOn(topCard))
                {
                    return card;
                }
            }

            throw new Exception("I should not get here if I can play a card.");
        }

        /// <summary>
        /// Do internal bookkeeping when game events happen
        /// </summary>
        /// <param name="action"></param>
        public override void OnGameAction(GameAction action)
        {
            // Ignore actions that I did
            if (action.Seat == seat)
            {
                return;
            }

            switch (action.type)
            {
                case GameActionType.Draw:
                    // Somebody drew a card, either because they could not play on the top card
                    // or because they thought it was a good idea.
                    break;

                case GameActionType.ForceDraw:
                    // Somebody had to draw a card because of Draw 2 or Draw 4 Wild
                    break;

                case GameActionType.Play:
                    // Somebody just played a card. Do we need to keep track of that?
                    break;

                case GameActionType.ShuffleDiscard:
                    // The discard pile was shuffled to become the new draw deck.
                    // The last card played was not included.
                    break;

                case GameActionType.TopCard:
                    // The first face-up card in the game was announced.
                    break;
            }
        }
    }
}
