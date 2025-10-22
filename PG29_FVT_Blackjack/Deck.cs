using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG29_FVT_Blackjack
{
    internal class Deck
    {
        List<Card> currentDeck;
        int deckSize;

        // Basic constructor for a default deck of 53 cards
        public Deck()
        {
            deckSize = 52;
            currentDeck = new List<Card>(deckSize);
        }

        // Parameterized constructor for a custom deck size
        public Deck(int size)
        {
            deckSize = size;
            currentDeck = new List<Card>(deckSize);
        }

        // Getter for the current deck
        public List<Card> getDeck()
        {

            return currentDeck;
        }
        public void makeDefaultDeck()
        {
            // Temporary card variable to create new cards
            Card newCard;
            // Loop to create 52 cards and add them to the current deck
            for (int i = 1; i <= 52; i++)
            {
                // Suit 1 (Hearts - Red): Cards 1-13
                if (i <= 13)
                {
                    if (i == 1)
                    {
                        newCard = new Card('♥', 11, "red", "A");
                    }
                    else if (i <= 10)
                    {
                        newCard = new Card('♥', i, "red", $"{i}");
                    }
                    else if (i == 11)
                    {
                        newCard = new Card('♥', 10, "red", "J");
                    }
                    else if (i == 12)
                    {
                        newCard = new Card('♥', 10, "red", "Q");
                    }
                    // i == 13
                    else
                    {
                        newCard = new Card('♥', 10, "red", "K");
                    }
                }
                // Suit 2 (Diamonds - Red): Cards 14-26
                else if (i <= 26)
                {
                    // Convert to 1-13 range
                    int cardNum = i - 13; 
                    if (cardNum == 1)
                    {
                        newCard = new Card('♦', 11, "red", "A");
                    }
                    else if (cardNum <= 10)
                    {
                        newCard = new Card('♦', cardNum, "red", $"{cardNum}");
                    }
                    else if (cardNum == 11)
                    {
                        newCard = new Card('♦', 10, "red", "J");
                    }
                    else if (cardNum == 12)
                    {
                        newCard = new Card('♦', 10, "red", "Q");
                    }
                    else // cardNum == 13
                    {
                        newCard = new Card('♦', 10, "red", "K");
                    }
                }
                // Suit 3 (Clubs - Black): Cards 27-39
                else if (i <= 39)
                {
                    // Convert to 1-13 range
                    int cardNum = i - 26; 
                    if (cardNum == 1)
                    {
                        newCard = new Card('♣', 11, "black", "A");
                    }
                    else if (cardNum <= 10)
                    {
                        newCard = new Card('♣', cardNum, "black", $"{cardNum}");
                    }
                    else if (cardNum == 11)
                    {
                        newCard = new Card('♣', 10, "black", "J");
                    }
                    else if (cardNum == 12)
                    {
                        newCard = new Card('♣', 10, "black", "Q");
                    }
                    // cardNum == 13
                    else
                    {
                        newCard = new Card('♣', 10, "black", "K");
                    }
                }
                // Suit 4 (Spades - Black): Cards 40-52
                else
                {
                    // Convert to 1-13 range
                    int cardNum = i - 39; 
                    if (cardNum == 1)
                    {
                        newCard = new Card('♠', 11, "black", "A");
                    }
                    else if (cardNum <= 10)
                    {
                        newCard = new Card('♠', cardNum, "black", $"{cardNum}");
                    }
                    else if (cardNum == 11)
                    {
                        newCard = new Card('♠', 10, "black", "J");
                    }
                    else if (cardNum == 12)
                    {
                        newCard = new Card('♠', 10, "black", "Q");
                    }
                    // cardNum == 13
                    else
                    {
                        newCard = new Card('♠', 10, "black", "K");
                    }
                }
                currentDeck.Add(newCard);
            }
        }

        // Function to create an empty deck of a given size
        public void createEmptyDeck(int size)
        {
            currentDeck = new List<Card>(size);
        }

        // Function to shuffle the current deck of cards
        public void shuffleDeck()
        {
            Random myRan = new Random();
            List<Card> tempDeck = new List<Card>(deckSize);

            int randomNum = myRan.Next(0, 52);

        }
    }
}