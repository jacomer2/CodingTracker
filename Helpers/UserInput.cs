using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTracker.Helpers;

namespace CodingTracker.Helpers
{
    class UserInput
    {

        static public string MainMenu()
        {
            
            return AnsiConsole.Prompt(
                new TextPrompt<string>("What is the [green]start time[/] of your session? [grey](Format: yyyy-mm-dd hh:mm:ss)[/]\n")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]That's not a valid time[/]")
                .Validate(startTime =>
                {
                    bool test = ValidateInput.validateDateTime(startTime);

                    return test switch
                    {
                        false => ValidationResult.Error("[red]That's not a valid time.[/] Please be sure the format is correct."),
                        _ => ValidationResult.Success(),
                    };
                }));

        }

    }
}
