using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG29_FVT_Blackjack
{
    // FELIPE
    internal class Card
    {
        private int mSuit;
        private int mValue;
        private string mColor = "";
        public bool IsFaceCard;

        public Card(int suit, int value, string color)
        {
            mSuit = suit;
            mValue = value;
            mColor = color;
        }
        //Getters
        public int getSuit()
        {
            return mSuit;
        }
        public int getValue()
        {
            return mValue;
        }
        public string getColor()
        {
            return mColor;
        }

        //Setters
        public void setColor(string color)
        {
            mColor = color;
        }

        public void setValue(int value)
        {
            mValue = value;
        }
        public void setSuit(int suit)
        {
            mSuit = suit;
        }

        //method to print the cards
        public void printCard()
        {
            Console.WriteLine("Card: " + mValue);
        }
        public void isFaceCard()
        {
            if (mValue < 10 && mValue != 14)
            {
                mValue = 10;
            }
            if (mValue == 14)
            {
                mValue = 11;
            }
        }
    }
}