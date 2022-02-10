﻿using System;
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
            Dictionary<string, string> XorO =
              new Dictionary<string, string>(){
                {"1", "X"},
                {"2", "O" } };
            int turn = 1;
            int numPlayers = Int16.Parse(GetInput(PlayersDict,"Please choose how many players want to play").Item2);
            string player1 = GetInput(XorO, "Player 1 choose your todo").Item1;
            if (numPlayers == 2) { string player2 = GetInput(PlayersDict, "Player 2 choose your todo").Item1; }
            string[,] board = { {" "," "," " }, { " ", " ", " " }, { " ", " ", " " }, };

            
            PrintBoard(board);
            do
            {
                Console.WriteLine("player 1 it is your turn to choose");
                int choice1 = GetZeroToNine(board);
                board[(int)choice1 / 3 -1, choice1 % 3 - 1] = player1;

            } while (over == false);
            
            return "Menu";
        }
        static void PrintBoard(string[,] board)
        {
            Console.WriteLine("    1     2     3");
            Console.WriteLine("       |     |      ");
            Console.WriteLine("1:  {0}  |  {1}  |  {2}", board[0,0], board[0,1], board[0,2]);
            Console.WriteLine("  _____|_____|_____ ");
            Console.WriteLine("       |     |      ");
            Console.WriteLine("2:  {0}  |  {1}  |  {2}", board[1,0], board[1,1], board[1,2]);
            Console.WriteLine("  _____|_____|_____ ");
            Console.WriteLine("       |     |      ");
            Console.WriteLine("3:  {0}  |  {1}  | {2}", board[2,0], board[2,1], board[2,2]);
            Console.WriteLine("       |     |      ");
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
        static (string,string) GetInput(Dictionary<string, string> Options, string initialWrite)
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
            return (option, input);
        }
        static int GetZeroToNine(string[,] board)
        {
            int choice = 0;
            bool once = false;
            bool valid = false;
            
            

            do
            {
                if (once == false) { Console.WriteLine("Your input was not valid please try again"); }
                PrintBoard(board);


                Console.WriteLine("Please choose the row you wish to place:");
                string input = Console.ReadLine();
                int.TryParse(input, out choice);
                if (choice > 0 && choice < 10) { valid = true; }
                once = true;
            } while (valid == false);
            
            return choice;
        }
    }
}

