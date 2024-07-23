using Spectre.Console;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using CodingTracker.Controllers;
using CodingTracker.Helpers;
using System.Runtime.CompilerServices;
using CodingTracker.Models;

public static class Program
{
    public static void Main(string[] args)
    {
        CodingController.createDB();

        bool quit = false;

        while (!quit)
        {
            // Ask for the user's favorite fruit
            var action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]CODING TRACKER[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(
                        new[] { "View Session History", "Add Coding Session", "Delete Coding Session",
                                "Update Coding Session", "Exit Application" }
                    )
            );

            switch (action)
            {
                case "View Session History":
                    var sessions = CodingController.getAllSessions();

                    // Create a table
                    var table = new Table();

                    // Add some columns
                    table.AddColumn("Id");
                    table.AddColumn("Start Time");
                    table.AddColumn("End Time");
                    table.AddColumn("Duration");



                    if (sessions != null)
                    {
                        foreach (var session in sessions)
                        {
                            table.AddRow(session.Id.ToString(), session.StartTime, session.EndTime, session.Duration.ToString() + " Minutes");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No sessions found.");
                    }

                    // Render the table to the console
                    AnsiConsole.Write(table);

                    AnsiConsole.WriteLine("Press any key to continue");
                    AnsiConsole.Console.Input.ReadKey(false);

                    Console.Clear();

                    break;

                case "Add Coding Session":
                    UserInput.InsertSessionPrompt();

                    AnsiConsole.WriteLine("Press any key to continue");
                    AnsiConsole.Console.Input.ReadKey(false);

                    Console.Clear();
                    break;

                case "Delete Coding Session":
                    int id = UserInput.DeleteSessionPrompt();

                    CodingController.deleteDB(id);

                    AnsiConsole.WriteLine("Press any key to continue");
                    AnsiConsole.Console.Input.ReadKey(false);

                    Console.Clear();
                    break;

                case "Update Coding Session":
                    UserInput.UpdateSessionPrompt();

                    AnsiConsole.WriteLine("Press any key to continue");
                    AnsiConsole.Console.Input.ReadKey(false);

                    Console.Clear();
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