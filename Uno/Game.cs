using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public class Game
    {
        const int CARDS_TO_DEAL = 7;
        Deck deck;
        Deck discard;
        Card topCard;
        public bool debug = false;

        Dictionary<string, int> scores;

        public Game()
        {
            deck = new Deck();
        }

        protected void Deal(List<Player> players)
        {
            for(int i = 0; i < CARDS_TO_DEAL; i++)
            {
                foreach(Player p in players)
                {
                    p.Draw(deck.Draw());
                }
            }
        }

        public void PlayGame(List<Player> players)
        {
            GameResult result = new GameResult(players);
            GameAction action;
            Player currentPlayer;

            deck.Populate();
            deck.Shuffle();

            discard = new Deck();

            Deal(players);

            bool gameOver = false;
            int playerTurn = 0;
            int turnDirection = 1;

            topCard = deck.Draw();

            // Mulligan wild cards
            while (topCard.Value == CardValue.Wild || topCard.Value == CardValue.WildDrawFour)
            {
                deck.Add(topCard);
                deck.Shuffle();
                topCard = deck.Draw();
            }

            Console.WriteLine(String.Format("The top card is a {0}.", topCard));

            currentPlayer = players[playerTurn];

            while (!gameOver)
            {
                action = currentPlayer.Turn(topCard);

                // Keep drawing as long as the player wants to draw
                while (action.type == GameActionType.Draw)
                {
                    currentPlayer.Draw(deck.Draw());
                    Console.WriteLine(String.Format("{0} chose to draw.", currentPlayer));

                    action = currentPlayer.Turn(topCard);
                }

                // Play the card they chose
                if (action.card.Color == CardColor.Wild)
                {
                    throw new Exception("Must assign a color to a card before playing a wild card");
                }

                discard.Add(topCard);
                topCard = action.card;

                Console.WriteLine(String.Format("{0} played a {1}", currentPlayer, topCard));

                // Check for victory
                if (currentPlayer.HandCount == 0)
                {
                    Console.WriteLine(String.Format("{0} Wins!!", currentPlayer));
                    gameOver = true;
                }
                else
                {
                    // Check reverse
                    if (topCard.Value == CardValue.Reverse)
                    {
                        turnDirection *= -1;
                    }

                    // Move to next player
                    if (topCard.Value == CardValue.Skip)
                    {
                        playerTurn = (playerTurn + 2 * turnDirection + players.Count) % players.Count;
                    }
                    else
                    {
                        playerTurn = (playerTurn + turnDirection + players.Count) % players.Count;
                    }

                    currentPlayer = players[playerTurn];

                    // Handle draw cards
                    if (topCard.Value == CardValue.DrawTwo)
                    {
                        Console.WriteLine(String.Format("{0} draws two.", currentPlayer));
                        currentPlayer.Draw(deck.Draw());
                        currentPlayer.Draw(deck.Draw());
                    }
                    else if (topCard.Value == CardValue.WildDrawFour)
                    {
                        Console.WriteLine(String.Format("{0} draws four.", players[playerTurn]));
                        currentPlayer.Draw(deck.Draw());
                        currentPlayer.Draw(deck.Draw());
                        currentPlayer.Draw(deck.Draw());
                        currentPlayer.Draw(deck.Draw());
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
