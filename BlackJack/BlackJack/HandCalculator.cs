using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public static class HandCalculator
    {
        public static int Calculate(List<Card> hand)
        {
            int sum = 0;

            int acesCount = 0;

            foreach (var card in hand)
            {
                if (card.Value == 14) // 14 IS ACE
                {
                    acesCount++;
                }
            }
            sum = hand.Select(x => x.Power).Sum();

            for (int i = 0; i < acesCount; i++)
            {
                if (sum > 21)
                    sum -= 10;
            }

            return sum;
        }
    }
}
