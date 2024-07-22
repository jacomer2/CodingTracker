using Spectre.Console;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using CodingTracker.Controllers;
using CodingTracker.Helpers;
using System.Runtime.CompilerServices;

public static class Program
{
    public static void Main(string[] args)
    {
        AnsiConsole.Markup("[underline red]Hello[/] World!\n");

        CodingController.createDB();

        string age = UserInput.MainMenu();

        Console.WriteLine(age);
        

        bool quit = false;

        while (!quit)
        {
            Console.WriteLine();
            // Ask for the user's favorite fruit
            var action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("CODING TRACKER")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(
                        new[] { "View Session History", "Add Coding Session", "Delete Coding Session",
                                "Update Coding Session", "Exit Application" }
                    )
            );

            switch (action)
            {
                case "View Session History":
                    AnsiConsole.WriteLine("Viewing session history...");
                    break;

                case "Add Coding Session":
                    AnsiConsole.WriteLine("Adding a new coding session...");
                    break;

                case "Delete Coding Session":
                    AnsiConsole.WriteLine("Deleting a coding session...");
                    break;

                case "Update Coding Session":
                    AnsiConsole.WriteLine("Updating a coding session...");
                    break;

                case "Exit Application":
                    return;

                default:
                    AnsiConsole.WriteLine("Unknown action selected.");
                    break;
            }

        }

    }

}