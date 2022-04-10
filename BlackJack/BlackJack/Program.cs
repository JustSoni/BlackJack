using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BlackJack
{
    public class Program
    {
        static void Main()
        {
            Deck deck = new Deck();

            deck.ShuffleDeck();

            int chips;
            int bet;

            Console.WriteLine("With how much chips would you like to start?");
            Console.Write("Chip -> ");
            chips = int.Parse(Console.ReadLine());

            while (chips > 0)
            {
                if (deck.Count < 15) // Reshuffling a deck if there are less than 10 cards remaining
                {
                    Dealer.Reshuffle(deck);
                }

                Console.WriteLine($"Your Chips = {chips}");

                Messages.DisplayBet();
                bet = int.Parse(Console.ReadLine());

                while (bet > chips)
                {
                    Messages.InsufficientFundsRebet();
                    bet = int.Parse(Console.ReadLine());
                }

                chips -= bet;

                int win = Dealer.DealGame(bet, deck);

                chips += win;
            }
        }
    }
}