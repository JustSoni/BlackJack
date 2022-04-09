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
            //Deck.FillDeck();
            //Deck.PrintDeck();

            Deck deck = new Deck();

            deck.ShuffleDeck();

            //Console.WriteLine(deck.Count);

            int chips = 0;
            int bet = 0;

            Console.WriteLine("With how much chips would you like to start?");
            Console.Write("Chip -> ");
            chips = int.Parse(Console.ReadLine());

            string move = "";


            while (true)
            {
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

                while (playerHand.Select(x => x.Power).Sum() < 21) // Player's turn
                {
                    Messages.NextMove();
                    move = Console.ReadLine();
                    while (!(move == "Hit" || move == "Stand" || move == "Double"))
                    {
                        Console.WriteLine("Wrong option, try again -> ");
                        move = Console.ReadLine();
                    }

                    if (move == "Hit")
                    {
                        Messages.Dealing();
                        playerHand.Add(deck.DealStackCard());
                    }
                    else if (move == "Stand")
                    {
                        break;
                    }
                    //else(move=="Double")
                    //{
                    //    bet += bet;
                    //    Messages.Dealing();
                    //    playerHand.Add(deck.DealStackCard());
                    //}

                    Messages.ShowCards(playerHand, true); // True -> Player
                    Messages.ShowCards(dealerHand, false); // False -> Dealer
                }

                if (playerHand.Select(x => x.Power).Sum() > 21)
                {
                    Messages.Busted();
                }
                else if (playerHand.Select(x => x.Power).Sum() == 21 && playerHand.Count == 2)
                {
                    Messages.Dealing();
                    Thread.Sleep(1000);
                    dealerHand.Add(deck.DealStackCard());
                    Thread.Sleep(1000);

                    Messages.ShowCards(playerHand, true); // True -> Player
                    Messages.ShowCards(dealerHand, false); // False -> Dealer

                    Thread.Sleep(1000);

                    if (dealerHand.Select(x => x.Power).Sum() < 21)
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
                    while (dealerHand.Select(x => x.Power).Sum() < 17)
                    {
                        dealerHand.Add(deck.DealStackCard());
                        Thread.Sleep(1000);
                    }

                    if (dealerHand.Select(x => x.Power).Sum() < playerHand.Select(x => x.Power).Sum() || dealerHand.Select(x => x.Power).Sum()>21)
                    {
                        Messages.ShowCards(playerHand, true); // True -> Player
                        Messages.ShowCards(dealerHand, false); // False -> Dealer

                        //Console.ForegroundColor = ConsoleColor.Green;
                        //Console.WriteLine($"You win! Received {bet} chips");


                        Messages.Win(bet);

                        //Console.ResetColor();
                        chips += bet * 2;
                    }
                    else if (dealerHand.Select(x => x.Power).Sum() == playerHand.Select(x => x.Power).Sum())
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