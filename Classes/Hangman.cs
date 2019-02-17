using System;
using System.Collections.Generic;
using System.Text;

namespace MyHangman.Classes
{
    /// <summary>
    /// Hangman game
    /// </summary>
    public class Hangman
    {
        /// <summary>
        /// Keeps track of number of rounds played. PROPERTY
        /// </summary>
        public int NumberOfRounds { get; private set; }

        //Hold the game board in a string[37][11]
        /// <summary>
        /// Represents the game board.
        /// </summary>
        public string[] GameBoard { get; private set; }

        /// <summary>
        /// Holds number of remaining attempts left.
        /// </summary>
        public int RemainingAttempts { get; private set; }

        //Create a dictionary to hold the words (length 6)
        /// <summary>
        /// Contains the dictionary of words to guess.
        /// </summary>
        public string[] SixLetterDictionaryWords { get; private set; } = new string[]
        {
                "abrupt", "action", "bailey", "belted", "blazes", "sweaty", "plenty", "mascot",
                "cavern", "coiled", "facing", "fluent", "waited", "surfed", "pirate", "laughs",
                "gifted", "hoards", "making", "motive", "uprise", "sundae", "notify", "hosted",
                "pencil", "pounds", "radius", "wander", "tribal", "soared", "notice", "gloves",
                "thorny", "towels", "trucks", "washer", "thanks", "plural", "nature", "filter"
        };

        /// <summary>
        /// Contains the word the user is trying to guess.
        /// </summary>
        public string pickedWord{get;}

        public char[] guessedCharacters { get; private set; } = { '*', '*', '*', '*', '*', '*' };

        /// <summary>
        /// Holds incorrect guesses.
        /// </summary>
        public List<char> incorrectLetters = new List<char>();

        /// <summary>
        /// Method to print the incorrect letters
        /// </summary>
        public void PrintIncorrectLetters()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Incorrect letters guessed: ");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char wrongChars in incorrectLetters)
            {
                Console.Write($"{wrongChars} ");
            }
            Console.WriteLine();
            Console.ResetColor();

        }


        /// <summary>
        /// Print out the game board
        /// </summary>
        public void PrintOutBoard()
        {
            Console.Clear();
            //Console.WriteLine();
            PrintLogoLarge();
            foreach (string gameBoardString in GameBoard)
            {
                Console.WriteLine(gameBoardString);
            }
        }

