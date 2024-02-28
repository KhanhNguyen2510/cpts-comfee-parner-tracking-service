namespace Domain.Extensions
{
    public class EnvironmentExtension
    {
        public static string GetAppConnectionString() =>
            Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? string.Empty;
    }
}
