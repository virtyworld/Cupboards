using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private GameData data;
    private string file = "player.txt";

    public GameData Data => data;

    public void Load()
    {
        data = new GameData();
        string json = ReadFromFIle(file);
        if (json!= null)JsonUtility.FromJsonOverwrite(json, data);

    }

    public void Save()
    {
        string json = JsonUtility.ToJson(data);
        WriteToFile(file, json);
    }

    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFIle(string fileName)
    {
        string path = Application.dataPath + "/" +fileName;
      
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.LogWarning("File not found");
            return null;
        }
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) Save();
    }
}