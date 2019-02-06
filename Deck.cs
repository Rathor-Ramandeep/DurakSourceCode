/** Deck.cs
 *	
 *	This is a collection class used to intantiate a deck of cards.
 *   
 *	@author		Musab Nazir
 *	@version	2019.04
 *	@since		Feb 2019 
*/
using System;
using System.Text;
using System.Collections.Generic;

namespace durakTesting
{
    public class Deck
    {
        // deck sizes
        public static int DEFAULT_DECK_SIZE = 52;
        public static int NORMAL_DECK_SIZE = 36;
        public static int SMALL_DECK_SIZE = 20;

        // Empty list to hold all the cards in the deck
        private List<PlayingCard> myDeck = new List<PlayingCard>();

        // Property to modify or retrieve the deck
        public List<PlayingCard> MyDeck { get => myDeck; set => myDeck = value; }

        // Default constructor
        public Deck()
        {
            Initialize(DEFAULT_DECK_SIZE);
        }

        // Parametrized constructor that takes the size of the deck
        public Deck(int deckSize)
        {
            Initialize(deckSize);
        }

        
        /// <summary>
        /// Method that populates the deck depending on the size provided
        /// </summary>
        /// <param name="size"></param>
        public void Initialize(int size)
        {
            for (int i = 0; i < size; i++)
            {
                for(int suits=0; suits < PlayingCard.SUITS; suits++)
                {
                    for(int ranks=0; ranks < PlayingCard.RANKS; ranks++)
                    {
                        PlayingCard card = new PlayingCard();
                        card.Suit = suits;
                        card.Rank = ranks;
                        if(MyDeck.Count < size)
                        {
                            MyDeck.Add(card);
                        }
                    }
                }
            }
        }

        public void Shuffle()
        {
            for (int index = 0; index < MyDeck.Count; index++)
            {
                Random rand = new Random(new System.DateTime().Millisecond);
                // find a Random spot for remaining positions. 
                int swapTarget = index + (rand.Next(MyDeck.Count - index));
                //swap between random spot and index
                PlayingCard temp = MyDeck[index];
                MyDeck[index] = MyDeck[swapTarget];
                MyDeck[swapTarget] = temp;
            }
        }
        
        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (PlayingCard card in MyDeck)
            {
                sb.Append(card.ToString() + "\n");
            }
            return sb.ToString();
        }
    }
}
