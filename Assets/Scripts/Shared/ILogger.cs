using UnityEngine;

public interface ILogger
{
    void Log(string source, string message, string color = "yellow");
    void Warn(string source, string message, string color = "orange");
    void Error(string source, string message, string color = "red");
}
