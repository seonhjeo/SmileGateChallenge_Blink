using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class FileIOHelper
{
    public void CreateJsonFile(string path, string fName, string json)
    {
        FileStream stream = new FileStream(string.Format("{0}/{1}.json", path, fName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json);
        stream.Write(data, 0, data.Length);
        stream.Close();
    }

    public T LoadJsonFile<T>(string path, string fName)
    {
        FileStream stream = new FileStream(string.Format("{0}/{1}.json", path, fName), FileMode.Open);
        if(stream != null)
        {
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<T>(jsonData);
        }
        return default(T);
    }
}
