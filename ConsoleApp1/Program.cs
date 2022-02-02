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
            string option = GetInput(gameOptions, "Please choose a game to play");
            return option;
        }
        static string NaughtsAndCrosses()
        {
            Dictionary<string, string> PlayersDict =
              new Dictionary<string, string>(){
                {"1", "1 Player"},
                {"2", "2 Players" } };
            string numPlayers = GetInput(PlayersDict,"Please choose how many players want to play");
            return "Menu";
        }
        static string ScissorsPaperRock()
        {
            //todo
            return "Menu";
        }
        static string GetInput(Dictionary<string, string> Options, string initialWrite)
        {
            string option;
            bool valid = false;
            bool once = false;
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
                string input = Console.ReadLine();
                valid = Options.TryGetValue(input, out option);
                once = true;



            }
            while (valid == false);
            return option;
        }
    }
}
