using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Welcome to the Game Suite");
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
        static string NaughtsAndCrosses()
        {

            //todo
            bool over = false;
            Dictionary<string, string> PlayersDict =
              new Dictionary<string, string>(){
                {"1", "1 Player"},
                {"2", "2 Players" } };
            Dictionary<string, string> Difficulty =
              new Dictionary<string, string>(){
                {"1", "Easy"},
                {"2", "Medium" } };


            Dictionary<string, string> XorO =
              new Dictionary<string, string>(){
                {"1", "X"},
                {"2", "O" } };
            int turn = 1;
            int numPlayers = Int16.Parse(GetInput(PlayersDict, "Please choose how many players want to play").Item2);

            if (numPlayers == 1) { string robotDiff = GetInput(Difficulty, "Please choose the difficulty of the AI").Item1; }
            

            string player1 = GetInput(XorO, "Player 1 choose your todo").Item1;
            string player2 = "X";
            if (player1 == "X") { player2 = "O"; }



            string[] board = { " ", " ", " " ,  " ", " ", " " ,  " ", " ", " " };
            string gameOver = "";
            int choice1 = 0;
            int choice2 = 0;
            PrintBoard(board);
            do
            {
                Console.WriteLine("player 1 it is your turn to choose");
                choice1 = GetZeroToNine(board);
                board[choice1 - 1] = player1;

                gameOver = checkOver(board);
               
                if (gameOver != "no") {  break; }

                if (numPlayers == 2)
                {
                     choice2 = GetZeroToNine(board);
                } else
                {
                    choice2 = robotHard(board,player1,player2) + 1;
                }
                
                board[choice2 - 1] = player2;

                gameOver = checkOver(board);

            } while (gameOver == "no");

            Console.WriteLine(gameOver);

            GameOver(gameOver, board, player1, player2); 

            return "Menu";
        }
        static void PrintBoard(string[] board)
        {
            string[] boardCopy = { "","","","","","","","","" };
            Array.Copy(board, boardCopy, 9);

            for (int i = 0; i<9; i++ )
            {
                if (boardCopy[i] == " ")
                {
                    boardCopy[i] = "" + (i + 1);
                }
            }
            

            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", boardCopy[0], boardCopy[ 1], boardCopy[ 2]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", boardCopy[3], boardCopy[4], boardCopy[5]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", boardCopy[6], boardCopy[7], boardCopy[8]);
            Console.WriteLine("     |     |      ");
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
            Random rnd = new Random();
            Dictionary<string, string> PlayersChoice =
              new Dictionary<string, string>(){
                {"1", "Scissors"},
                {"2", "Paper" },
                {"3","Rock" } };
            Dictionary<string, string> KeepPlaying =
              new Dictionary<string, string>(){
                {"1", "Yes"},
                {"2", "No" }};
            do
            {
                int choice = Int16.Parse(GetInput(PlayersChoice, "Please choose which choice you wish to use").Item2);
                int robotChoice = rnd.Next(1, 3);
                if (choice == robotChoice) { outcome = 2; }
                else if (choice + 1 == robotChoice || choice - 2 == robotChoice) { outcome = 1; }
                else if (choice == robotChoice + 1 || choice == robotChoice - 2) { outcome = 3; }
                Console.WriteLine("You Chose: " + PlayersChoice[choice.ToString()]);
                Console.WriteLine("Your Opponent Chose: " + PlayersChoice[robotChoice.ToString()]);

                Console.WriteLine(winner[outcome]);
                Console.WriteLine();
                play = GetInput(KeepPlaying, "Would you like to keep playing?").Item1;
            }
            while (play == "Yes");
            return "Menu";

        }
        static Tuple<string,string> GetInput(Dictionary<string, string> Options, string initialWrite)
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
            Console.WriteLine(option);
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


                Console.WriteLine("Please choose the row you wish to place:");
                string input = Console.ReadLine();
                valid = int.TryParse(input, out choice);
                if (choice < 0 && choice > 10) { valid = false; }
                else if (board[choice - 1] != " ") { valid = false; }
                once = true;
            } while (valid == false);

            return choice;
        }
        static string checkOver(string[] board) {

            

            if ( checkTie(board)) { return "tie";  }
            string win = "no";
            for (int i = 0; i<3; i++)
            {
                
                if (board[i*3] == board[i*3 + 1] && board[i*3] == board[i*3+2] && board[i*3] != " ") { win = board[i*3];  break;  }
                if (board[i] == board[i + 3] && board[i] == board[i + 6] && board[i] != " ") { win = board[i]; break; }
            }
            if (board[0] == board[4] && board[0] == board[8] && board[0] != " ") { win = board[0]; }
            if (board[2] == board[4] && board[2] == board[6] && board[2] != " ") { win = board[2]; }
            return win;
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
            if (whoWon == "X") {
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

            for (int i =0; i<9; i++)
            {
                if (board[i] == " ") { moves.Add(i); }
                  
            }
            foreach (int move in moves)
            {
                board[move] = player2;
                int newScore = miniMax(board, player1, player2, 0, false);
                board[move] = " ";
                 if ( newScore > bestScore) { bestMove = move;  bestScore = newScore; } 
            }


            return bestMove;
        }
        static int miniMax(string [] board, string player1, string player2, int depth, bool isMax)
        {
            Console.WriteLine(score);
            PrintBoard(board);
            int score = evaluate(board, player1, player2);
            if (score == 10) { return score; }
            if (score == -10) { return score; }

            if (checkTie(board) ) { return 0; }

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
                Console.WriteLine(best);
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
                Console.WriteLine(best);
                return best;
            }
            
            return 98032497;
        }

        static int evaluate(string[] board, string player1, string player2)
        {
            string win = checkOver(board);
            Console.WriteLine(win);
            if (win == player1) { return -10; }
            if (win == player2) { return +10; }

            

            return 0;

        }
    }
}
