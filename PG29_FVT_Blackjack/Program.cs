namespace PG29_FVT_Blackjack
{
    using System.Reflection.Metadata;
    using System.Runtime.CompilerServices;
    using System;
    using System.Linq;

    internal class Program
    {
        static bool isGameOver = false;

        static void Main(string[] args)
        {
            while (!isGameOver)
            {
                PlayBlackJack();
            }
        }
        static void PlayActions()
        {
            Console.WriteLine("1: Switch one random one of your cards with the dealers hidden card only once");
            Console.WriteLine("2: Randomly double either your total or the dealers total");
            Console.WriteLine("3: Remove one of your cards randomly for another from the deck");
            Console.WriteLine("4: Hit");
            Console.WriteLine("5: Stand");
        }

        private static void PlayBlackJack()
        {
            // Generating the basic game deck
            Deck gameDeck = new Deck();
            gameDeck.makeDefaultDeck();
            List<Card> playingDeck = gameDeck.getDeck();

            // Game variables
            float money = 100;
            Random random = new Random();
            bool isRoundOver = false;

            while (money > 0)
            {
                Console.WriteLine("\n=== NEW ROUND ===");
                Console.WriteLine("You have $" + money);

                // Get bet amount
                float betAmount;
                while (true)
                {
                    Console.Write("How much would you like to bet? $");
                    string userBet = Console.ReadLine();

                    if (float.TryParse(userBet, out betAmount) && betAmount > 0 && betAmount <= money)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid bet. Please enter an amount between $1 and $" + money);
                }

                // Deal initial cards (random from deck, duplicates allowed
                List<Card> playerHand = new List<Card>();
                List<Card> dealerHand = new List<Card>();

                playerHand.Add(playingDeck[random.Next(0, 52)]);
                dealerHand.Add(playingDeck[random.Next(0, 52)]);
                playerHand.Add(playingDeck[random.Next(0, 52)]);
                dealerHand.Add(playingDeck[random.Next(0, 52)]);

                // Calculate initial totals
                int playerTotal = CalculateHandValue(playerHand);
                int dealerTotal = CalculateHandValue(dealerHand);

                // Show initial hands
                Console.WriteLine("\nYour cards: ");
                PrintList(playerHand);
                Console.WriteLine("Your total: " + playerTotal);
                Console.WriteLine("\nDealer's first card: ");
                dealerHand[0].printCard();
                Console.WriteLine();

                // Check for player blackjack
                if (playerTotal == 21)
                {
                    Console.WriteLine("\n BLACKJACK! YOU WIN! ");
                    float winnings = betAmount * 1.5f;
                    money += winnings;
                    Console.WriteLine("You won $" + winnings + "! You now have $" + money);
                    continue;
                }

                // Player's turn
                bool playerBusted = false;
                string userInput = "";

                while (playerTotal < 21 && !isRoundOver)
                {
                    PlayActions();

                    userInput = Console.ReadLine();
                    int userResponse = int.Parse(userInput);
                    int playerRand = 0;

                    switch(userResponse){
                        case 1:
                            
                            playerRand = random.Next(0, playerHand.Count);

                            playerHand.Add(dealerHand[1]);
                            dealerHand.Add(playerHand[playerRand]);
                            dealerHand.RemoveAt(1);
                            playerHand.RemoveAt(0);

                            playerTotal = CalculateHandValue(playerHand);
                            dealerTotal = CalculateHandValue(dealerHand);

                            Console.WriteLine("Your new total is: " + playerTotal);
                            Console.WriteLine("Dealers new total is: " + dealerTotal);

                            Console.WriteLine("Your cards: ");
                            PrintList(playerHand);
                            Console.WriteLine("Dealers cards: ");
                            PrintList(dealerHand);

                            isRoundOver = true;
                            break;
                        case 2:
                            playerRand = random.Next(0, 2);
                            if(playerRand == 1)
                            {
                                playerTotal = playerTotal * 2;
                                Console.WriteLine("Your new total is: " + playerTotal);
                            }
                            else
                            {
                                dealerTotal = dealerTotal * 2;
                                Console.WriteLine("Dealers new total is: " + dealerTotal);
                            }
                                isRoundOver = true;
                            break;
                        case 3:
                            playerRand = random.Next(0, playerHand.Count);
                            playerHand.RemoveAt(playerRand);
                            playerHand.Add(playingDeck[random.Next(0, 52)]);
                            playerTotal = CalculateHandValue(playerHand);

                            Console.WriteLine("Your new total is: " + playerTotal);
                            Console.WriteLine("Your cards: ");
                            PrintList(playerHand);
                            isRoundOver = true;
                            break;
                        case 4: 
                            // Player hits - get random card
                            playerHand.Add(playingDeck[random.Next(0, 52)]);
                            playerTotal = CalculateHandValue(playerHand);

                            Console.WriteLine("Your new total is: " + playerTotal);
                            PrintList(playerHand);
                            break;
                        case 5:
                            Console.WriteLine("You choose to stand with " + playerTotal);
                            PrintList(playerHand);
                            isRoundOver = true;
                            break;
                            
                    }

                    
                    //Console.WriteLine("\nYour cards: ");
                    //PrintList(playerHand);
                    //Console.WriteLine("Your new total: " + playerTotal);

                    if (playerTotal > 21)
                    {
                        playerBusted = true;
                        Console.WriteLine("\nBUST! You went over 21!");
                        money -= betAmount;
                        Console.WriteLine("You lost $" + betAmount + ". You now have $" + money);
                        isRoundOver = false;
                        break;
                    }
                    else if (playerTotal == 21)
                    {
                        Console.WriteLine("\nYou hit 21! Perfect!");
                        isRoundOver = false;
                        break;
                    }
                }

                // Skip dealer's turn if player busted
                if (playerBusted)
                {
                    continue;
                }

                // Dealer's turn
                Console.WriteLine("\n--- DEALER'S TURN ---");
                Console.WriteLine("Dealer's cards: ");
                PrintList(dealerHand);
                Console.WriteLine("Dealer's total: " + dealerTotal);

                while (dealerTotal < 17)
                {
                    Console.WriteLine("\nDealer hits...");
                    dealerHand.Add(playingDeck[random.Next(0, 52)]);
                    dealerTotal = CalculateHandValue(dealerHand);

                    Console.WriteLine("Dealer's cards: ");
                    PrintList(dealerHand);
                    Console.WriteLine("Dealer's total: " + dealerTotal);
                    isRoundOver = false;
                }

                // Determine winner
                Console.WriteLine("\n=== FINAL RESULTS ===");
                Console.WriteLine("Your total: " + playerTotal);
                Console.WriteLine("Dealer's total: " + dealerTotal);

                if (dealerTotal > 21)
                {
                    Console.WriteLine("\n Dealer busted! YOU WIN!");
                    money += betAmount;
                    Console.WriteLine("You won $" + betAmount + "! You now have $" + money);
                    isRoundOver = false;
                }
                else if (playerTotal > dealerTotal)
                {
                    Console.WriteLine("\n YOU WIN! ");
                    money += betAmount;
                    Console.WriteLine("You won $" + betAmount + "! You now have $" + money);
                    isRoundOver = false;
                }
                else if (dealerTotal > playerTotal)
                {
                    Console.WriteLine("\n Dealer wins!");
                    money -= betAmount;
                    Console.WriteLine("You lost $" + betAmount + ". You now have $" + money);
                    isRoundOver = false;
                }
                else
                {
                    Console.WriteLine("\n PUSH! It's a tie!");
                    Console.WriteLine("You keep your bet. You still have $" + money);
                    isRoundOver = false;
                }
                
                Console.WriteLine("Press [ENTER] to continue...");
                Console.ReadLine();
                Console.Clear();
               
            }

            // Player ran out of money
            Console.WriteLine("\n You're out of money!");
            Console.Write("Would you like to get more money and play again? (y/n): ");
            string response = Console.ReadLine().ToLower();

            if (response == "y")
            {
                isGameOver = false;
                Console.Clear();    
            }
            else
            {
                isGameOver = true;
                Console.WriteLine("Thanks for playing!");
            }
        }

        // Calculate hand value with Ace logic (11 or 1)
        private static int CalculateHandValue(List<Card> hand)
        {
            int total = 0;
            int aceCount = 0;

            foreach (Card card in hand)
            {
                int value = card.getValue();
                total += value;
                if (value == 11) // Ace
                {
                    aceCount++;
                }
            }

            // Convert Aces from 11 to 1 if busting
            while (total > 21 && aceCount > 0)
            {
                total -= 10; // Change an Ace from 11 to 1
                aceCount--;
            }

            return total;
        }

        // Print list of cards
        private static void PrintList(List<Card> cardList)
        {
            foreach (Card card in cardList)
            {
                card.printCard();
            }
        }
    }
}