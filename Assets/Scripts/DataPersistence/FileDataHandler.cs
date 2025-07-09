using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;





public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string _dataDirPath, string _dataFileName)
    {
        dataDirPath = _dataDirPath;
        dataFileName = _dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception)
            {
                UnityEngine.Debug.LogError($"An error occured when loading the save file: {fullPath} \n");
            }
        }
        return loadedData;

    }

    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(_data, true);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                { 
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception)
        {
            UnityEngine.Debug.LogError($"An error occured when writing data to the save file: {fullPath} \n");
        }
    }

}
