using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    class Player
    {
        protected List<Card> hand;
        protected int playerIndex;
        protected int playerCount;
        private string name;

        public int HandCount { get => hand.Count; }

        public Player(string name)
        {
            hand = new List<Card>();
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }

        public void Draw(Card card)
        {
            hand.Add(card);
        }

        /// <summary>
        /// </summary>
        /// <param name="card"></param>
        /// <returns>True if the player can play on the given card</returns>
        public bool CanPlayOn(Card card)
        {
            foreach (Card c in hand)
            {
                if (c.CanPlayOn(card))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Add up the points in my hand. If someone wins, they get these points.
        /// </summary>
        /// <returns></returns>
        public int HandScore()
        {
            int score = 0;

            foreach (Card c in hand)
            {
                score += c.Score;
            }

            return score;
        }

        /// <summary>
        /// The player always has the option to draw cards on their turn.
        /// The game will ask the player if they want to draw.
        /// Return true to draw a card. Return false to play a card.
        /// </summary>
        /// <param name="topCard"></param>
        /// <returns></returns>
        public virtual bool DrawOption(Card topCard)
        {
            throw new NotImplementedException();
        }

        public Card Play(Card topCard)
        {
            Card c = PickCardToPlay(topCard);

            if (hand.Contains(c))
            {
                hand.Remove(c);
            }
            else
            {
                throw new Exception("Tried to play a card that is not in hand");
            }

            return c;
        }

        /// <summary>
        /// Return the card that the player wants to play.
        /// Wild cards must be given a color.
        /// </summary>
        /// <param name="topCard"></param>
        /// <returns></returns>
        protected virtual Card PickCardToPlay(Card topCard)
        {
            throw new NotImplementedException();
        }

        public void OnPlayerDraw(int playerIndex)
        {
            // Do we want to keep track of this?
        }

        public void OnPlayerPlay(int playerIndex, Card card)
        {
            // Do we want to keep track of this?
        }
    }
}
