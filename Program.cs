using System;
using System.Collections.Generic;

namespace durakTesting
{
    public class Program
    {
        private const int CARDS_PER_HAND = 6;
        static void Main(string[] args)
        {
            try
            {
                Deck deck1 = new Deck(20);
                //Console.WriteLine(deck1.ToString());
                deck1.Shuffle();
                //Console.WriteLine("******************************");
                //Console.WriteLine(deck1.ToString());
                Deal(deck1);
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
            
        }

        static void Deal(Deck theDeck, int totalPlayers =4)
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
                List<PlayingCard> Hand = new List<PlayingCard>();

                for (int card = 0; card < CARDS_PER_HAND; card++)
                {
                    Random rnd = new Random();
                    int index = rnd.Next(theDeck.MyDeck.Count);
                    Hand.Add(theDeck.MyDeck[index]);
                    theDeck.MyDeck.RemoveAt(index);
                }
                theDeck.Shuffle();
                //add the hand to the overall collection
                playerHands.Add(Hand);
            }
            
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("*********Hand*************");
                for (int j = 0; j < playerHands[i].Count; j++)
                {
                    Console.WriteLine(playerHands[i][j]);
                }
            }
            Console.WriteLine("\n=======Remaining Deck=========");
            Console.WriteLine(theDeck.ToString());
        }
    }
}
