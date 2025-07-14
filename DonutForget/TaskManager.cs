namespace DonutForget;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TaskManager
{
    private const string AppName = "donutforget";
    private TaskList _taskList = new();

    public TaskManager()
    {
    }
    
    public void Add(string title, string? description = null)
    {
        // get max id
        // TODO: keep a running ID or change to GUID
        var max = _taskList.Tasks.Any() ? _taskList.Tasks.Max(t => t.Id) : 0;
        _taskList.Tasks.Add(new Task {  Id = max+1, Title = title, Description = description });
        Save();
    }

    public Task Get(int id)
    {
        return _taskList.Tasks.First(t => t.Id == id);
    }

    public List<Task> List()
    {
        return _taskList.Tasks;
    }

    public void Remove(int id)
    {
        _taskList.Tasks.RemoveAll(t => t.Id == id);
        Save();
    }

    public void Save()
    {
        // TODO: move this to a storage handler/provider for swapping storage mechanisms
        using var file = GetConfigFile();
        file.SetLength(0);
        file.Flush();
        JsonSerializer.Serialize(file, _taskList, typeof(TaskList), DonutContext.Default);
    }

    public void Load()
    {
        using var file = GetConfigFile();
        try
        {
            _taskList = JsonSerializer.Deserialize(file, typeof(TaskList), DonutContext.Default) as TaskList ?? new TaskList();
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to load task list");
            Console.WriteLine(e);
            _taskList = new TaskList();
        }
    }
    
    static FileStream GetConfigFile()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);
        var appDir = Path.Combine(appData, AppName);
        var dirInfo = Directory.CreateDirectory(appDir);
        var storeFile = Path.Combine(dirInfo.FullName, $"{AppName}.json");
        return File.Open(storeFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }
}