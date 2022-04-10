using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BlackJack
{
    public class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();

            deck.ShuffleDeck();


            int chips = 0;
            int bet = 0;

            Console.WriteLine("With how much chips would you like to start?");
            Console.Write("Chip -> ");
            chips = int.Parse(Console.ReadLine());

            string move = "";


            while (true)
            {
                if (deck.Count < 10) // Reshuffling a deck if there are less than 10 cards remaining
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("RESHUFFLING!");
                    Console.WriteLine("---------------------------------------------------");
                    Thread.Sleep(1000);
                    deck.FillStackDeck(); // Reshuffle a deck.
                }

                Console.WriteLine($"Remaining chips = {chips}");
                List<Card> playerHand = new List<Card>();
                List<Card> dealerHand = new List<Card>();

                Messages.DisplayOptions();
                bet = int.Parse(Console.ReadLine());

                while (bet > chips)
                {
                    Messages.InsufficientFundsRebet();
                    bet = int.Parse(Console.ReadLine());
                }

                chips -= bet;

                Messages.Dealing();
                playerHand.Add(deck.DealStackCard());
                dealerHand.Add(deck.DealStackCard());
                playerHand.Add(deck.DealStackCard());

                Messages.ShowCards(playerHand, true); // True -> Player
                Messages.ShowCards(dealerHand, false); // False -> Dealer

                while (HandCalculator.Calculate(playerHand) < 21) // Player's turn
                {
                    Messages.NextMove();
                    move = Console.ReadLine();
                    move = move.ToLower();
                    while (!(move == "hit" || move == "stand" || move == "double"))
                    {
                        Console.WriteLine("Wrong option, try again -> ");
                        move = Console.ReadLine();
                    }

                    move = move.ToLower();

                    if (move == "hit")
                    {
                        Thread.Sleep(1000);
                        Messages.Dealing();
                        Thread.Sleep(1000);
                        playerHand.Add(deck.DealStackCard());
                        Thread.Sleep(1000);
                    }
                    else if (move == "stand")
                    {
                        break;
                    }
                    else if (move == "double") // TODO :: Add Double
                    {
                        bet += bet;
                        Console.WriteLine(bet);
                        Messages.Dealing();
                        playerHand.Add(deck.DealStackCard());
                        break;
                    }

                    Messages.ShowCards(playerHand, true); // True -> Player
                    Messages.ShowCards(dealerHand, false); // False -> Dealer
                }

                if (HandCalculator.Calculate(playerHand) > 21)
                {
                    Messages.Busted();
                }
                else if (HandCalculator.Calculate(playerHand) == 21 && playerHand.Count == 2)
                {
                    Messages.Dealing();
                    Thread.Sleep(1000);
                    dealerHand.Add(deck.DealStackCard());
                    Thread.Sleep(1000);

                    Messages.ShowCards(playerHand, true); // True -> Player
                    Messages.ShowCards(dealerHand, false); // False -> Dealer

                    Thread.Sleep(1000);

                    if (HandCalculator.Calculate(dealerHand) < 21)
                    {
                        Messages.WinBlackjack(bet);
                        chips += (int)(bet * 2.5);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Dealer has also a blackjack! PUSH");
                        Console.WriteLine("Money returned!");
                        Console.ResetColor();
                        chips += bet;
                    }
                }
                else
                {
                    Messages.Dealing();
                    Thread.Sleep(1000);
                    dealerHand.Add(deck.DealStackCard());
                    Thread.Sleep(1000);
                    while (HandCalculator.Calculate(dealerHand) < 17)
                    {
                        dealerHand.Add(deck.DealStackCard());
                        Thread.Sleep(1000);
                    }

                    if (HandCalculator.Calculate(dealerHand) < HandCalculator.Calculate(playerHand) || HandCalculator.Calculate(dealerHand) > 21)
                    {
                        Messages.ShowCards(playerHand, true); // True -> Player
                        Messages.ShowCards(dealerHand, false); // False -> Dealer

                        //Console.ForegroundColor = ConsoleColor.Green;
                        //Console.WriteLine($"You win! Received {bet} chips");


                        Messages.Win(bet);

                        //Console.ResetColor();
                        chips += bet * 2;
                    }
                    else if (HandCalculator.Calculate(dealerHand) == HandCalculator.Calculate(playerHand))
                    {
                        Messages.ShowCards(playerHand, true); // True -> Player
                        Messages.ShowCards(dealerHand, false); // False -> Dealer

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"It is a push! {bet} chips returned;");
                        Console.ResetColor();
                        chips += bet;
                    }
                    else
                    {
                        Messages.ShowCards(playerHand, true); // True -> Player
                        Messages.ShowCards(dealerHand, false); // False -> Dealer

                        Messages.DealerWin();

                        Console.WriteLine();
                    }


                }


            }
        }
    }
}