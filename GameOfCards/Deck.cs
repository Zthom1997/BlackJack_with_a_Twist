using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfCards
{
    class Deck
    {
        public List<Card> CardDeck = new List<Card>();
        private readonly Random RandomCard = new Random();

        //Method to create the deck of cards
        public void CreateDeck()
        {
            //Unpacking pack of cards and create a deck
            string[] suit = { "Klaveren" , "Schoppen", "Harten", "Ruiten" };
            string[] rank = { "2" , "3", "4", "5", "6", "7", "8", "9", "10", "Boer", "Vrouw", "Heer", "Aas" };

            for (int i = 0; i < suit.Count(); i++)
            {
                for (int j = 0; j < rank.Count(); j++)
                {
                   CardDeck.Add(new Card(suit[i], rank[j]));
                }
            }
        }

        //Method to deal a random card
        public Card DealCard()
        {
            int RandCard = RandomCard.Next(CardDeck.Count());
            Card card = CardDeck[RandCard];
            return card;
        }

        //Method to determine value of card
        //Rank and Suit are converted into deck so GetSuit() returns Rank and GetRank() returns Suit
        public int DetermineValueofCard(Card card)
        {
            int value = 0;
            if (card.GetSuit() == "2" || card.GetSuit() == "3" || card.GetSuit() == "4" || card.GetSuit() == "5" || card.GetSuit() == "6" ||
                    card.GetSuit() == "7" || card.GetSuit() == "8" || card.GetSuit() == "9" || card.GetSuit() == "10")
            {
                value = Int32.Parse(card.GetSuit());
            }
            else if (card.GetSuit() == "Boer" || card.GetSuit() == "Vrouw" || card.GetSuit() == "Heer")
            {
                value = 10;
            }
            else if(card.GetSuit() == "Aas")
            {
                value = 11;
            }
            CardDeck.Remove(card);
            return value;
        }

        
        //Method to reset Carddeck
        public void ResetDeck()
        {
            CardDeck.Clear();
        }
    }
}
