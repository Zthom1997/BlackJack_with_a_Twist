using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfCards
{
    class Price
    {
        private int price;

        public Price(int _price)
        {
            price = _price;
        }

        //Method to get value of price
        public int GetPrice()
        {
            return price;
        }

        //Method to set value of price
        public void SetPrice(int _bet)
        {
            price += _bet;
        }

        //Method to pay price to winner or bank
        public int PayPrice(Player player, int _bet)
        {
            if (player.HasBlackJack() == true)
            {
                price = _bet / 100 * 150;
                return price;
            }
            else
            {
                price = _bet / 100 * 100;
                return price;
            }
        }

        //Method to reset price
        public void ResetPrice()
        {
            price = 0;
        }
    }
}
