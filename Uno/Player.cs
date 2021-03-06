﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public class Player
    {
        protected List<Card> hand;
        protected int playerIndex;
        protected int playerCount;
        private string name;
        protected int seat;

        public int HandCount { get => hand.Count; }

        public Player(string name, int seatIndex)
        {
            this.name = name;
            this.seat = seatIndex;
            hand = new List<Card>();
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

        public PlayerGameAction Turn(Card topCard)
        {
            PlayerGameAction action = TakeTurn(topCard);

            if (action.type == GameActionType.Play)
            {
                // Make sure wild card is assigned a color
                if (action.card.Color == CardColor.Wild)
                {
                    throw new Exception("Must assign a color to a card before playing a wild card");
                }

                // Remove card from hand
                if (hand.Contains(action.card))
                {
                    hand.Remove(action.card);
                }
                else
                {
                    throw new Exception("Tried to play a card that is not in hand");
                }
            }

            return action;
        }

        protected virtual PlayerGameAction TakeTurn(Card topCard)
        {
            throw new NotImplementedException();
        }

        public virtual void OnGameAction(GameAction action)
        {
            throw new NotImplementedException();
        }
    }
}
