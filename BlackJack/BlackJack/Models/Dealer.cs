using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BlackJack.Models
{
    public static class Dealer
    {
        public static int DealGame(int bet, Deck deck)
        {
            Console.WriteLine("In DEALER class");

            string move = "";

            int win = 0;
            bool doubleTrigger = false;
            int initialBet = bet;

            List<Card> playerHand = new List<Card>();
            List<Card> dealerHand = new List<Card>();

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
                    doubleTrigger = true;
                       bet += bet;
                    Console.WriteLine($"Doubling your bet! Your bet now is -> {bet}");
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
                win = 0;
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
                    win = (int)(bet * 2.5);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Dealer has also a blackjack! PUSH");
                    Console.WriteLine("Money returned!");
                    Console.ResetColor();
                    win = bet;
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



                    Messages.Win(bet);

                    win = bet * 2;
                }
                else if (HandCalculator.Calculate(dealerHand) == HandCalculator.Calculate(playerHand))
                {
                    Messages.ShowCards(playerHand, true); // True -> Player
                    Messages.ShowCards(dealerHand, false); // False -> Dealer

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"It is a push! {bet} chips returned;");
                    Console.ResetColor();
                    win = bet;
                }
                else
                {
                    Messages.ShowCards(playerHand, true); // True -> Player
                    Messages.ShowCards(dealerHand, false); // False -> Dealer

                    Messages.DealerWin();

                    win = 0;

                    Console.WriteLine();
                }


            }

            if(doubleTrigger)
            {
                win -= initialBet;
            }
            return win;
        }

        public static Deck Reshuffle(Deck deck)
        {
            Thread.Sleep(1000);
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("RESHUFFLING!");
            Console.WriteLine("---------------------------------------------------");
            Thread.Sleep(1000);
            deck.FillStackDeck(); // Reshuffle a deck.

            return deck;
        }
    }
}
