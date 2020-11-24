using System;
using System.Collections.Generic;
using System.Text;

namespace Uno
{
    public class Game
    {
        Deck deck;
        Deck discard;
        Card topCard;
        public bool debug = false;
        public bool interactive = false;
        GameResult result;
        List<Player> players;
        Random generator;

        public Game(List<Player> players, int randomSeed = 0)
        {
            deck = new Deck();
            result = new GameResult(players);
            this.players = players;

            if (randomSeed == 0)
            {
                generator = new Random();
            }
            else
            {
                generator = new Random(randomSeed);
            }
        }

        protected void Deal(List<Player> players)
        {
            for(int i = 0; i < GameInfo.STARTING_HAND_SIZE; i++)
            {
                foreach(Player p in players)
                {
                    p.Draw(deck.Draw());
                }
            }
        }

        protected void LogAction(GameAction action, int? seat = null, string player = null)
        {
            action.Seat = seat;
            action.Player = player;

            // Log action 
            result.actions.Add(action);

            // Brodcast action to all players
            for(int i = 0; i < players.Count; i++)
            {
                players[i].OnGameAction(action);
            }

            // Display action if we're in debug mode
            if (debug)
            {
                Console.WriteLine(action);

                // Wait for Enter key if desired
                if (interactive)
                {
                    Console.ReadLine();
                }
            }
        }

        public void Draw(GameActionType type, int seat)
        {
            // Check if we need to shuffle the discard pile
            if (deck.Empty)
            {
                deck = discard;
                deck.Shuffle(generator.Next());
                discard = new Deck();
                LogAction(new GameAction(GameActionType.ShuffleDiscard));
            }

            players[seat].Draw(deck.Draw());
            LogAction(new GameAction(type), seat, players[seat].ToString());
        }

        /// <summary>
        /// Play a game.
        /// </summary>
        /// <returns>Stats from the game</returns>
        public GameResult PlayGame()
        {
            GameAction action;
            Player currentPlayer;

            deck.Populate();
            deck.Shuffle(generator.Next());

            discard = new Deck();

            Deal(players);

            bool gameOver = false;
            int playerTurn = 0;
            int turnDirection = 1;

            // Flip over top card
            topCard = deck.Draw();

            // Mulligan wild cards
            while (topCard.Value == CardValue.Wild || topCard.Value == CardValue.WildDrawFour)
            {
                deck.Add(topCard);
                deck.Shuffle(generator.Next());
                topCard = deck.Draw();
            }

            // Log top card
            action = new GameAction(GameActionType.TopCard);
            action.card = topCard;
            LogAction(action);

            currentPlayer = players[playerTurn];

            while (!gameOver)
            {
                action = currentPlayer.Turn(topCard);

                // Keep drawing as long as the player wants to draw
                while (action.type == GameActionType.Draw)
                {
                    Draw(GameActionType.Draw, playerTurn);
                    action = currentPlayer.Turn(topCard);
                }

                // Play the card they chose
                discard.Add(topCard);
                topCard = action.card;
                LogAction(action, playerTurn, currentPlayer.ToString());

                // Check for victory
                if (currentPlayer.HandCount == 0)
                {
                    //Console.WriteLine(String.Format("{0} Wins!!", currentPlayer));

                    result.winnerSeat = playerTurn;
                    result.winnerName = currentPlayer.ToString();
                    result.pointsWon = 0;

                    for (int i = 0; i < players.Count; i++)
                    {
                        result.seats[i].CardsLeft = players[i].HandCount;
                        result.seats[i].HandScore = players[i].HandScore();
                        result.seats[i].winner = false;

                        result.pointsWon += players[i].HandScore();
                    }

                    result.seats[playerTurn].winner = true;
                    result.seats[playerTurn].PointsWon = result.pointsWon;

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
                        Draw(GameActionType.ForceDraw, playerTurn);
                        Draw(GameActionType.ForceDraw, playerTurn);
                    }
                    else if (topCard.Value == CardValue.WildDrawFour)
                    {
                        Draw(GameActionType.ForceDraw, playerTurn);
                        Draw(GameActionType.ForceDraw, playerTurn);
                        Draw(GameActionType.ForceDraw, playerTurn);
                        Draw(GameActionType.ForceDraw, playerTurn);
                    }
                }
            }

            return result;
        }
    }
}
