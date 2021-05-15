using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //THINGS TO SAVE
    //inventory
    //dialogue

    public static void SaveGlobalProgress(GlobalProgressChecker gpc)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/globalprog.bug";
        FileStream stream = new FileStream(path, FileMode.Create);

        GlobalProgData data = new GlobalProgData(gpc);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GlobalProgData LoadGlobalProgress()
    {
        string path = Application.persistentDataPath + "/globalprog.bug";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GlobalProgData data = formatter.Deserialize(stream) as GlobalProgData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
