# Donut Forget

A simple cross-platform CLI task management application.

### Features

- Add tasks
- Remove tasks
- List tasks
- Get task details

### Upcoming Features (as I need them in no particular order)

- Mark tasks done
- Update tasks
- List filters; see done tasks
- Get task metrics (so I can add task counts to my custom prompt)
- Task prioritization/ordering
- Task due-dates and deadlines
- Task linking and grouping (sub-tasks, mark task as a blocker of another)
- Task Comment threads
- Task History
- Better CLI graphics (color coding)
- Alternative storage backends
- Interactive mode for organizing tasks in bulk
- Daemon mode to reduce storage reads & support background features
- ... other things I think of to add

## Prerequisites

.NET SDK 9.x

> https://dotnet.microsoft.com/en-us/download/dotnet/9.0

Eventually I might add executables in releases, at which point 
.NET Runtime 9.x will be sufficient.

## Setup (dev)

Open clone and open in your favorite dotnet IDE and publish the app for your architecture & OS. Or...

1. Clone and `cd` into the `DonutForget` solution and the `DonutForget` project.
2. `dotnet publish` (when done this way)
3. Copy the published files to somewhere in your PATH or add the publish-directory to your PATH.

## Usage

```
$ donutforget add "Task Title" --description "Description of the task"

$ donutforget list
┌─Tasks─────────────────┐
│ ID: 0   Task Title    │
└───────────────────────┘

$ donutforget get 0
4: Task Title
Description of the task

$ donutforget remove 0

$ donutforget list
┌─Tasks──┐
│  None  │
└────────┘
```

### Tips

#### Alias

```pwsh
Set-Alias -Name donut -Value donutforget
```

