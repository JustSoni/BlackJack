using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    public class Card
    {
        public int Value;
        public static string[] SuitsArray = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };
        public string Suit;

        public Card(int value, string suit)
        {
            Value = value;
            Suit = suit;
        }

        public int Power
        {
            get
            {
                if (Value < 10)
                    return Value;
                else if(Value<14)
                {
                    return 10;
                }
                else
                {
                    return 11;
                }
            }
        }

        public override string ToString()
        {
            string tempValue = "";
            string suitSentence = "";
            switch (this.Value)
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
                    tempValue = this.Value.ToString();
                    break;
            }
            switch (this.Suit)
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
            return $"{tempValue} of {suitSentence}";
        }
    }
}
