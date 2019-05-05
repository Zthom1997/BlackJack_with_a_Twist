using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            int numberOfPlayers = 0;
            bool play = true;

            void AddPlayers()
            {
                Console.WriteLine("WELKOM BIJ BLACKJACK WITH A TWIST!");
                Console.Write("Voer het aantal spelers in: ");
                try
                {
                    numberOfPlayers = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("De invoer is ongeldig!");
                    Console.Clear();
                }

                //Add players to PlayerList
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    Console.Write("Voer naam in: ");
                    string playerName = Console.ReadLine();

                    game.playerList.Add(new Player(playerName));
                }
            }

            //Kick-off game
            while (play)
            {
                AddPlayers();
                Console.WriteLine("Spelers zijn ingevoerd! Druk op ENTER om te starten.");
                Console.ReadKey();
                game.Play();
                Console.Clear();

                //Ask of player wants to play again
                //While play stays 'true', start the game again
                Console.WriteLine("Nog een keer spelen? (J/N): ");
                string playerInput = Console.ReadLine().ToLower();

                if (playerInput == "j")
                {
                    game.ResetGame();
                }
                else if (playerInput == "n")
                {
                    play = false;
                    Console.WriteLine("Bedankt voor het spelen");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("De invoer is ongeldig!");
                }
            }
        }
    }
}
