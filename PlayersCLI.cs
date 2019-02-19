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
            throw new NotImplementedException();
        }

        private void GetPlayerByUsername()
        {
            throw new NotImplementedException();
        }

        private void GetAllPlayers()
        {
            IList<Player> players = playerDAO.GetAllUsers();

            Console.WriteLine();
            Console.WriteLine("---- Players ----");
            Console.WriteLine("Player#\t\tUsername\t\t FirstName\t\tLastName");

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
            Console.WriteLine(" 3 - Enter your Username");
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
