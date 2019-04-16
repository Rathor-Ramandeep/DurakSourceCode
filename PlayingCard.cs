/** PlayingCard.cs
 *	
 *	This is a version of the PlayingCard class developed from OOP 2200.
 *   
 *	@author		Thom MacDonald, Musab Nazir
 *	@version	2019.02
 *	@since		March 2019 
 *	@see		Bronson, G. (2012).  Chapter 10 Introduction to Classes. 
 *				In A First Book of C++ (4th ed.). Boston, MA: Course Technology.
 *				http://acbl.mybigcommerce.com/52-playing-cards/ (images of the playing cards)
*/

using System;
using System.Drawing;

namespace durakTesting
{
    public class PlayingCard
    {
        #region Attributes and Constants
        // an array that holds the names of the ranks from highest to lowest (exluding not used)
        public static string[] CARD_RANK = new string[] { "Two", "Three", "Four", "Five",
                                "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King","Ace" };

        // an array that holds the names of the suits
        public static string[] CARD_SUIT = new string[] { "Spades", "Hearts", "Diamonds", "Clubs" };
        public static int RANKS = 13;      // the number of ranks
        public static int SUITS = 4;       // the number of suits

        private bool IsFaceUp;                    // true if face-up, false if face-down
        private int myRankIndex;                  // an int to represent the rank {1 - 13}
        private int mySuitIndex;                  // an int to represent the suit (0 - 3}
        private int myValue;                      // an int to represent the 'value' of the card. 
        #endregion

        #region Properties
        // Properties	
        // This property sets and gets the card rank in terms of the internal int indexes
        public int Rank
        {
            get
            {
                return myRankIndex;
            }
            set
            {
                if (value < 0 || value >= RANKS)
                {
                    throw new System.IndexOutOfRangeException("Value provided is a not a valid rank");
                }
                else
                {
                    myRankIndex = value;
                }
            }
        }

        // This version of the rank property takes the string names of the rank as input or output
        public string RankString
        {
            get
            {
                return CARD_RANK[Rank];
            }
            set
            {
                bool valid = false; // is the parameter a valid rank
                int rankIndex = 1;  // holds the index of the rank in the rank array if valid

                // for each possible rank until we find a match
                while (!valid && rankIndex <= RANKS)
                {
                    if (value == CARD_RANK[rankIndex])
                    {
                        valid = true; // a match was found
                    }
                    else
                    {
                        rankIndex++; // next index
                    }
                }

                // if the parameter is valid
                if (valid)
                {
                    // set myRankIndex to the rankIndex found
                    Rank = rankIndex;

                }
                else // rank parameter is not valid
                {
                    // throw an appropriate exception
                    throw new System.ArgumentException(value + " is not a recognized playing card rank.");
                }
            }
        }
        // This property sets and gets the card suit in terms of the internal int indexes
        public int Suit
        {
            get
            {
                return mySuitIndex;
            }
            set
            {
                if (value < 0 || value >= SUITS)
                {
                    throw new System.IndexOutOfRangeException("Value provided is a not a valid Suit");
                }
                else
                {
                    mySuitIndex = value;
                }
            }
        }

