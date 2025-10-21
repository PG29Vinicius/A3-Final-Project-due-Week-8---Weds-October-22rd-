using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG29_FVT_Blackjack
{
    // VINICIUS
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

        // Function to create a default deck of 53 cards
        public void makeDefaultDeck()
        {
            // Temporary card variable to create new cards
            Card newCard;
            // Loop to create 52 cards and add them to the current deck
            for (int i = 1; i <= 52; i++)
            {
                if (i <= 13)
                {
                    if (i == 1)
                    {
                        newCard = new Card(1, 11, "red", "A");
                    }
                    else if (i < 11)
                    {
                        newCard = new Card(1, i + 1, "red", $"{i + 1}");
                    }
                    else if (i == 11)
                    {
                        newCard = new Card(1, 10, "red", "J");
                    }
                    else if (i == 12)
                    {
                        newCard = new Card(1, 10, "red", "Q");
                    }
                    else if (i == 13)
                    {
                        newCard = new Card(1, 10, "red", "K");
                    }
                }
                else if (i <= 26)
                {
                    if (i == 14)
                    {
                        newCard = new Card(2, 11, "red", "A");
                    }
                    else if (i < 24)
                    {
                        newCard = new Card(2, i + 1, "red", $"{i + 1}");
                    }
                    else if (i == 24)
                    {
                        newCard = new Card(2, 10, "red", "J");
                    }
                    else if (i == 25)
                    {
                        newCard = new Card(2, 10, "red", "Q");
                    }
                    else if (i == 26)
                    {
                        newCard = new Card(2, 10, "red", "K");
                    }
                }
                else if (i <= 39)
                {
                    if (i == 27)
                    {
                        newCard = new Card(3, 11, "black", "A");
                    }
                    else if (i < 37)
                    {
                        newCard = new Card(3, i + 1, "black", $"{i + 1}");
                    }
                    else if (i == 37)
                    {
                        newCard = new Card(3, 10, "black", "J");
                    }
                    else if (i == 38)
                    {
                        newCard = new Card(3, 10, "black", "Q");
                    }
                    else if (i == 39)
                    {
                        newCard = new Card(3, 10, "black", "K");
                    }
                }
                else
                {
                    if (i == 40)
                    {
                        newCard = new Card(4, 11, "black", "A");
                    }
                    else if (i < 50)
                    {
                        newCard = new Card(4, i + 1, "black", $"{i + 1}");
                    }
                    else if (i == 50)
                    {
                        newCard = new Card(4, 10, "black", "J");
                    }
                    else if (i == 51)
                    {
                        newCard = new Card(4, 10, "black", "Q");
                    }
                    else if (i == 52)
                    {
                        newCard = new Card(4, 10, "black", "K");
                    }
                }
            }

            //for (int i = 0; i <= 52; i++)
            //{
            //    // The first 13 cards are of suit 1, the next 13 of suit 2, and so on
            //    if (i < 13)
            //    {
            //        if (i == 0)
            //        {
            //            newCard = new Card(1, 14, "red");
            //        }
            //        else
            //        {
            //            newCard = new Card(1, i + 1, "red");
            //        }
            //    }
            //    else if (i < 26)
            //    {
            //        if (i == 13)
            //        {
            //            newCard = new Card(2, 14, "red");
            //        }
            //        else
            //        {
            //            newCard = new Card(2, i - 12, "red");
            //        }
            //    }
            //    else if (i < 39)
            //    {
            //        if (i == 26)
            //        {
            //            newCard = new Card(3, 14, "red");
            //        }
            //        else
            //        {
            //            newCard = new Card(3, i - 25, "red");
            //        }
            //    }
            //    else
            //    {
            //        newCard = new Card(4, i - 38, "red");
            //    }

            //    // Adding the newly created card to the current deck
            //    currentDeck.Add(newCard);
            //}
        }

        // Function to create an empty deck of a given size
        public void createEmptyDeck(int size)
        {
            currentDeck = new List<Card>(size);
        }

        // Function to print the current deck (for debugging purposes)
        public void printDeck()
        {
            for (int i = 0; i < deckSize; i++)
            {
                //Console.WriteLine("Suit: " + currentDeck[i].getSuitString() + " Value : " + currentDeck[i].getValue());
            }
        }

        // Function to create a custom deck based on given parameters
        public void makeDeck(int maxValue, int minValue, int numOfSuits, String nameOfSuits)
        {

        }

        // Function to reset the value of face cards to 10 (Jack, Queen, King)
        public void resetFaceCards()
        {
            for (int i = 0; i < 53; i++)
            {
                if (currentDeck[i].IsFaceCard)
                {
                    currentDeck[i].setValue(10);
                }
            }
        }

        // Function to shuffle the current deck of cards
        public void shuffleDeck()
        {
            Random myRan = new Random();
            List<Card> tempDeck = new List<Card>(deckSize);

            int randomNum = 0;

            for (int i = 0; i < 53; i++)
            {
                randomNum = myRan.Next(0, deckSize);
                tempDeck.Add(currentDeck[randomNum]);
                currentDeck.RemoveAt(randomNum);
                deckSize--;
            }
            for (int i = 0; i < 53; i++)
            {
                currentDeck.Add(tempDeck[i]);
            }
        }
    }
}