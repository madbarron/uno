using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    internal class Deck
    {
        List<Card> cards;

        public bool Empty { get => cards.Count == 0; }

        public Deck()
        {
            cards = new List<Card>();
        }

        public Card Draw()
        {
            Card c = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);

            return c;
        }

        public void Shuffle(int randomSeed = 0)
        {
            Random generator;
            if (randomSeed == 0)
            {
                generator = new Random();
            }
            else
            {
                generator = new Random(randomSeed);
            }

            Card swapCard;
            int swapIndex;

            for(int i = 0; i < cards.Count; i++)
            {
                swapIndex = generator.Next(cards.Count);
                swapCard = cards[swapIndex];
                cards[swapIndex] = cards[i];
                cards[i] = swapCard;
            }
        }

        /// <summary>
        /// Add a card matching the given card to the deck
        /// </summary>
        /// <param name="card">Specification for the card to add</param>
        public void Add(Card card)
        {
            cards.Add(new Card(card));
        }

        /// <summary>
        /// Add a card matching the given card to the deck, a certain number of times
        /// </summary>
        /// <param name="card"></param>
        /// <param name="quantity"></param>
        protected void Add(Card card, int quantity)
        {
            for (int i = 0; i < quantity; i ++)
            {
                Add(card);
            }
        }

        /// <summary>
        /// Build the deck
        /// </summary>
        public void Populate()
        {
            cards.Clear();

            foreach (Card prototype in DeckInfo.Cards.Keys)
            {
                Add(prototype, DeckInfo.Cards[prototype].Quantity);
            }
        }
    }
}