        /// <summary>
        /// Checks if guessed letter is in the word
        /// </summary>
        /// <param name="letterGuessed"></param>
        /// <returns></returns>
        public bool ContainsLetter(string letterGuessed)
        {
            bool doesContainLetter = false; //assume false first
            //returns -1 if index not there, otherwise returns the first index of the letter
            int indexAtGuess = pickedWord.IndexOf(letterGuessed);

            //If the letter was found in the word to guess
            if (indexAtGuess != -1)
            {   //Change the * to the letter guess correctly
                this.guessedCharacters[indexAtGuess] = char.Parse(letterGuessed);
                doesContainLetter = !doesContainLetter; //Flag for letter found updated to true
            }

            //Change the value of guess spaces to what was guessed correctly
            if(doesContainLetter)
            {
                switch(indexAtGuess)
                {
                    case 0:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0,11) + " " + letterGuessed + this.GameBoard[8].Substring(13);
                        break;
                    case 1:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 14) + " " + letterGuessed + this.GameBoard[8].Substring(16);
                        break;
                    case 2:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 17) + " " + letterGuessed + this.GameBoard[8].Substring(19);
                        break;
                    case 3:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 20) + " " + letterGuessed + this.GameBoard[8].Substring(22);
                        break;
                    case 4:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 23) + " " + letterGuessed + this.GameBoard[8].Substring(25);
                        break;
                    case 5:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 26) + " " + letterGuessed + this.GameBoard[8].Substring(28);
                        break;
                }

                //**Add logic here to check if the letter is there again
                //Here check to see if the letter exists another time
                CheckForAnotherLetter(letterGuessed, indexAtGuess);

                //Print the board
                PrintOutBoard();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Correct!");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Remaining incorrect guesses: {RemainingAttempts}");
                PrintIncorrectLetters();
                Console.ResetColor();

                string strGuessed = new string(this.guessedCharacters);
                string strPicked = this.pickedWord;

                //If its the last guess remaining
                if (strGuessed.Equals(strPicked))
                {
                    this.RemainingAttempts = 0;
                    Console.WriteLine("\nYou WIN!!");
                }
            }

            /* If index 0   change row 10, spaces 12, 13
             * If index 1   spaces 15, 16
             * If index 2   spaces 18, 19
             * If index 3   spaces 21, 22
             * If index 4   spaces 24, 25
             * If index 5   spaces 27, 28
            */


            //Did not guess correctly
            else
            {
                this.RemainingAttempts--;
                incorrectLetters.Add(char.Parse(letterGuessed));
                IncorrectGuess();
                
            }

            return doesContainLetter;
        }

        public void CheckForAnotherLetter(string letterGuessed, int firstIndex)
        {
            bool doesContainLetter = false; //assume false first
            //returns -1 if index not there, otherwise returns the first index of the letter
            int indexAtGuess = pickedWord.IndexOf(letterGuessed, ++firstIndex);

            //If the letter was found in the word to guess
            if (indexAtGuess != -1)
            {   //Change the * to the letter guess correctly
                this.guessedCharacters[indexAtGuess] = char.Parse(letterGuessed);
                doesContainLetter = !doesContainLetter; //Flag for letter found updated to true
            }

            //Change the value of guess spaces to what was guessed correctly
            if (doesContainLetter)
            {
                switch (indexAtGuess)
                {
                    case 0:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 11) + " " + letterGuessed + this.GameBoard[8].Substring(13);
                        break;
                    case 1:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 14) + " " + letterGuessed + this.GameBoard[8].Substring(16);
                        break;
                    case 2:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 17) + " " + letterGuessed + this.GameBoard[8].Substring(19);
                        break;
                    case 3:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 20) + " " + letterGuessed + this.GameBoard[8].Substring(22);
                        break;
                    case 4:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 23) + " " + letterGuessed + this.GameBoard[8].Substring(25);
                        break;
                    case 5:
                        this.GameBoard[8] = this.GameBoard[8].Substring(0, 26) + " " + letterGuessed + this.GameBoard[8].Substring(28);
                        break;
                }
                //Call again to check for another letter
                CheckForAnotherLetter(letterGuessed, indexAtGuess);
            }

        }


        //Add missed guess to board
        public void IncorrectGuess()
        {
            //look at remaining attempts
            switch (this.RemainingAttempts)
            {
                case 5: //update at index 19
                    this.GameBoard[2] = this.GameBoard[2].Substring(0, 18) + " " + "O" + this.GameBoard[2].Substring(20);
                    break;
                case 4:
                    this.GameBoard[3] = this.GameBoard[3].Substring(0, 17) + " " + "/"  + this.GameBoard[3].Substring(18);
                    break;
                case 3:
                    this.GameBoard[3] = this.GameBoard[3].Substring(0, 19) + " " + "\\" + this.GameBoard[3].Substring(20);
                    break;
                case 2:
                    this.GameBoard[4] = this.GameBoard[4].Substring(0, 18) + " " + "|" + this.GameBoard[4].Substring(20);
                    break;
                case 1:
                    this.GameBoard[5] = this.GameBoard[5].Substring(0, 17) + " " + "/" + this.GameBoard[5].Substring(18);
                    break;
                case 0:
                    this.GameBoard[5] = this.GameBoard[5].Substring(0, 19) + " " + "\\" + this.GameBoard[5].Substring(20);
                    break;
            }
            PrintOutBoard();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"That guess was incorrect. ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Remaining incorrect guesses: {RemainingAttempts}");
            //Print incorrect letters
            PrintIncorrectLetters();

            Console.ForegroundColor = ConsoleColor.DarkGray;

            //If all guesses are used, print you lose
            if (this.RemainingAttempts == 0)
            {
                Console.WriteLine("**YOU LOSE**");
                //Print the word to the board so user knows what word they didnt guess

            }
            else if(this.RemainingAttempts == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Would you like a hint? (y/n) ");
                Console.ResetColor();
                if (char.Parse(Console.ReadLine().ToLower()).Equals('y'))
                {
                    Console.WriteLine($"Word to guess: {this.pickedWord}");
                }
            }
        }

        /// <summary>
        /// Play another round.
        /// </summary>
        public void PlayAnotherRound()
        {
            this.NumberOfRounds++;
        }

        //Constructor - print blank game board
        public Hangman(int roundNumber)
        {
            //Initializes to round 1 initially 
            this.NumberOfRounds = roundNumber;

            //Initialize number of tries to 6
            this.RemainingAttempts = 6;

            //Create the empty game board
            this.GameBoard = new string[] 
            {
               $"|--------HANGMAN GAME Round -{NumberOfRounds} ------",
                "|                  |                 ",
                "|                                    ",
                "|                                    ",
                "|                                    ",
                "|                                    ",
                "|                                    ",
                "|                                    ",
                "|          __ __ __ __ __ __         ",
                "|____________________________________"
            };

            //Get the word to guess
            Random rndNumber = new Random();
            int randomSeedValue = rndNumber.Next(0, SixLetterDictionaryWords.Length - 1);
            this.pickedWord = SixLetterDictionaryWords[randomSeedValue];

        }

        public void PrintLogoSmall()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@".  .  ,.  .  .  ,-. .   ,  ,.  .  .");
            Console.WriteLine(@"|  | /  \ |\ | /    |\ /| /  \ |\ |");
            Console.WriteLine(@"|--| |--| | \| | -. | V | |--| | \|");
            Console.WriteLine(@"|  | |  | |  | \  | |   | |  | |  |");
            Console.WriteLine();
            Console.ResetColor();
        }

        public void PrintLogoLarge()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@" _    _          _   _  _____ __  __          _   _ ");
            Console.WriteLine(@"| |  | |   /\   | \ | |/ ____|  \/  |   /\   | \ | |");
            Console.WriteLine(@"| |__| |  /  \  |  \| | |  __| \  / |  /  \  |  \| |");
            Console.WriteLine(@"|  __  | / /\ \ | . ` | | |_ | |\/| | / /\ \ | . ` |");
            Console.WriteLine(@"| |  | |/ ____ \| |\  | |__| | |  | |/ ____ \| |\  |");
            Console.WriteLine(@"|_|  |_/_/    \_\_| \_|\_____|_|  |_/_/    \_\_| \_|");
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
