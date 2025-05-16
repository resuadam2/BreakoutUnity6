using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    private static int record = 0;
    private static string path = Application.persistentDataPath + "/save.dat";

    public static bool SaveRecord(int points)
    {
        if (points > record)
        {
            record = points;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            RecordData data = new RecordData(record);

            binaryFormatter.Serialize(stream, data);
            stream.Close();
            return true;
        } else
        {
            return false;
        }
    }

    public static int LoadRecord()
    {
        if (!File.Exists(path))
        {
            return 0;
        }
        else
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RecordData data = binaryFormatter.Deserialize(stream) as RecordData;
            record = data.GetRecord();
        }
        return record;
    }
}
