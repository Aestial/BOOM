using System;
using System.IO;
using UnityEngine;

public abstract class Loader<T> : MonoBehaviour where T : MonoBehaviour
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
    // // Call Serialize/Deserialize, that's all.
    //     byte[] bytes = MessagePackSerializer.Serialize(mc);
    //     MyClass mc2 = MessagePackSerializer.Deserialize<MyClass>(bytes);

    //     // You can dump MessagePack binary blobs to human readable json.
    //     // Using indexed keys (as opposed to string keys) will serialize to MessagePack arrays,
    //     // hence property names are not available.
    //     // [99,"hoge","huga"]
    //     var json = MessagePackSerializer.ConvertToJson(bytes);
}