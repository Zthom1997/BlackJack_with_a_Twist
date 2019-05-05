using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfCards
{
    class House
    {
        public List<Card> hand = new List<Card>();
        private bool bust = false;
        private int pot;
        private int valueOfHand;

        public House(int _pot)
        {
            pot = _pot;
        }


        //Method to set value of hand
        public void SetValueOfHand(int _valueOfHand)
        {
            valueOfHand = _valueOfHand;
        }

        //Method to view value of hand
        public int GetValueOfHand()
        {
            return valueOfHand;
        }

        public void SetPot(int _bet)
        {
            pot = _bet;
        }

        public int GetPot()
        {
            return pot;
        }

        //Method to check if player is busted
        public bool Bust()
        {
            if (valueOfHand > 21)
            {
                bust = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Method to view cards of player
        public void ViewHand()
        {
            foreach (Card card in hand)
            {
                Console.WriteLine("{0} {1}", card.GetRank(), card.GetSuit());
                Console.ReadLine();
            }
            Console.WriteLine("De waarde van de kaarten van de bank is {0}", valueOfHand);
        }

        //Method to reset house properties
        public void HouseReset()
        {
            hand.Clear();
            valueOfHand = 0;
            bust = false;
        }
    }
}
