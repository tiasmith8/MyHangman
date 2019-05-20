using MyHangman.DAL;
using MyHangman.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHangman
{
    public class PlayersCLI
    {
        const string Command_GetAllPlayers = "1"; 
        const string Command_AddNewPlayer = "2"; 
        const string Command_GetPlayerByUsername = "3"; 
        const string Command_GetLeaderboard = "4";
        const string Command_StartGame = "5";
        const string Command_Quit = "q";

        //Holds db connection to pull player data from SQL Server db
        private IPlayerDAO playerDAO;

        //Constructor
        public PlayersCLI(IPlayerDAO playerDAO)
        {
            this.playerDAO = playerDAO;
        }

        public void RunCLI()
        {
            PrintMenu();

            while (true)
            {
                string command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case Command_GetAllPlayers:
                        GetAllPlayers();
                        break;
                    case Command_AddNewPlayer:
                        AddNewPlayer();
                        break;
                    case Command_GetPlayerByUsername:
                        GetPlayerByUsername();
                        break;
                    case Command_GetLeaderboard:
                        GetLeaderboard();
                        break;
                    case Command_StartGame:
                        Console.WriteLine("Starting Game.");
                        Console.WriteLine();
                        return;
                    case Command_Quit:
                        Console.WriteLine("Thank you for playing MyHangman.");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Enter one of the options above.");
                        break;
                }

                PrintMenu();

            }
        }

        //Yet to implement functionality for keeping score
        private void GetLeaderboard()
        {
            // Connect to the database

            //Pull top 5 players arranged by score
            IList<Player> top5Players = playerDAO.GetLeaderboard();

            // Header
            Console.WriteLine("Player#\t\tUsername\t\t Score");

            foreach (Player p in top5Players)
            {
                Console.WriteLine($"{p.TopPlayers()}");
            }
            Console.ReadLine();
        }

        //Print player info for Username
        private void GetPlayerByUsername()
        {
            Console.Write("Search for player by Username: ");
            string usernameInput = Console.ReadLine();

            IList<Player> returnedPlayer = playerDAO.GetPlayerByUsername(usernameInput);

            if(returnedPlayer.Count ==0)
            {
                Console.WriteLine("No players found.");

            }
            else
            {
                Console.WriteLine($"Player Info: ");
                Console.WriteLine("Player#\t\tUsername\t\t FirstName\t\tLastName");
                Console.WriteLine($"{returnedPlayer[0].ToString()}");
            }

            Console.ReadLine();
        }

        private void GetAllPlayers()
        {
            IList<Player> players = playerDAO.GetAllUsers();

            Console.WriteLine();
            Console.WriteLine("---- Players ----");
            // Align columns {col#, left-align-9 spaces} which are based off of the database column lengths
            Console.WriteLine("{0,-9}{1,-30}{2,-30}{3,-30}",
                "Player#", "Username","FirstName","LastName");

            for (int index = 0; index < players.Count; index++)
            {
                Console.WriteLine(" - " + players[index]);
            }
            Console.ReadLine();
        }

        private void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Main-Menu Type in a command");
            Console.WriteLine(" 1 - See list of all players");
            Console.WriteLine(" 2 - Add Player");
            Console.WriteLine(" 3 - Search for player by Username");
            Console.WriteLine(" 4 - See Leaderboard");
            Console.WriteLine(" 5 - Begin Game");
            Console.Write("Choose an option: " );
        }

        private void AddNewPlayer()
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Username Name: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password Name: ");
            string password = Console.ReadLine();

            Player player = new Player
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };

            bool isSuccessful = playerDAO.AddUser(player);

            if (isSuccessful)
            {
                Console.WriteLine("Player added.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Not able to add player. Try again");
                Console.ReadLine();
            }
        }
    }
}
