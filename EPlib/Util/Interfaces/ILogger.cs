namespace EPlib.Util.Interfaces
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message);
        void LogGeneral(string message);
    }
}
