using System;
using System.Collections.Generic;

namespace Uno
{
    public enum CardValue
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        DrawTwo,
        Skip,
        Reverse,
        Wild,
        WildDrawFour
    }

    public enum CardColor
    {
        Red,
        Yellow,
        Green,
        Blue,
        Wild
    }

    public class Card: IEquatable<Card>
    {
        private CardValue value;
        private CardColor color;
        private readonly Dictionary<CardValue, int> cardScores = new Dictionary<CardValue, int>()
        {
            {CardValue.Zero, 0 },
            {CardValue.One, 1 },
            {CardValue.Two, 2 },
            {CardValue.Three, 3 },
            {CardValue.Four, 4 },
            {CardValue.Five, 5 },
            {CardValue.Six, 6 },
            {CardValue.Seven, 7 },
            {CardValue.Eight, 8 },
            {CardValue.Nine, 9 },

            {CardValue.Skip, 20 },
            {CardValue.DrawTwo, 20 },
            {CardValue.Reverse, 20 },
            {CardValue.Wild, 50 },
            {CardValue.WildDrawFour, 50 }
        };

        private readonly Dictionary<CardValue, int> cardQuantities = new Dictionary<CardValue, int>()
        {
            {CardValue.Zero, 1 },
            {CardValue.One, 2 },
            {CardValue.Two, 2 },
            {CardValue.Three, 2 },
            {CardValue.DrawTwo, 2 },
            {CardValue.Reverse, 2 },
            {CardValue.Wild, 1 },
            {CardValue.WildDrawFour, 1 }
        };

        public CardValue Value { get => value; set => this.value = value; }
        public CardColor Color { get => color; set => color = value; }

        public Card(CardValue value, CardColor color)
        {
            this.value = value;
            this.color = color;
        }

        public Card(Card other)
        {
            this.value = other.value;
            this.color = other.color;
        }

        public override string ToString()
        {
            return color.ToString() + " " + value.ToString();
        }

        public bool CanPlayOn(Card topCard)
        {
            // Check this card is wild
            if (color == CardColor.Wild)
            {
                return true;
            }

            // Card must match either color or value of top card
            return color == topCard.color || value == topCard.value;
        }

        public bool Equals(Card other)
        {
            if (this.ToString() == other.ToString())
            { }

            // Compare color only if the card is not wild
            if (Value == CardValue.Wild && other.Value == CardValue.Wild)
            {
                return true;
            }
            else if (Value == CardValue.WildDrawFour && other.Value == CardValue.WildDrawFour)
            {
                return true;
            }
            else
            {
                bool b = Value == other.Value && Color == other.Color;
                return b;
            }
        }

        public override int GetHashCode()
        {
            if (Value == CardValue.Zero)
            {
                int val = (int)Value;
            }
            return (int) Value;
        }

        /// <summary>
        /// The points value of the card
        /// </summary>
        public int Score
        {
            get
            {
                return cardScores[this.value];
            }
        }
    }
}
