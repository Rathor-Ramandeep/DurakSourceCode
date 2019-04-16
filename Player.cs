using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace durakTesting
{
    public class Player
    {
        private List<PlayingCard> hand = new List<PlayingCard>();
        private String playerName;
        public bool playerAttacking = true;

        public Player()
        {
            Hand = null;
        }

        public Player(String playerName)
        {
            Hand = null;
            this.PlayerName = playerName;
        }

        public List<PlayingCard> Hand { get => hand; set => hand = value; }
        public string PlayerName { get => playerName; set => playerName = value; }


        /// <summary>
        /// PlayCard - Method for playing a card from hand
        /// </summary>
        virtual public void PlayCard()
        {
            // list that holds playable options for a player
            List<PlayingCard> PlayableCards = new List<PlayingCard>();
            
            // list that holds the indexes of the playable cards in the hand
            List<int> PlayableIndexes = new List<int>();
            
            // if its not the forst turn
            if (Program.River.Count >= 1)
            {
                // populate playable cards list
                foreach (var item in Program.River)
                {
                    foreach (var card in Hand)
                    {
                        if (playerAttacking) // for attackers they can only play cards
                            //with ranks that are already in the river
                        {
                            if (card.Rank == item.Rank)
                            {
                                PlayableCards.Add(card);
                                PlayableIndexes.Add(Hand.IndexOf(card));
                            }
                        }
                        else // for defenders they can only play cards that are the same
                        // suit as ones in the river
                        {
                            if (card.Suit == item.Suit)
                            {
                                PlayableCards.Add(card);
                                PlayableIndexes.Add(Hand.IndexOf(card));
                            }
                        }
                    }
                }

            }
            else // if its the first turn
            {
                // all cards in the hand are playable
                PlayableCards = Hand;
                foreach(var item in Hand)
                {
                    PlayableIndexes.Add(Hand.IndexOf(item));
                }
                
            }

            foreach (var item in PlayableCards)
            {
                Console.WriteLine(item);
            }
            foreach (var item in PlayableIndexes)
            {
                Console.WriteLine(item);
            }
            
            // validate user input
            ValidateInput(PlayableIndexes);

            Program.DisplayRiver();

        }

        /// <summary>
        /// Finds the lowest card of a particular suit.
        /// </summary>
        /// <param name="trumpCard"></param>
        /// <returns>PlayingCard object</returns>
        public PlayingCard LowestCard(PlayingCard trumpCard)
        {
            //create playing card object to return
            PlayingCard theCard = null;

            //create list of playing cards to isolate for lowest card
            List<PlayingCard> cardsWithTrumpSuit = new List<PlayingCard>();

            //find all cards that match trump card suit
            foreach (PlayingCard card in this.Hand)
            {
                if (card.Suit == trumpCard.Suit)
                    cardsWithTrumpSuit.Add(card);
            }
            //find lowest card in cardsWithTrumpSuit if anything was added in cardsWithTrumpSuit
            if (cardsWithTrumpSuit.Count > 0)
            {
                //assume first card is the smallest
                PlayingCard minValueCard = cardsWithTrumpSuit[0];
                // iterate through the list to find the actual smallest card
                for (int i = 0; i <= cardsWithTrumpSuit.Count - 2; i++)
                {
                    if (cardsWithTrumpSuit[i + 1].Value < minValueCard.Value)
                    {
                        minValueCard = cardsWithTrumpSuit[i + 1];
                    }
                }
                theCard = minValueCard;
            }

            return theCard;
        }

        public void Dump()
        {
            Console.WriteLine(ToString());
        }


        /// <summary>
        /// To String override
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\n******Player " + this.PlayerName + " Hand******");
            //foreach (PlayingCard card in Hand)
            //{
            //    builder.Append("\n" + card);
            //}
            return builder.ToString();
        }


        /// <summary>
        /// Validates user iput for selecting what card they want to play
        /// </summary>
        /// <param name="indexes"></param>
        /// <returns>an integer that maps directly to the index of a card in their hand</returns>
        public int ValidateInput(List<int> indexes)
        {
            // get user input
            int userInput;
            bool isValid = false;
            while (!Int32.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Enter  NUMERIC value.");
                foreach (var item in indexes)
                {
                    Console.WriteLine(item);
                }
            }

            foreach (var item in indexes)
            {
                if (userInput == item)
                {
                    // Add the card to the river and remove from hand
                    Program.River.Add(Hand[userInput]);
                    Hand.RemoveAt(userInput);
                    isValid = true;
                }
            }
            // if the index doesn't match the ones they can play
            if (!isValid)
            {
                Console.WriteLine("Invalid card selected. Please try again");
                Console.WriteLine("Playable Index are: ");
                foreach (var item in indexes)
                {
                    Console.WriteLine(item);
                }
                ValidateInput(indexes);
            }
            return userInput;
        }


    }
}

