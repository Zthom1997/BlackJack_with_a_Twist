using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfCards
{
    class Player
    {
        private string name;
        private int valueOfHand;
        private bool bust = false;
        private bool standing = false;
        private int chips;
        private int bet;
        public List<Card> hand = new List<Card>();

        public Player(string _name)
        {
            name = _name;
        }

        //Method to set name of player
        public void SetName(string _name)
        {
            name = _name;
        }

        //Method to get name of player
        public string GetName()
        {
            return name;
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

        public void SetStanding(bool _isStanding)
        {
            standing = _isStanding;
        }

        public bool GetStanding()
        {
            return standing;
        }

        public void SetBust(bool _bust)
        {
            bust = _bust;
        }

        public int GetChips()
        {
            return chips;
        }

        public void SetChips(int _chips)
        {
            chips = _chips;
        }

        public void SetBet(int _bet)
        {
            bet = _bet;
        }

        public int GetBet()
        {
            return bet;
        }

        //Method to check if player wants to stand
        public bool PlayerIsStanding(string input)
        {
            if (!bust && input == "p") 
            {
                standing = true;
                Console.WriteLine("Speler {0} past bij een kaartenwaarde van {1}", name, valueOfHand);
                Console.ReadKey();
                return standing;
            }
            else
            {
                return standing;
            }
        }

        //Method to check if player has blackjack
        public bool HasBlackJack()
        {
            if(valueOfHand == 21)
            {
                Console.WriteLine("Gefeliciteerd! U heeft een BlackJack!");
                return true;
            }
            else
            {
                return false;
            }
        } 

        //Method to check if player is busted
        public bool Bust()
        {
            if(valueOfHand > 21)
            {
                //bust = true;
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
            foreach(Card card in hand)
            {
                Console.WriteLine("{0} {1}", card.GetRank(), card.GetSuit());
                Console.ReadLine();
            }
            Console.WriteLine("De waarde van de kaarten is {0}", valueOfHand);
        }

        //Method to reset player properties
        public void ResetPlayer()
        {
            standing = false;
            bust = false;
            valueOfHand = 0;
            hand.Clear();
        }
    }
}
