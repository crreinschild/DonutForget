namespace DonutForget;

using DonutForget;
using System.CommandLine;
using Spectre.Console;

public class CommandLine
{
    TaskManager taskManager = new TaskManager();
    
    public void Main(string[] args)
    {
        taskManager.Load();
        RootCommand rootCommand = new("DonutForget CLI task management utility.")
        {
            GetAddCommand(),
            GetRemoveCommand(),
            GetListCommand(),
            GetGetCommand()
        };
        var parseResult = rootCommand.Parse(args);
        parseResult.Invoke();
    }


    // FUNCTIONS
    // TODO: Move this stuff into it's own class

    Command GetAddCommand()
    {
        Command addCommand = new("add", "Add a new task");
        Argument<string> titleArgument = new("title")
        {
            Description = "The title of the task."
        };
        Option<string> descriptionOption = new("--description", ["-d"])
        {
            Description = "The description of the task.",
            Required = false
        };
        addCommand.Arguments.Add(titleArgument);
        addCommand.Options.Add(descriptionOption);
        addCommand.SetAction(result =>
        {
            var title = result.GetValue(titleArgument);
            var description = result.GetValue(descriptionOption);
            if (!string.IsNullOrWhiteSpace(title))
            {
                taskManager.Add(title, description);
            }
        });
        return addCommand;
    }

    Command GetRemoveCommand()
    {
        Command removeCommand = new("remove", "Remove a task")
        {
            Aliases = { "rm" }
        };
        Argument<int> idArgument = new("id")
        {
            Description = "The id of the task to remove."
        };
        removeCommand.Arguments.Add(idArgument);
        removeCommand.SetAction(result =>
        {
            var id = result.GetValue(idArgument);
            taskManager.Remove(id);
        });
        return removeCommand;
    }

    Command GetListCommand()
    {
        Command listCommand = new("list", "List tasks")
        {
            Aliases = { "ls" }
        };
        listCommand.SetAction(_ =>
        {
            var tasks = taskManager.List()
                .Select(t => 
                    new Columns(
                        new Text($" ID: {t.Id.ToString()} "), 
                        new Text(t.Title)))
                .ToList();
            if (tasks.Count == 0) tasks = [new Columns(new Text(" None "))];
            var panel = new Panel(new Rows(tasks))
            {
                Header = new PanelHeader("Tasks")
            };
            AnsiConsole.Write(panel);
        });
        return listCommand;
    }

    Command GetGetCommand()
    {
        Command getCommand = new("get", "Get task by id");
        
        Argument<int> idArgument = new("id")
        {
            Description = "The id of the task to get."
        };
        getCommand.Arguments.Add(idArgument);
        getCommand.SetAction(result =>
        {
            var id = result.GetValue(idArgument);
            var task = taskManager.Get(id);
            Console.WriteLine($"{task.Id}: {task.Title}{Environment.NewLine}{task.Description}");
        });
        return getCommand;
    }
}