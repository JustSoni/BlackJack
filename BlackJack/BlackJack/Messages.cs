using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BlackJack
{
    public static class Messages
    {
        public static void DisplayOptions()
        {
            Console.WriteLine("Dealer: Place your bets sir!");
            Console.Write("Bet -> ");
        }
        public static void Busted()
        {
            Console.WriteLine();
            string text = "BUSTED! Dealer wins!";
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(200);
            }
            Console.ResetColor();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Thread.Sleep(1000);
        }

        public static void DealerWin()
        {
            string text = "Dealer won!";
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(200);
            }
            Console.ResetColor();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Thread.Sleep(1000);

        }

        public static void WinBlackjack(int bet)
        {
            int win= (int)(bet * 2.5);

            for (int i = 0; i < 20; i++)
            {
                if (i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"BLACKJACK! Received {win} chips");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
                else if (i % 3 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"BLACKJACK! Received {win} chips");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
                else if(i % 5 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"BLACKJACK! Received {win} chips");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"BLACKJACK! Received {win} chips");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
                Thread.Sleep(200);
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("------------------------------------");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.ResetColor();
        }

        public static void Win(int bet)
        {
            int win = bet;
            for (int i = 0; i < 20; i++)
            {
                if (i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You win! Received {win} chips");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"You win! Received {win} chips");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
                Thread.Sleep(200);
            }
        }

       

        public static void InsufficientFundsRebet()
        {
            Console.WriteLine("Insufficient funds! Bet again!");
            Console.Write("Bet -> ");
        }

        public static void NextMove()
        {
            Console.Write("Hit/Stand/Double -> ");
        }

        public static void Dealing()
        {
            Console.WriteLine("Dealing!");
        }

        public static void ShowCards(List<Card> hand, bool player)
        {
            if(player)
            {
                Console.WriteLine("________________");
                Console.WriteLine("Player's hand:");
                foreach (var card in hand)
                {
                    Console.WriteLine(card);
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Score = {HandCalculator.Calculate(hand)}");
                Console.ResetColor();
                Console.WriteLine("________________");
            }
            else
            {
                Console.WriteLine("________________");
                Console.WriteLine("Dealer's hand:");
                foreach (var card in hand)
                {
                    Console.WriteLine(card);
                    if(hand.Count==1)
                    {
                        Console.WriteLine("Face down card");
                    }
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Score = {HandCalculator.Calculate(hand)}");
                Console.ResetColor();

                Console.WriteLine("________________");
            }
        }
    }
}
