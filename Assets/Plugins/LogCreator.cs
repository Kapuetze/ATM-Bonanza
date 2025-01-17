using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCreator : MonoBehaviour
{
    public static LogCreator instance { get; private set; }

    public int displayedMessage = 2;

    private List<string> log = new List<string>();

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddLog(string content)
    {
        log.Add(content);
    }

    public static void Log(string content)
    {
        if(instance != null)
        {
            instance.AddLog(content);
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("clear log"))
            log.Clear();

        int limit = Mathf.Clamp(log.Count - displayedMessage, 0, log.Count - 1);

        for(int i = limit; i < log.Count; i++)
        {

            GUILayout.Label(log[i]);
        }
    }
}