        // This version of the suit property inputs and outputs in string format
        public string SuitString
        {
            get
            {
                return CARD_SUIT[Suit];
            }
            set
            {
                bool valid = false; // is the parameter a valid rank
                int suitIndex = 0;  // holds the index of the suit in the suit array if valid	

                // for each possible suit until we find a match
                while (!valid && suitIndex < SUITS)
                {
                    if (value == CARD_SUIT[suitIndex])
                    {
                        valid = true; // a match was found
                    }
                    else
                    {
                        suitIndex++; // next index
                    }
                }

                // if the suit parameter is one of the valid suits
                if (valid)
                {
                    // set mySuitIndex to the suitIndex found
                    Suit = suitIndex;
                }
                else // suit parameter is not valid
                {
                    // throw an appropriate exception
                    throw new System.ArgumentException(value + " is not a recognized playing card suit.");
                }
            }
        }
        public int Value { get => myValue; set => myValue = value; }
        public bool FaceUp { get => IsFaceUp; set => IsFaceUp = value; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Costructor. Every card instantiated via this is the Ace of Hearts
        /// </summary>
        public PlayingCard()
        {
            Rank = 0;
            Suit = 0;
            Value = 2;
            FaceUp = true;
        }

        /// <summary>
        /// Parametrized constructor that takes indexes directly
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        /// <param name="value"></param>
        /// <param name="faceUp"></param>
        public PlayingCard(int rank, int suit, bool faceUp = true)
        {
            Rank = rank;
            Suit = suit;
            Value = rank + 2;     // Since the rank array starts with 'two' we need an offset
            FaceUp = faceUp;
        }


        /// <summary>
        /// Overloaded Parametrized Constructor that takes the rank as a string
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        /// <param name="faceUp"></param>
        public PlayingCard(string rank, int suit, bool faceUp = true)
        {
            RankString = rank;
            Suit = suit;
            Value = Rank + 2;     // Since the rank array starts with 'two' we need an offset
            FaceUp = faceUp;
        }

        /// <summary>
        /// Overloaded Parametrized Constructor that takes both the rank and the suit as a string
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        /// <param name="faceUp"></param>
        public PlayingCard(string rank, string suit, bool faceUp = true)
        {
            RankString = rank;
            SuitString = suit;
            Value = Rank + 2;     // Since the rank array starts with 'two' we need an offset
            FaceUp = faceUp;
        }

        /// <summary>
        /// Overloaded Parametrized Constructor that takes the suit as a string
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        /// <param name="faceUp"></param>
        public PlayingCard(int rank, string suit, bool faceUp = true)
        {
            Rank = rank;
            SuitString = suit;
            Value = rank + 2;     // Since the rank array starts with 'two' we need an offset
            FaceUp = faceUp;
        }
        #endregion

        #region Public Methods and Overrides
        /// <summary>
        /// ToString ovveride to show the card in human readable format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // declare an empty string
            string cardString;

            // if the card is face-up
            if (IsFaceUp)
            {
                // build a descriptive string from the obj state
                cardString = CARD_RANK[Rank] + " of " + CARD_SUIT[mySuitIndex];
            }
            else // card is face-down
            {
                // string indicates face-down
                cardString = "Face-Down";
            }
            // return the string
            return cardString;
        }


        /// <summary>
        /// Override for the object class' Equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns> True if the values of the cards are the same
        //public override bool Equals(object obj)
        //{
        //    return (Value == ((PlayingCard)obj).Value);
        //}

        public Image GetImage()
        {
            string SuitName = "";
            string RankName = "";
            string ImageName = "";
            Image cardImage;        // variable to hold the image

            // if card is face down
            if(!FaceUp)
            {
                ImageName = "purple_back";
            }
            else
            {
                // set rank part of the name
                if (Rank == 0) { RankName = "_2"; }
                else if (Rank == 1) { RankName = "_3"; }
                else if (Rank == 2) { RankName = "_4"; }
                else if (Rank == 3) { RankName = "_5"; }
                else if (Rank == 4) { RankName = "_6"; }
                else if (Rank == 5) { RankName = "_7"; }
                else if (Rank == 6) { RankName = "_8"; }
                else if (Rank == 7) { RankName = "_9"; }
                else if (Rank == 8) { RankName = "_10"; }
                else if (Rank == 9) { RankName = "J"; }
                else if (Rank == 10) { RankName = "Q"; }
                else if (Rank == 11) { RankName = "K"; }
                else if (Rank == 12) { RankName = "A"; }

                // set suit part of the name
                if (Suit == 0) { SuitName = "S"; }
                else if(Suit == 1) { SuitName = "H"; }
                else if (Suit == 2) { SuitName = "D"; }
                else { SuitName = "C"; }

                ImageName = RankName + SuitName;
            }
            
            cardImage = Properties.Resources.ResourceManager.GetObject(ImageName) as Image;
            return cardImage;
        }
        #endregion
    }
}