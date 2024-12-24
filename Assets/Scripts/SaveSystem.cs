using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static void SaveData(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/StickyConnectData.bi";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static bool IsExists()
    {
        string path = Application.persistentDataPath + "/StickyConnectData.bi";
        return File.Exists(path);
    }

    public static PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/StickyConnectData.bi";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}