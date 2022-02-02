using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome To Game Suite");
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
            string option;
            bool valid = false;
            bool once = false;
            do
            {
                if (once == true)
                {
                    Console.WriteLine("The game option you put was invalid. You must put the number which is in front of the option you wish to choose.");
                    Console.WriteLine();
                }
                Console.WriteLine("Please choose a game to play");
                foreach (KeyValuePair<string, string> entry in gameOptions)
                {
                    Console.WriteLine(entry.Key + ": " + entry.Value);
                }
                Console.WriteLine("Type the number for the game you want to play:");
                string input = Console.ReadLine();
                valid = gameOptions.TryGetValue(input, out option);
                once = true;

                

            }
            while (valid == false);
            return option;
        }
        static string NaughtsAndCrosses()
        {
            //todo
            return "Menu";
        }
        static string ScissorsPaperRock()
        {
            //todo
            return "Menu";
        }
    }
}
