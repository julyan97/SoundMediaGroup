using System.IO;
using System.Text.Json;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class JsonService : IJsonService
    {
        public T GetObjectFromJsonFile<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
