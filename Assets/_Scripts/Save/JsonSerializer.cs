using System;
using System.IO;
using UnityEngine;

public abstract class JsonSerializer<T> : MonoBehaviour where T : MonoBehaviour
{    
    [SerializeField] protected string fileName;
    string filePath;
    protected void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(filePath);
    }
    protected void SetFilePath(string fileName)
    {
        this.fileName = fileName;
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(filePath);
    }
    protected void Save(object obj)
    {
        string json = JsonUtility.ToJson(obj);
        File.WriteAllText(filePath, json);
        Debug.Log(json);
    }
    protected U GetFromFileOrCreate<U>(Func<U> createFunction)
    {        
        return File.Exists(filePath) ? Load<U>() : createFunction();
    }
    protected U Load<U>()
    {
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<U>(json);
    }
}