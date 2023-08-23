using UnityEngine;

public class Logger : MonoBehaviour, ILogger
{
    public void Log(string source, string message, string color = "yellow") => DebugUtil.Log(gameObject, source, message, color);
    public void Warn(string source, string message, string color = "yellow") => DebugUtil.Warn(gameObject ,source, message, color);
    public void Error(string source, string message, string color = "yellow") => DebugUtil.Error(gameObject ,source, message, color);
}
