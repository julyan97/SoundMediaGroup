namespace WebApplication1.Services.Interfaces
{
    public interface IJsonService
    {
        T GetObjectFromJsonFile<T>(string filePath);
    }
}
