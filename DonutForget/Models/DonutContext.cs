using System.Text.Json.Serialization;

namespace DonutForget;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(TaskList))]
public partial class DonutContext : JsonSerializerContext
{
    
}