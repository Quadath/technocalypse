using UnityEngine;

public class Logger : MonoBehaviour, ILogger
{
    public void Log(string message)
    {
        Debug.Log(message);
    }
}
