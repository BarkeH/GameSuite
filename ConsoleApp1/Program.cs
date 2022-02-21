using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        public static bool start = false;
        static void Main(string[] args)
        {
            
            Thread t = new Thread(new ThreadStart(EnterProgram));
            t.Start();
            Console.ReadLine();
            t.Abort();


            Console.Clear();
            string state = "Menu";
            while (state != "Exit")
            {
                if (state == "Menu") { state = Menu(); }
                if (state == "Naughts and Crosses") { state = NaughtsAndCrosses(); }
                if (state == "Scissors Paper Rock") { state = ScissorsPaperRock(); }

            }
        }

        static string Menu()
        {
            Dictionary<string, string> gameOptions =
              new Dictionary<string, string>(){
                {"1", "Naughts and Crosses"},
                {"2", "Scissors Paper Rock" },
                {"3", "Exit" } };
            string option = GetInput(gameOptions, "Please choose a game to play").Item1;
            return option;
        }
        
        static string ScissorsPaperRock()
        {
            string play = "Yes";
            int outcome = 1;

            Dictionary<int, string> winner =
              new Dictionary<int, string>(){
                {1, "You win"},
                {2, "You tie" },
                {3, "You lose" } };
            
            Dictionary<string, string> PlayersChoice =
              new Dictionary<string, string>(){
                {"1", "Scissors"},
                {"2", "Paper" },
                {"3","Rock" },
                {"4", "Exit" } };
            
            do
            {
                Random rnd = new Random();
                int choice = Int16.Parse(GetInput(PlayersChoice, "Please choose which choice you wish to use").Item2);
                if (choice == 4) { break; }
                int robotChoice = rnd.Next(1, 4);
                if (choice == robotChoice) { outcome = 2; }
                else if (choice + 1 == robotChoice || choice - 2 == robotChoice) { outcome = 1; }
                else if (choice == robotChoice + 1 || choice == robotChoice - 2) { outcome = 3; }

                Console.WriteLine("Press enter to see the results");
                Console.ReadLine();
                Console.Clear();

                Console.WriteLine("You Chose: " + PlayersChoice[choice.ToString()]);
                Console.WriteLine("Your Opponent Chose: " + PlayersChoice[robotChoice.ToString()]);
                


                Console.WriteLine(winner[outcome]);
                Console.WriteLine();
                
            }
            while (play == "Yes");
            return "Menu";

        }
        static Tuple<string, string> GetInput(Dictionary<string, string> Options, string initialWrite)
        {
            string option;
            bool valid = false;
            bool once = false;
            string input = "";
            do
            {
                if (once == true)
                {
                    Console.WriteLine("The option you put was invalid. You must put the number which is in front of the option you wish to choose.");
                    Console.WriteLine();
                }
                Console.WriteLine(initialWrite);
                foreach (KeyValuePair<string, string> entry in Options)
                {
                    Console.WriteLine(entry.Key + ": " + entry.Value);
                }
                Console.WriteLine("Type the number for the option you want to choose:");
                input = Console.ReadLine();
                valid = Options.TryGetValue(input, out option);
                once = true;

            }
            while (valid == false);
            return Tuple.Create(option, input);
        }

        static int GetZeroToNine(string[] board)
        {
            int choice = 0;
            bool once = false;
            bool valid = false;

            do
            {
                
                if (once == true) { Console.WriteLine("Your input was not valid please try again"); }
                PrintBoard(board);


                string input = Console.ReadLine();
                valid = int.TryParse(input, out choice);
                Console.WriteLine(choice);
                if (choice < 1 || choice > 9) { valid = false; }
                else if (board[choice - 1] != " ") { valid = false; }
                once = true;
                Console.Clear();
            } while (valid == false);

            return choice;
        }

        static string NaughtsAndCrosses()
        {

            bool over = false;
            Dictionary<string, string> PlayersDict =
              new Dictionary<string, string>(){
                {"1", "1 Player"},
                {"2", "2 Players" } };
            Dictionary<string, string> Difficulty =
              new Dictionary<string, string>(){
                {"1", "Easy"},
                {"2", "Hard" } };


            Dictionary<string, string> XorO =
              new Dictionary<string, string>(){
                {"1", "X"},
                {"2", "O" } };
            int turn = 1;
            int numPlayers = Int16.Parse(GetInput(PlayersDict, "Please choose how many players want to play").Item2);
            string robotDiff = "";
            if (numPlayers == 1) { robotDiff = GetInput(Difficulty, "Please choose the difficulty of the AI").Item1; }


            string player1 = GetInput(XorO, "Player 1 choose your piece").Item1;
            string player2 = "X";
            if (player1 == "X") { player2 = "O"; }



            string[] board = { " ", " ", " ", " ", " ", " ", " ", " ", " " };
            string gameOver = "";
            int choice1 = 0;
            int choice2 = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("player 1 it is your turn to choose");
                choice1 = GetZeroToNine(board);
                board[choice1 - 1] = player1;

                gameOver = checkOver(board);


                if (gameOver != "no") { break; }
                Console.Clear();
                if (numPlayers == 2)
                {
                    Console.WriteLine("player 2 it is your turn to choose");
                    choice2 = GetZeroToNine(board);
                }
                else
                {
                    if (robotDiff == "Easy") { choice2 = robotEasy(board) + 1; }
                    if (robotDiff == "Hard") { choice2 = robotHard(board, player1, player2) + 1; }

                }

                board[choice2 - 1] = player2;

                gameOver = checkOver(board);

            } while (gameOver == "no");


            GameOver(gameOver, board, player1, player2);

            return "Menu";
        }
        static void PrintBoard(string[] board)
        {
            Console.WriteLine("     |     |      ");
            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 3; y++)
                {
                    string x = board[(i * 3) + y];
                    if (x != " ")
                    {
                        if (x == "X") { Console.ForegroundColor = ConsoleColor.Red; }
                        else { Console.ForegroundColor = ConsoleColor.Green; }
                        Console.Write("  {0}", board[(i * 3) + y]);
                    }
                    else
                    {
                        Console.Write("  {0}", (i * 3 + 1) + y);
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    if (y != 2) { Console.Write("  |"); }

                }
                Console.WriteLine();

                if (i != 2)
                {
                    Console.WriteLine("_____|_____|_____ ");
                    Console.WriteLine("     |     |      ");
                }

            }
            Console.WriteLine("     |     |      ");
        }
        static string checkOver(string[] board)
        {
            for (int i = 0; i < 3; i++)
            {

                if (board[i * 3] == board[i * 3 + 1] && board[i * 3] == board[i * 3 + 2] && board[i * 3] != " ") { return board[i * 3]; }
                if (board[i] == board[i + 3] && board[i] == board[i + 6] && board[i] != " ") { return board[i];  }
            }
            if (board[0] == board[4] && board[0] == board[8] && board[0] != " ") { return board[0]; }
            if (board[2] == board[4] && board[2] == board[6] && board[2] != " ") { return board[2]; }
            if (checkTie(board)) { return "tie"; }
            return "no";
            
        }
        static bool checkTie(string[] board)
        {
            foreach (string i in board)
            {
                if (i == " ") { return false; }
            }

            return true;
        }
        static void GameOver(string whoWon, string[] board, string player1, string player2)
        {
            PrintBoard(board);
            if (whoWon == "tie") { Console.WriteLine("The game was a tie"); }
            if (whoWon == "X")
            {
                if (player1 == "X") { Console.WriteLine("Player 1 won"); }
                else { Console.WriteLine("Player 2 won"); }
            }
            if (whoWon == "O")
            {
                if (player1 == "O") { Console.WriteLine("Player 1 won"); }
                else { Console.WriteLine("Player 2 won"); }
            }
        }
        static int robotEasy(string[] board)
        {
            Random rnd = new Random();
            int robotChoice;
            bool valid = false;
            do
            {
                robotChoice = rnd.Next(0, 8);
                if (board[robotChoice] == " ") { valid = true; }
            } while (valid == false);
            return robotChoice;

        }
        static int robotHard(string[] board, string player1, string player2)
        {
            int bestMove = -1;
            int bestScore = -1000;
            List<int> moves = new List<int>();

            for (int i = 0; i < 9; i++)
            {
                if (board[i] == " ") { moves.Add(i); }

            }
            foreach (int move in moves)
            {
                board[move] = player2;
                int newScore = miniMax(board, player1, player2, 0, false);
                board[move] = " ";
                if (newScore > bestScore) { bestMove = move; bestScore = newScore; }
            }


            return bestMove;
        }
        static int miniMax(string[] board, string player1, string player2, int depth, bool isMax)
        {
            int score = evaluate(board, player1, player2);
            if (score == 10) { return score; }
            if (score == -10) { return score; }

            if (checkTie(board)) { return 0; }

            if (isMax == true)
            {
                int best = -1000;

                List<int> moves = new List<int>();

                for (int i = 0; i < 9; i++)
                {
                    if (board[i] == " ") { moves.Add(i); }
                }

                foreach (int move in moves)
                {
                    board[move] = player2;
                    best = Math.Max(best, miniMax(board, player1, player2, depth + 1, !isMax));
                    board[move] = " ";
                }
                return best;
            }
            if (isMax == false)
            {
                int best = 1000;

                List<int> moves = new List<int>();

                for (int i = 0; i < 9; i++)
                {
                    if (board[i] == " ") { moves.Add(i); }
                }

                foreach (int move in moves)
                {
                    board[move] = player1;
                    best = Math.Min(best, miniMax(board, player1, player2, depth + 1, !isMax));
                    board[move] = " ";
                }
                return best;
            }

            return 0;
        }

        static int evaluate(string[] board, string player1, string player2)
        {
            string win = checkOver(board);
            if (win == player1) { return -10; }
            if (win == player2) { return +10; }

            return 0;
        }
        static void EnterProgram()
        {
            string msg = "Welcome to the Game Suite";
            string msg2 = "Press enter to continue";

            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg.Length / 2) + "}", msg);
            while (start == false)
            {
                Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg2.Length / 2) + "}", msg2);
                System.Threading.Thread.Sleep(600);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();

                System.Threading.Thread.Sleep(400);
                Console.WriteLine("{0," + ((Console.WindowWidth / 2) + msg2.Length / 2) + "}", msg2);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();

            }
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}

