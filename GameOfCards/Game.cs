using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfCards
{
    class Game
    {
        public List<Player> playerList = new List<Player>();
        Deck deck = new Deck();
        House house = new House(10000);
        Price price = new Price(0);

        //Method to start the game
        public void StartGame()
        {
            deck.CreateDeck();
            Console.Clear();

            //Ask each player how many cards they want to take
            foreach(Player player in playerList)
            {
                if (player.hand.Count() < 2)
                {
                    int numberOfCards = 0;
                    while (numberOfCards < 1)
                    {
                        Console.WriteLine("Hoeveel kaarten wilt u afnemen?: ");
                        try
                        {
                            numberOfCards = Int32.Parse(Console.ReadLine());
                            Console.Clear();
                        }
                        catch
                        {
                            Console.WriteLine("Ongeldige invoer!");
                        }
                    }

                    for (int i = 0; i < numberOfCards; i++)
                    {
                        //Take random card from deck and assign it to hand
                        Card card = deck.DealCard();
                        Console.WriteLine("{0} pakt {1} {2}", player.GetName(), card.GetRank(), card.GetSuit());
                        Console.ReadKey();
                        player.hand.Add(card);

                        //Determine value of cards and assign it to hand of the player
                        int valueOfCard = deck.DetermineValueofCard(card);
                        int playerValue = player.GetValueOfHand();
                        int totalValue = playerValue + valueOfCard;
                        player.SetValueOfHand(totalValue);
                    }
                }
                Console.Clear();
            }

        }

        //Method to place bets to increase the price
        public void PlaceBet()
        {
            Console.Clear();
            int houseChips = house.GetPot();
            int houseBet = 500;
            price.SetPrice(houseBet);
            house.SetPot(houseChips - houseBet);

            foreach (Player player in playerList)
            {
                Console.Clear();
                Console.WriteLine("Hoeveel chips wilt u kopen?: ");
                int numberOfChips = Int32.Parse(Console.ReadLine());
                player.SetChips(numberOfChips);

                Console.WriteLine("Hoeveel chips wilt u inzetten?: ");
                int bet = 0;

                while (bet == 0)
                {
                    try
                    {
                        //Calculate price and reduce chips of player
                        bet += Int32.Parse(Console.ReadLine());
                        price.SetPrice(bet);
                        player.SetChips(numberOfChips - bet);
                    }
                    catch
                    {
                        Console.WriteLine("Ongeldige inzet geplaatst!");
                    }
                }
            }
        }
         
        //Method for the player to take cards
        public void PlayerHit(Player player)
        {
            //Take random card from deck and assign it to hand
            Card card = deck.DealCard();
            Console.WriteLine("{0} pakt {1} {2}", player.GetName(), card.GetRank(), card.GetSuit());
            Console.ReadKey();
            player.hand.Add(card);

            //Calculate value of cards and assign it to hand of the player
            int valueOfCard = deck.DetermineValueofCard(card);
            int playerValue = player.GetValueOfHand();
            int totalValue = playerValue + valueOfCard;
            player.SetValueOfHand(totalValue);
        }

        //Method for the house to take cards
        public void HouseHit()
        {
            if(house.hand.Count() == 0)
            {
                Card card = deck.DealCard();
                Console.WriteLine("De bank pakt {0} {1}", card.GetRank(), card.GetSuit());
                Console.ReadKey();
                house.hand.Add(card);
                int valueOfCard = deck.DetermineValueofCard(card);
                int houseValue = house.GetValueOfHand();
                int totalValue = houseValue + valueOfCard;
                house.SetValueOfHand(totalValue);
            }
            else
            {
                //Take cards while the handvalue of the house is less than 16
                while(house.GetValueOfHand() < 16)
                {
                    Console.Clear();
                    Card card = deck.DealCard();
                    Console.WriteLine("De bank pakt {0} {1}", card.GetRank(), card.GetSuit());
                    Console.ReadKey();
                    house.hand.Add(card);
                    int valueOfCard = deck.DetermineValueofCard(card);
                    int houseValue = house.GetValueOfHand();
                    int totalValue = houseValue + valueOfCard;
                    house.SetValueOfHand(totalValue);
                }
            }
        }
        
        //Method to play the actual game
        public void Play()
        {
            PlaceBet();
            StartGame();
            HouseHit();
            foreach (Player player in playerList)
            {
                bool bust = player.Bust();
                string input = "";

                //Check for Blackjack
                if (player.HasBlackJack() && player.hand.Count() == 2)
                {
                    Console.WriteLine("Gefeliciteerd {0} met een BlackJack!", player.GetName());
                }

                //Player has choices while player is not standing or is busted
                while (!player.Bust() && !player.GetStanding())
                {
                    Console.Clear();
                    Console.WriteLine("Het aantal spelers is {0}", playerList.Count());
                    Console.WriteLine("De prijzenpot bevat momenteel ${0},-", price.GetPrice());
                    Console.WriteLine("Het is de beurt aan {0}", player.GetName());

                    //Menu for the player while playing the game
                    Console.WriteLine("Maak uw keuze:");
                    Console.WriteLine("\n\nDruk op K om kaarten te kopen      \nDruk op P om te passen" +
                        "\nDruk op V om de kaarten in hand te kijken" + "\nDruk op H om de kaarten van de bank te kijken");

                    input = Console.ReadLine().ToLower();
                    if (input == "k")
                    {
                        Console.Clear();
                        PlayerHit(player);
                        player.Bust();
                    }
                    else if (input == "v")
                    {
                        player.ViewHand();
                    }
                    else if (input == "h")
                    {
                        house.ViewHand();
                    }
                    else if (input == "p")
                    {
                        player.PlayerIsStanding(input);
                    }
                    else
                    {
                        Console.WriteLine("De invoer is ongeldig!");
                    }
                }

                    //Check if player is busted and return message
                    if (player.Bust())
                    {
                        Console.WriteLine("{0} heeft een kaartenwaarde van {1} en heeft helaas verloren", player.GetName(), player.GetValueOfHand());
                        Console.ReadKey();
                        Console.Clear();
                    }
            }

            //Play rest of the game
            HouseHit();
            DetermineWinner(house);
        }

        //Method to determine who is the winner
        public void DetermineWinner (House house)
        {
            Console.Clear();
            bool houseBust = house.Bust();
            foreach(Player player in playerList)
            {
                //Check if player has lower handvalue than the house
                if (player.GetValueOfHand() > house.GetValueOfHand() && player.Bust() == false|| houseBust == true)
                {
                    Console.WriteLine("Waarde van {0} is {1}....", player.GetName(), player.GetValueOfHand());
                    Console.ReadKey();
                    Console.WriteLine("Waarde van de bank is {0}....", house.GetValueOfHand());
                    Console.ReadKey();
                    Console.WriteLine("Gefeliciteerd! {0} heeft de bank verslagen!", player.GetName());
                    Console.ReadKey();

                    int winnerPrice = price.PayPrice(player, player.GetBet());
                    Console.WriteLine("{0} ontvangt ${1}", player.GetName(), winnerPrice);
                    player.SetChips(player.GetChips() + winnerPrice);
                }
                else if(player.GetValueOfHand() == house.GetValueOfHand())
                {
                    Console.WriteLine("Het is een gelijk spel.");
                    Console.ReadKey();
                }

                else
                {
                    Console.WriteLine("Waarde van {0} is {1}....", player.GetName(), player.GetValueOfHand());
                    Console.ReadKey();
                    Console.WriteLine("Waarde van de bank is {0}....", house.GetValueOfHand());
                    Console.ReadKey();
                    Console.WriteLine("Helaas! {0} is verslagen door de bank!", player.GetName());
                    Console.ReadKey();

                    int bets = price.GetPrice();
                    house.SetPot(bets + house.GetPot());
                }
            }
        }

        //Method to reset player, house and game properties to start a new game
        public void ResetGame()
        {
            foreach(Player player in playerList)
            {
                player.ResetPlayer();
            }
            house.HouseReset();
            deck.ResetDeck();
            price.ResetPrice();
        }
    }
}