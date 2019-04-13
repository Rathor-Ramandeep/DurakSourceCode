using System;
using System.Collections.Generic;

namespace durakTesting
{
    public class Program
    {
        //constant for cards per hand
        private const int CARDS_PER_HAND = 6;
        //declare trump card
        public static PlayingCard trumpCard = null;
        // River
        public static List<PlayingCard> River = new List<PlayingCard>();
        //declare players
        public static Player player1 = new Player("Player 1");
        public static Player player2 = new Player("Bot 2");

        public static List<Player> TotalPlayers = new List<Player>();
        static void Main(string[] args)
        {
            try
            {
                // add players to the total player list
                TotalPlayers.Add(player1);
                TotalPlayers.Add(player2);

                Deck deck1 = new Deck();
                // shuffle cards
                deck1.Shuffle();
                deck1.Shuffle();
                deck1.Shuffle();
                
                // deal the deck between 2 players
                Deal(deck1,2);

                Console.WriteLine("Cards have been assigned. Press any key to set TRUMP card.");
                Console.ReadKey();

                // set trump card
                SetTrump(deck1);

                Console.WriteLine("TRUMP card was set. Press any key to begin game.");
                Console.ReadKey();

                // set active player
                Player ActivePlayer = PickFirstTurn(TotalPlayers);
                while(TotalPlayers.Count > 1)
                {
                    // first turn
                    Console.WriteLine("\nIts your turn:\n" + ActivePlayer);
                    Console.WriteLine("Select a card by pressing 1, 2 etc. Press S to skip");
                    ActivePlayer.PlayCard();

                    // if there are cards in the deck, replenish the hand
                    if(deck1.MyDeck.Count != 0)
                    {
                        ActivePlayer.Hand.Add(deck1.MyDeck[0]);
                        deck1.MyDeck.RemoveAt(0);
                    }

                    // check if any player is finished their cards
                    if (ActivePlayer.Hand.Count <= 0)
                    {
                        TotalPlayers.Remove(ActivePlayer);
                        Console.WriteLine("\n*********GAME OVER**********");
                    }

                    //change active player
                    if (ActivePlayer.PlayerName == "Bot 2") { ActivePlayer = player1; }
                    else { ActivePlayer = player2; }

                    Console.ReadKey();
                }

            }
            catch(ArgumentNullException ee)
            {
                Console.WriteLine("No player had suitable trump suit cards");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }

        }

        /// <summary>
        /// Method prints cards in the river to the console
        /// </summary>
        public static void DisplayRiver()
        {
            Console.WriteLine("\n-------River-------");
            foreach (PlayingCard card in River)
            {
                Console.WriteLine(card.ToString());
            }
        }

        /// <summary>
        /// Deals the card and perpares each player's hand
        /// </summary>
        /// <param name="theDeck"></param>
        /// <param name="totalPlayers"></param>
        static void Deal(Deck theDeck, int totalPlayers = 4)
        {
            if (CARDS_PER_HAND * totalPlayers > theDeck.MyDeck.Count)
            {
                throw new ArgumentException("The deck size specified is not enough for the number of players");
            }
            // make the lists to hold all hands
            List<List<PlayingCard>> playerHands = new List<List<PlayingCard>>();

            for (int player = 0; player < totalPlayers; player++)
            {
                // make a list that stores the current players hand
                List<PlayingCard> CurrentHand = new List<PlayingCard>();
                for (int card = 0; card < CARDS_PER_HAND; card++)
                {
                    Random rnd = new Random();
                    int index = rnd.Next(theDeck.MyDeck.Count);

                    CurrentHand.Add(theDeck.MyDeck[index]);
                    theDeck.MyDeck.RemoveAt(index);
                }
                theDeck.Shuffle();
                //assign hand to players respectively 
                if (player == 0)
                {
                    player1.Hand = CurrentHand;
                }
                if (player == 1)
                {
                    player2.Hand = CurrentHand;
                }
                //if (player == 2)
                //{
                //    player3.Hand = CurrentHand;
                //}
                //if (player == 3)
                //{
                //    player4.Hand = CurrentHand;
                //}
                //add the hand to the overall collection
                playerHands.Add(CurrentHand);
            }
            //output player hands
            player1.Dump();
            player2.Dump();
            //player3.Dump();
            //player4.Dump();

            //Console.WriteLine("\n=======Remaining Deck=========");
            //Console.WriteLine(theDeck.ToString());
        }
        
        
        /// <summary>
        /// Set Trump Card - Assign trump card from remaining deck and remove respective card from deck
        /// </summary>
        /// <param name="theDeck"></param>
        static void SetTrump(Deck theDeck)
        {
            Random rnd = new Random();
            int index = rnd.Next(theDeck.MyDeck.Count);
            trumpCard = theDeck.MyDeck[index];
            theDeck.MyDeck.RemoveAt(index);

            //output trump card
            Console.WriteLine("\n=======TRUMP CARD=======\n" + trumpCard);
            //display remaining deck
            //Console.WriteLine("\n=======Remaining Deck=========");
            //Console.WriteLine(theDeck.ToString());
        }


        /// <summary>
        /// Selects which player gos first depending on the trump card selected for the round
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="player3"></param>
        /// <param name="player4"></param>
        /// <returns>Player object</returns>
        static Player PickFirstTurn(List<Player> TotalPlayers)
        {
            Player returnPlayer = null;
            List<PlayingCard> trumpSuitCards = new List<PlayingCard>();

            foreach (Player player in TotalPlayers)
            {
                PlayingCard playerLowest = player.LowestCard(trumpCard);
                trumpSuitCards.Add(playerLowest);
            }

            int playerIndex = -1;
            PlayingCard lowest = new PlayingCard();  //placeholder card to help compare other cards to
            lowest.Value = 99;      // give high value to start in order to check later

            for (int index = 0; index < trumpSuitCards.Count; index++)
            {
                if (trumpSuitCards[index] != null)
                {
                    if (trumpSuitCards[index].Value < lowest.Value)
                    {
                        playerIndex = index;
                        lowest = trumpSuitCards[index];
                    }
                }
            }
            if(playerIndex != -1)
            {
                returnPlayer = TotalPlayers[playerIndex];
            }


            if (returnPlayer == null)
            {
                throw new ArgumentNullException("Player with lowest card not determined... ERROR!");
            }
            return returnPlayer;
        }


    }
}
