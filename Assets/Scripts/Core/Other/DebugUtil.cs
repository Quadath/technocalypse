using UnityEngine;

public static class DebugUtil
{
    // ReSharper disable Unity.PerformanceAnalysis
    public static void Log(Object sender, string source, string message, string color = "yellow")
	{
		if (sender == null)
        {
            Debug.Log($"<color={color}>[NULL]</color> {message}");
            return;
        }

        string objName = sender.name;
        int id = sender.GetInstanceID();

        Debug.Log($"<color=grey>[{objName} | ID={id}]</color> <color={color}>[{source}]</color> {message}", sender);
	}
    // ReSharper disable Unity.PerformanceAnalysis
    public static void Log(string source, string message, string color = "yellow")
	{
        Debug.Log($"<color={color}>[{source}]</color> {message}");
	}

    public static void Warn(string source, string message, string color = "orange")
    {
        Debug.LogWarning($"<color={color}>[{source}]</color> {message}");
    }
    public static void Warn(Object sender, string source, string message, string color = "orange")
    {
        if (sender == null)
        {
            Debug.LogWarning($"[NULL] {message}");
            return;
        }
        
        string objName = sender.name;
        int id = sender.GetInstanceID();

        Debug.LogWarning($"<color=grey>[{objName} | ID={id}]</color> <color={color}>[{source}]</color> {message}", sender);
    }

    public static void Error(string source, string message, string color = "red")
    {
        Debug.LogWarning($"<color={color}>[{source}]</color> {message}");
    }
    
    public static void Error(Object sender, string source, string message, string color = "red")
    {
        if (sender == null)
        {
            Debug.LogError($"[NULL] {message}");
            return;
        }
        
        string objName = sender.name;
        int id = sender.GetInstanceID();

        Debug.LogError($"<color=grey>[{objName} | ID={id}]</color> <color={color}>[{source}]</color> {message}", sender);
    }
}