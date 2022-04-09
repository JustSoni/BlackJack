using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class Deck
    {
        public Card[] deck = new Card[52];

        public Stack<Card> stackDeck = new Stack<Card>();



        public Deck()
        {
            stackDeck = new Stack<Card>();
            FillStackDeck();
        }

        public int Count => stackDeck.Count();

        public void FillStackDeck()
        {
            CleanStackDeck();
            int index = 0;
            foreach (string suit in Card.SuitsArray)
            {
                for (int value = 2; value <= 14; value++)
                {
                    Card card = new Card(value, suit);
                    stackDeck.Push(card);
                    index++;
                }
            }
            Random rnd = new Random();
            stackDeck.OrderBy(x => rnd.Next());
        }

        public void ReShuffleDeck()
        {
            FillStackDeck();
            ShuffleDeck();
        }

        public Stack<Card> ShuffleDeck()
        {
            List<Card> listDeck = stackDeck.ToList();

            int n = listDeck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = listDeck[k];
                listDeck[k] = listDeck[n];
                listDeck[n] = value;
            }

            stackDeck = new Stack<Card>(listDeck);
            return stackDeck;
        }

        public void CleanStackDeck()
        {
            for (int i = 0; i < 52; i++)
            {
                if (stackDeck.Count > 0)
                {
                    stackDeck.Pop();
                }
                else
                {
                    break;
                }
            }
        }

        public void DealStackDeck()
        {
            for (int i = 0; i < 52; i++)
            {
                Card dealtCard = stackDeck.Pop();
                string tempValue = "";
                string suitSentence = "";
                switch (dealtCard.Value)
                {
                    case 11:
                        tempValue = "Jack";
                        break;
                    case 12:
                        tempValue = "Queen";
                        break;
                    case 13:
                        tempValue = "King";
                        break;
                    case 14:
                        tempValue = "Ace";
                        break;
                    default:
                        tempValue = dealtCard.Value.ToString();
                        break;
                }
                switch (dealtCard.Suit)
                {
                    case "Hearts":
                        suitSentence = " of Hearts";
                        break;
                    case "Diamonds":
                        suitSentence = " of Diamonds";
                        break;
                    case "Clubs":
                        suitSentence = " of Clubs";
                        break;
                    case "Spades":
                        suitSentence = " of Spades";
                        break;
                }
                Console.WriteLine($"{tempValue} of {suitSentence}");
            }
        }

        /*
        public  void PrintDeck()//
        {
            for (int i = 0; i < 52; i++)
            {
                Console.WriteLine($"{deck[i].Value} of {deck[i].Suit}");
            }
        }
        */
        public Card DealStackCard()
        {
            Card dealtCard = stackDeck.Pop();
            string tempValue = "";
            string suitSentence = "";
            switch (dealtCard.Value)
            {
                case 11:
                    tempValue = "Jack";
                    break;
                case 12:
                    tempValue = "Queen";
                    break;
                case 13:
                    tempValue = "King";
                    break;
                case 14:
                    tempValue = "Ace";
                    break;
                default:
                    tempValue = dealtCard.Value.ToString();
                    break;
            }
            switch (dealtCard.Suit)
            {
                case "Hearts":
                    suitSentence = " of Hearts";
                    break;
                case "Diamonds":
                    suitSentence = " of Diamonds";
                    break;
                case "Clubs":
                    suitSentence = " of Clubs";
                    break;
                case "Spades":
                    suitSentence = " of Spades";
                    break;
            }
            Console.WriteLine($"{tempValue} of {suitSentence}");
            return dealtCard;
        }

        private static Random rng = new Random();

    }
}
