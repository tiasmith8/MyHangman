using System;
using MyHangman.Classes;
using MyHangman.DAL;

namespace MyHangman
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create database connection to pull in players saved to db
            IPlayerDAO playerDAO = new PlayerSqlDAO(@"Server=.\SQLEXPRESS;Database=HangmanPlayers;Trusted_Connection=True;");

            //Create new menu object for entering players menu
            PlayersCLI cli = new PlayersCLI(playerDAO);
            //Run the 1st CLI to get the player
            cli.RunCLI();


            //Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Welcome to Hangman!\n");
            Console.Write("Would you like to play a round? (y/n) ");
            string playGame = Console.ReadLine();
            int numberOfRoundsPlayed = 0;
            
            while (playGame.ToLower().Equals("y"))
            {
                //Create a new game board object and track rounds played.
                Hangman gameBoard = new Hangman(++numberOfRoundsPlayed);

                //Print initial gameboard to screen
                gameBoard.PrintOutBoard();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();

                //Console.WriteLine($"Word to guess: {gameBoard.pickedWord}");

                //While number of tries isn't up or guess isn't there
                while (gameBoard.RemainingAttempts > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Guess a letter: ");
                    Console.ResetColor();
                    string letterGuessed = Console.ReadLine();

                    //Check if character is in the picked word
                    //pass in string with guessed character
                    gameBoard.ContainsLetter(letterGuessed);

                    if (gameBoard.GuessedCharacters.Equals(gameBoard.PickedWord))
                    {
                        Console.WriteLine($"guessed word: {gameBoard.GuessedCharacters.ToString()}");
                        gameBoard.PrintOutBoard();

                        break;
                    }

                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nWould you like to play again? (y/n) ");
                playGame = Console.ReadLine();

                //Increment RoundCounter
                gameBoard.PlayAnotherRound();

            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Thank you for playing.\n");
            Console.ResetColor();
        }
    }
}


//TODO
/*
 * Create a Leaderboard database table: Playerid, playerscore
 * Add user score and display in leaderboard
 * Change list all players to ViewLeaderboard menu item
 * Create top menu for Login/Register
 *  -> Create Register flow to Add new user
 *  Top menu: Add moving ascii art and choose between 2 choices: 1-Login 2-Register
 *          If Register
 *          If Login
 * Get padding to display correctly in columns
 * Finish implementing the interface for PlayerSqlDAO
 * Add a scoreboard on the side
 * Add logic for easy (4 letter words), medium(5 letter words), hard (6 letter words) - put into classes
 * Add a scoring mechanism to deduct for hints and increase for correct guesses, decrease for incorrect guesses
 * Add functionality to use database user for the game
 * Refactor code - menu, game boards for easy/medium/hard
 * Add logic for putting the correct letter twice: You already chose that letter
 * Catch null exceptions for bad letters entered
 * Make you win and you lose ascii signature
 * Add unit tests
 */
