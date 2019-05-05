using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfCards
{
    class Card
    {
        private string rank;
        private string suit;

        public Card(string _rank, string _suit)
        {
            rank = _rank;
            suit = _suit;
        }

        //Method to get rank of card
        public string GetRank()
        {
            return rank;
        }

        //Method to get suit of card
        public string GetSuit()
        {
            return suit;
        }
    }
}
