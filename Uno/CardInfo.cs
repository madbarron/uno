using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public class CardInfo
    {
        private int score;
        private int quantity;
        private bool isWild;

        public int Score { get => score; }
        public int Quantity { get => quantity; }
        public bool IsWild { get => isWild; }

        public CardInfo(int score, int quantity, bool isWild = false)
        {
            this.score = score;
            this.quantity = quantity;
            this.isWild = isWild;
        }
    }
}
