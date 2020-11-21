using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public static class DeckInfo
    {
        private static Dictionary<Card, CardInfo> cards;

        public static Dictionary<Card, CardInfo> Cards { get => cards; }

        static DeckInfo()
        {
            cards = new Dictionary<Card, CardInfo>();

            CardColor[] colors = new CardColor[4]
            {
                CardColor.Blue, CardColor.Red, CardColor.Green, CardColor.Yellow
            };
            // Define deck

            foreach (CardColor color in colors)
            {
                cards[new Card(CardValue.Zero, color)] = new CardInfo(0, 1);
                cards[new Card(CardValue.One, color)] = new CardInfo(1, 2);
                cards[new Card(CardValue.Two, color)] = new CardInfo(2, 2);
                cards[new Card(CardValue.Three, color)] = new CardInfo(3, 2);
                cards[new Card(CardValue.Four, color)] = new CardInfo(4, 2);
                cards[new Card(CardValue.Five, color)] = new CardInfo(5, 2);
                cards[new Card(CardValue.Six, color)] = new CardInfo(6, 2);
                cards[new Card(CardValue.Seven, color)] = new CardInfo(7, 2);
                cards[new Card(CardValue.Eight, color)] = new CardInfo(8, 2);
                cards[new Card(CardValue.Nine, color)] = new CardInfo(9, 2);
                cards[new Card(CardValue.Skip, color)] = new CardInfo(20, 2);
                cards[new Card(CardValue.DrawTwo, color)] = new CardInfo(20, 2);
                cards[new Card(CardValue.Reverse, color)] = new CardInfo(20, 2);
            }

            cards[new Card(CardValue.Wild, CardColor.Wild)] = new CardInfo(50, 4, true);
            cards[new Card(CardValue.WildDrawFour, CardColor.Wild)] = new CardInfo(50, 4, true);

        }
    }
}
