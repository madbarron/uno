using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public class Game
    {
        const int CARDS_TO_DEAL = 7;
        Deck drawDeck;
        Deck discard;
        Card topCard;

        List<Player> players;

        public Game()
        {
            players = new List<Player>();
            players.Add(new BasicPlayer("Alice"));
            players.Add(new BasicPlayer("Bob"));
            players.Add(new BasicPlayer("Charlie"));

            drawDeck = new Deck();
            drawDeck.Populate();
            drawDeck.Shuffle();

            discard = new Deck();

            Deal();
        }

        protected void Deal()
        {
            for(int i = 0; i < CARDS_TO_DEAL; i++)
            {
                foreach(Player p in players)
                {
                    p.Draw(drawDeck.Draw());
                }
            }
        }

        public void Play()
        {
            bool gameOver = false;
            int playerTurn = 0;
            int turnDirection = 1;

            topCard = drawDeck.Draw();

            // Mulligan wild cards
            while (topCard.Value == CardValue.Wild || topCard.Value == CardValue.WildDrawFour)
            {
                drawDeck.Add(topCard);
                drawDeck.Shuffle();
                topCard = drawDeck.Draw();
            }

            Console.WriteLine(String.Format("The top card is a {0}.", topCard));

            while (!gameOver)
            {
                // Player must draw
                while (!players[playerTurn].CanPlayOn(topCard))
                {
                    players[playerTurn].Draw(drawDeck.Draw());
                    Console.WriteLine(String.Format("{0} had to draw.", players[playerTurn]));
                }

                // Player chooses to draw
                while (players[playerTurn].DrawOption(topCard))
                {
                    players[playerTurn].Draw(drawDeck.Draw());
                    Console.WriteLine(String.Format("{0} chose to draw.", players[playerTurn]));
                }

                // Play the card they chose
                Card toPlay = players[playerTurn].Play(topCard);
                if (toPlay.Color == CardColor.Wild)
                {
                    throw new Exception("Must assign a color to a card before playing a wild card");
                }
                discard.Add(topCard);
                topCard = toPlay;
                Console.WriteLine(String.Format("{0} played a {1}", players[playerTurn], topCard));

                // Check for victory
                if (players[playerTurn].HandCount == 0)
                {
                    Console.WriteLine(String.Format("{0} Wins!!", players[playerTurn]));
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

                    // Handle draw cards
                    if (topCard.Value == CardValue.DrawTwo)
                    {
                        Console.WriteLine(String.Format("{0} draws two.", players[playerTurn]));
                        players[playerTurn].Draw(drawDeck.Draw());
                        players[playerTurn].Draw(drawDeck.Draw());
                    }
                    else if (topCard.Value == CardValue.WildDrawFour)
                    {
                        Console.WriteLine(String.Format("{0} draws four.", players[playerTurn]));
                        players[playerTurn].Draw(drawDeck.Draw());
                        players[playerTurn].Draw(drawDeck.Draw());
                        players[playerTurn].Draw(drawDeck.Draw());
                        players[playerTurn].Draw(drawDeck.Draw());
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
