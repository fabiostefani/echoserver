namespace EchoServer;
public static class ProgramHelper
{
    public static string APP_VERSION { get; private set; }
    public static string ASPNETCORE_ENVIRONMENT { get; private set; }

    public static void ReadEnviroments()
    {
        APP_VERSION = Environment.GetEnvironmentVariable("APP_VERSION");
        ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    }
}