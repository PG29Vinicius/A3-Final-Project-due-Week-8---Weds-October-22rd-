namespace PG29_FVT_Blackjack
{
    using System.Reflection.Metadata;
    using System.Runtime.CompilerServices;
    using System;
    using System.Linq;
    internal class Program
    {
        static void Main(string[] args)
        {
            // Calling the BlackJack game function
            PlayBlackJack();
        }

        private static void PlayBlackJack()
        {
            // Generating the basic game deck values
            Deck gameDeck = new Deck();
            List<Card> playingDeck;
            gameDeck.makeDefaultDeck();
            //gameDeck.resetFaceCards();

            // Creating default variables
            int minNumCards = 20;
            bool isRoundOver = false;
            int dealerTotal = 0;
            int PlayerTotal = 0;
            float Money = 100;
            float BetAmout;

            // Shuffeling the games deck
            gameDeck.shuffleDeck();

            // Creating the playingDeck
            playingDeck = gameDeck.getDeck();

            // Creating new lists for to hold the cards
            List<Card> Player1Hand = new List<Card>();
            List<Card> DealerHand = new List<Card>();


            while (/*playingDeck.Count > minNumCards && */Money >= 0 && !isRoundOver)
            {
                Random random = new Random();
                // Adding Cards to the players and the dealers hand
                DealerHand.Add(playingDeck.ElementAt(random.Next(0, 52)));
                Player1Hand.Add(playingDeck.ElementAt(random.Next(0, 52)));
                DealerHand.Add(playingDeck.ElementAt(random.Next(0, 52)));
                Player1Hand.Add(playingDeck.ElementAt(random.Next(0, 52)));

                // Getting the total value for the players and dealers hand
                dealerTotal = DealerHand.ElementAt(0).getValue() + DealerHand.ElementAt(1).getValue();
                PlayerTotal = Player1Hand.ElementAt(0).getValue() + Player1Hand.ElementAt(1).getValue();

                // looping and removing the first card 
                for (int i = 0; i < 4; i++)
                {
                    playingDeck.RemoveAt(i);
                }
                // Displaying the UI for the player and dealers hand
                Console.WriteLine("You have $" + Money + " Dollars");
                Console.WriteLine("How much would you like to bet? ");
                string userBet = Console.ReadLine();
                BetAmout = float.Parse(userBet);
                if (BetAmout > Money)
                {
                    Console.WriteLine("You dont have that much money try again");
                    return;
                }

                Console.WriteLine("These are your cards: ");

                Player1Hand.ElementAt(0).printCard();
                Player1Hand.ElementAt(1).printCard();
                Console.Write("Your total is: " + PlayerTotal);
                Console.WriteLine();
                Console.Write("The dealers first card is: ");
                DealerHand.ElementAt(0).printCard();

                // Checking for a players blackjack 
                if (PlayerTotal == 21)
                {
                    isRoundOver = true;
                    Console.WriteLine("BLACKJACK YOU WIN!");
                    Money = (float)(BetAmout * 1.5);
                    return;
                }
                // If the players total is over 21 they bust and loss their bet
                else if (PlayerTotal > 21)
                {
                    isRoundOver = true;
                    Console.WriteLine("You busted. The dealer wins");
                    Money = Money - BetAmout;
                    return;
                }
                Console.WriteLine("Would you like to hit or stand? enter H for hit or S for stand");
                String userInput = Console.ReadLine().ToUpper();

                // Checking if the player wants to hit or stand
                while (!userInput.Equals("H") && !userInput.Equals("S"))
                {
                    Console.WriteLine("Type a valid answer. Would you like to hit or stand? enter H for hit or S for stand");
                    userInput = Console.ReadLine();
                    userInput.ToUpper();
                }
                if (userInput.Equals("S"))
                {
                    Console.WriteLine("You choose to stand your total is: " + PlayerTotal);
                    Console.Write($"Your Dealers cards are: ");
                    printList(DealerHand);
                    
                }

                // If the player wants to hit and isnt over 21 give an extra card then ask again
                while (userInput.Equals("H") && PlayerTotal < 21 && !isRoundOver)
                {
                    Player1Hand.Add(playingDeck.ElementAt(0));
                    PlayerTotal = PlayerTotal + playingDeck.ElementAt(0).getValue();
                    printList(Player1Hand);
                    Console.WriteLine("Your new total is : " + PlayerTotal);

                    Console.WriteLine("Would you like to hit or stand? enter H for hit or S for stand");
                    userInput = Console.ReadLine();
                    userInput.ToUpper();
                    if (PlayerTotal > 21)
                    {
                        isRoundOver = true;
                        Console.WriteLine("You busted. The dealer wins");
                        Money = Money - BetAmout;
                        return;
                    }
                }

                // If they stand the dealer checks if their total is under 17 and adds another card until it is or busts
                while (userInput.Equals("S") && dealerTotal < 17 && !isRoundOver)
                {
                    DealerHand.Add(playingDeck.ElementAt(0));
                    dealerTotal = dealerTotal + playingDeck.ElementAt(0).getValue();
                    Console.Write("The dealers new cards are: ");
                    printList(DealerHand);
                    Console.WriteLine("The dealers total is : " + dealerTotal);
                }
                // Checking for a players blackjack 
                if (PlayerTotal == 21)
                {
                    isRoundOver = true;
                    Console.WriteLine("BLACKJACK YOU WIN!");
                    Money = (float)(BetAmout * 1.5);
                    return;
                }

                // Checking if the player loses and the dealer didn't bust
                if (dealerTotal > PlayerTotal && dealerTotal < 22)
                {
                    isRoundOver = true;
                    Console.WriteLine("Dealer wins");
                    Money = Money - BetAmout;
                    Console.WriteLine($"You lost ${BetAmout}. You now have ${Money} dumbass");
                    return;
                }

                // Checking if the player busted 
                else if (PlayerTotal > 21)
                {
                    isRoundOver = true;
                    Console.WriteLine("Dealer wins");
                    Money = Money - BetAmout;
                }

                // Checking if the dealer busted
                else if (dealerTotal > 21)
                {
                    isRoundOver = true;
                    Console.WriteLine("Dealers total is over 21 their Total is: " + dealerTotal);
                    Console.WriteLine("Dealer busted you WIN! ");
                    Money = BetAmout + BetAmout;
                }

                // Checking if you pushed
                else if (dealerTotal == PlayerTotal)
                {
                    isRoundOver = true;
                    Console.WriteLine("Push you get your money back");
                }
            }
            // Checking if the player runs out of money and if they want to play again after
            if (Money <= 0)
            {
                Console.WriteLine("You lost all your money would you like to get more and play again('y/n')");
                string userResponse = Console.ReadLine();
                if (userResponse == "y")
                {
                    Money = 100;
                }
                else
                {
                    return;
                }
            }
        }
        // Printing the list of the cards 
        private static void printList(List<Card> cardList)
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                cardList.ElementAt(i).printCard();
            }
        }
    }
}