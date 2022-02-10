```
string state = "Menu";
while (state != "Exit")
{
    if (state == "Menu") { state = Menu(); }
    if (state == "Naughts and Crosses") { state = NaughtsAndCrosses(); }
    if (state == "Scissors Paper Rock") { state = ScissorsPaperRock(); }

}
```

main loop 
uses state variable to determine which part of the code should be running

```
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
```
menu function
gets player input which in turn it returns the new state which will be used to choose which game is being played or if the player wants to exit

```
static (string, string) GetInput(Dictionary<string, string> Options, string initialWrite)
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
```