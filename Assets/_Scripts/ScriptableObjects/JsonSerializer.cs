using System;
using System.IO;
using UnityEngine;

public abstract class JsonSerializer<T> : ScriptableObject where T : ScriptableObject
{    
    string filePath;
    virtual protected void Awake()
    {

    }
    protected void SetFilePath(string fileName)
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log("File path: " + filePath);
    }
    protected void SetEmptyFilePath()
    {
        if (String.IsNullOrEmpty(filePath))
            SetFilePath(name+".json");
    }
    protected void Save(object obj)
    {
        SetEmptyFilePath();
        string json = JsonUtility.ToJson(obj);
        File.WriteAllText(filePath, json);
        Debug.Log("Json saved into file: " + json);
    }
    
    protected U GetFromFileOrCreate<U>(Func<U> createFunction)
    {        
        SetEmptyFilePath();
        if(File.Exists(filePath))
        {
            Debug.Log("File exists: " + filePath);  
            return Load<U>();
        }   
        Debug.Log("File doesn't exist: " + filePath);  
        return createFunction();
    }

    protected U Load<U>()
    {
        Debug.Log( typeof(U).ToString() + " is loading from file...");
        string json = File.ReadAllText(filePath);
        Debug.Log("Json loaded. Value: " + json);
        return JsonUtility.FromJson<U>(json);
    }
}