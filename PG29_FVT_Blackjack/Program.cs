namespace PG29_FVT_Blackjack
{
    using System.Reflection.Metadata;
    using System.Runtime.CompilerServices;
    using System;
    using System.Linq;

    internal class Program
    {
        // Used to validate if game is over
        static bool isGameOver = false;

        static void Main(string[] args)
        {
            while (!isGameOver)
            {
                PlayBlackJack();
            }
        }

        // Method to print all the game actions
        static void PlayActions()
        {
            Console.WriteLine("1: Switch one random one of your cards with the dealers hidden card only once");
            Console.WriteLine("2: Randomly double either your total or the dealers total");
            Console.WriteLine("3: Remove one of your cards randomly for another from the deck");
            Console.WriteLine("4: Hit");
            Console.WriteLine("5: Stand");
        }

        // Handles the game logic
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

            //loops as long as the player has money
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
                    //Validates if the player has enough money to place the bet
                    if (float.TryParse(userBet, out betAmount) && betAmount > 0 && betAmount <= money)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid bet. Please enter an amount between $1 and $" + money);
                }

                // Deal initial cards (random from deck, duplicates allowed
                List<Card> playerHand = new List<Card>();
                List<Card> dealerHand = new List<Card>();

                //Deals random cards to the Dealer and the Player
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

                //loops as long as the round is not over
                while (playerTotal < 21 && !isRoundOver)
                {
                    //Prints game actions
                    PlayActions();

                    userInput = Console.ReadLine();
                    int userResponse = int.Parse(userInput);
                    int playerRand = 0;

                    //selects the behaviour based on user selection
                    switch(userResponse){

                        //Exchange one random player card with the dealer's hidden card
                        case 1:
                            
                            playerRand = random.Next(0, playerHand.Count);

                            playerHand.Add(dealerHand[1]);
                            dealerHand.Add(playerHand[playerRand]);
                            dealerHand.RemoveAt(1);
                            playerHand.RemoveAt(playerRand);

                            playerTotal = CalculateHandValue(playerHand);
                            dealerTotal = CalculateHandValue(dealerHand);

                            //Prints the new hands for the Dealer and the player
                            Console.Clear();
                            Console.WriteLine("Your cards: ");
                            PrintList(playerHand);
                            Console.WriteLine("Your new total is: " + playerTotal);
                            Console.WriteLine("Dealers cards: ");
                            PrintList(dealerHand);
                            Console.WriteLine("Dealers new total is: " + dealerTotal);

                            isRoundOver = true;
                            break;
                        //Randomly double either players total or the dealers total
                        case 2:
                            playerRand = random.Next(0, 2);
                            Console.Clear();
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
                        //Remove one of player cards randomly for another from the deck
                        case 3:
                            playerRand = random.Next(0, playerHand.Count);
                            playerHand.RemoveAt(playerRand);
                            playerHand.Add(playingDeck[random.Next(0, 52)]);
                            playerTotal = CalculateHandValue(playerHand);

                            Console.Clear();
                            Console.WriteLine("Your cards: ");
                            PrintList(playerHand);
                            Console.WriteLine("Your new total is: " + playerTotal);
                            isRoundOver = true;
                            break;
                        // Player hits - get random card
                        case 4: 
                            playerHand.Add(playingDeck[random.Next(0, 52)]);
                            playerTotal = CalculateHandValue(playerHand);

                            Console.Clear();
                            Console.WriteLine("Your cards: ");
                            PrintList(playerHand);
                            Console.WriteLine("Your new total is: " + playerTotal);
                            break;
                        //Player stands - delaers turn
                        case 5:
                            Console.Clear();
                            Console.WriteLine("Your cards: ");
                            PrintList(playerHand);
                            Console.WriteLine("You choose to stand with " + playerTotal);
                            isRoundOver = true;
                            break;
                            
                    }
                    //Validate if player busted
                    if (playerTotal > 21)
                    {
                        playerBusted = true;
                        Console.WriteLine("\nBUST! You went over 21!");
                        money -= betAmount;
                        Console.WriteLine("You lost $" + betAmount + ". You now have $" + money);
                        isRoundOver = false;
                        break;
                    }

                    //Validate if the player hits blackjack
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

                //Dealer hits
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

                //Validates if dealer busted
                if (dealerTotal > 21)
                {
                    Console.WriteLine("\n Dealer busted! YOU WIN!");
                    money += betAmount;
                    Console.WriteLine("You won $" + betAmount + "! You now have $" + money);
                    isRoundOver = false;
                }
                //if dealer did not busted, dealer wins
                else if (playerTotal > dealerTotal)
                {
                    Console.WriteLine("\n YOU WIN! ");
                    money += betAmount;
                    Console.WriteLine("You won $" + betAmount + "! You now have $" + money);
                    isRoundOver = false;
                }
                //compares the totals and declares a winner
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
                
                //Play another round...
                Console.WriteLine("Press [ENTER] to continue...");
                Console.ReadLine();
                Console.Clear();
               
            }

            // Player ran out of money
            Console.WriteLine("\n You're out of money!");
            Console.Write("Would you like to get more money and play again? (y/n): ");
            string response = Console.ReadLine().ToLower();

            //logic to play again or quit the game
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
                // Ace Logic
                if (value == 11) 
                {
                    aceCount++;
                }
            }

            // Convert Aces from 11 to 1 if busting
            while (total > 21 && aceCount > 0)
            {
                // Change an Ace from 11 to 1
                total -= 10; 
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