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
        private string mName;
        private int mSuit;
        private int mValue;
        private string mColor = "";
        public bool IsFaceCard;

        public Card(int suit, int value, string color, string name)
        {
            mSuit = suit;
            mValue = value;
            mColor = color;
            mName = name;
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
        public string getName()
        {
            return mName;
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
        public void setName(string name)
        {
            mName = name;
        }

        //method to print the cards
        public void printCard()
        {
            //Console.WriteLine("Card: " + mValue + " of " + mName);
            Console.WriteLine($"Card: {mValue} of {mName}");
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