using UnityEngine;

public static class DebugUtil
{
	public static void Log(Object sender, string source, string message, string color = "yellow")
	{
		if (sender == null)
        {
            Debug.Log($"<color={color}>[NULL]</color> {message}");
            return;
        }

        string objName = sender.name;
        int id = sender.GetInstanceID();

        Debug.Log($"<color={color}>[{objName} | ID={id}]</color> <color=cyan>[{source}]</color> {message}", sender);
	}
	public static void Log(string source, string message, string color = "yellow")
	{
        Debug.Log($"<color={color}>[{source}]</color> {message}");
	}

    public static void Warn(Object sender, string message)
    {
        if (sender == null)
        {
            Debug.LogWarning($"[NULL] {message}");
            return;
        }

        Debug.LogWarning($"[{sender.name} | ID={sender.GetInstanceID()}] {message}", sender);
    }

    public static void Error(Object sender, string message)
    {
        if (sender == null)
        {
            Debug.LogError($"[NULL] {message}");
            return;
        }

        Debug.LogError($"[{sender.name} | ID={sender.GetInstanceID()}] {message}", sender);
    }
}