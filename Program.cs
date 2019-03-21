using System;
using System.Collections.Generic;

namespace durakTesting
{
    public class Program
    {
        //constant for cards per hand
        private const int CARDS_PER_HAND = 6;
        //declare trump card
        public static PlayingCard trumpCard = new PlayingCard();
        //declare players
        public static Player player1 = new Player("Player 1");
        public static Player player2 = new Player("Bot 2");
        public static Player player3 = new Player("Bot 3");
        public static Player player4 = new Player("Bot 4");

        static void Main(string[] args)
        {
            try
            {
                Deck deck1 = new Deck(52);
                //Console.WriteLine(deck1.ToString());
                deck1.Shuffle();
                //Console.WriteLine("******************************");
                //Console.WriteLine(deck1.ToString());
                Deal(deck1);

                Console.WriteLine("Cards have been assigned. Press any key to set TRUMP card.");
                Console.ReadKey();

                SetTrump(deck1);

                Console.WriteLine("TRUMP card was set. Press any key to begin game.");
                Console.ReadKey();

                Console.WriteLine("\nPlayer going first:\n" + PickFirstTurn(player1, player2, player3, player4));

                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }

        }

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
                if (player == 2)
                {
                    player3.Hand = CurrentHand;
                }
                if (player == 3)
                {
                    player4.Hand = CurrentHand;
                }
                //add the hand to the overall collection
                playerHands.Add(CurrentHand);
            }
            //output player hands
            player1.Dump();
            player2.Dump();
            player3.Dump();
            player4.Dump();

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
            Console.WriteLine("\n=======Remaining Deck=========");
            Console.WriteLine(theDeck.ToString());
        }

        static Player PickFirstTurn(Player player1, Player player2, Player player3, Player player4)
        {
            Player returnPlayer = new Player();

            PlayingCard player1Lowest = player1.LowestCard(trumpCard);
            PlayingCard player2Lowest = player2.LowestCard(trumpCard);
            PlayingCard player3Lowest = player3.LowestCard(trumpCard);
            PlayingCard player4Lowest = player4.LowestCard(trumpCard);

            bool p1lessp2 = false;
            bool p3lessp4 = false;
            

            if (player1Lowest.Value < player2Lowest.Value)
            {
                p1lessp2 = true;
            }

            if (player3Lowest.Value < player4Lowest.Value)
            {
                p3lessp4 = true;
            }

            if (p1lessp2 == true && p3lessp4)
            {
                if (player1Lowest.Value < player3Lowest.Value)
                {
                    returnPlayer = player1;
                }
                else
                {
                    returnPlayer = player3;
                }
            }
            else if (p1lessp2 == true && p3lessp4 == false)
            {
                if (player1Lowest.Value < player4Lowest.Value)
                {
                    returnPlayer = player1;
                }
                else
                {
                    returnPlayer = player4;
                }
            }
            else if (p1lessp2 == false && p3lessp4 == true)
            {
                if (player2Lowest.Value < player3Lowest.Value)
                {
                    returnPlayer = player2;
                }
                else
                {
                    returnPlayer = player3;
                }
            }
            else if (p1lessp2 == false && p3lessp4 == false)
            {
                if (player2Lowest.Value < player4Lowest.Value)
                {
                    returnPlayer = player2;
                }
                else
                {
                    returnPlayer = player4;
                }
            }
            else
            {
                throw new Exception("Player with lowest card not determined... ERROR!");
            }
            return returnPlayer;
        }


    }
}
