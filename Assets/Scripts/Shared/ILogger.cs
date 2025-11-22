using UnityEngine;

public interface ILogger
{
    void Log(string message);
}
public enum DebugMessageType { Log, Warn, Error }