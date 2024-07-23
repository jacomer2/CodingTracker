using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTracker.Helpers;
using CodingTracker.Controllers;
using CodingTracker.Models;

namespace CodingTracker.Helpers
{
    class UserInput
    {

        static public void InsertSessionPrompt()
        {
            string startTime = StartTimePrompt();

            string endTime = EndTimePrompt();

            int duration = Conversions.calculateDuration(startTime, endTime);

            while (duration <= 0)
            {
                AnsiConsole.Markup("[red]End time must be after start time[/]");
                Console.WriteLine();

                endTime = EndTimePrompt();

                duration = Conversions.calculateDuration(startTime, endTime);
            }

            CodingSession codingSession = new CodingSession(startTime, endTime, duration);

            CodingController.insertDB(codingSession);

            return;
        }

        static public int DeleteSessionPrompt()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>("What is the [green]ID[/] of the session you want to delete?\n")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]That's not a valid ID[/]")
                .Validate(id =>
                {

                    return id switch
                    {
                        < 0 => ValidationResult.Error("[red]ID must be greater than 0[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));
        }

        static public void UpdateSessionPrompt()
        {
            int id = IdPrompt();

            CodingSession? oldSession = CodingController.getSession(id);

            if (oldSession == null)
            {
                return;
            }

            string newStartTime = StartTimePrompt();

            string newEndTime = EndTimePrompt();

            int newDuration = Conversions.calculateDuration(newStartTime, newEndTime);


            CodingSession? newSession = new CodingSession(id, newStartTime, newEndTime, newDuration);

            CodingController.updateDB(newSession);

            return;
        }

        static public int IdPrompt()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>("What is the [green]ID[/] of the session you want to update?\n")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]That's not a valid ID[/]")
                .Validate(id =>
                {

                    return id switch
                    {
                        < 0 => ValidationResult.Error("[red]ID must be greater than 0[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));
        }

        static public string StartTimePrompt()
        {
            
            return AnsiConsole.Prompt(
                new TextPrompt<string>("What is the [green]start time[/] of your session? [grey](Format: yyyy-mm-dd hh:mm:ss)[/]\n")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]That's not a valid time[/]")
                .Validate(startTime =>
                {

                    bool test = Conversions.validateDateTime(startTime);

                    return test switch
                    {
                        false => ValidationResult.Error("[red]That's not a valid time.[/] Please be sure the format is correct."),
                        _ => ValidationResult.Success(),
                    };
                }));

        }


        static public string EndTimePrompt()
        {

            return AnsiConsole.Prompt(
                new TextPrompt<string>("What is the [green]end time[/] of your session? [grey](Format: yyyy-mm-dd hh:mm:ss)[/]\n")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]That's not a valid time[/]")
                .Validate(startTime =>
                {
                    bool test = Conversions.validateDateTime(startTime);

                    return test switch
                    {
                        false => ValidationResult.Error("[red]That's not a valid time.[/] Please be sure the format is correct."),
                        _ => ValidationResult.Success(),
                    };
                }));

        }



    }
}
