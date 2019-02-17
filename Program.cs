using System;
using MyHangman.Classes;

namespace MyHangman
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Welcome to Hangman!\n");
            Console.Write("Would you like to play a round? (y/n) ");
            string playGame = Console.ReadLine();
            int numberOfRoundsPlayed = 0;
            
            while (playGame.ToLower().Equals("y"))
            {
                //Create a new game board object
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
