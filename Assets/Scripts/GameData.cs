using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class DataToSave
{
    public string m_BestPlayerName;
    public int m_BestScore;
}

public class GameData : MonoBehaviour
{
    // Persistent instance of the class
    public static GameData Instance;

    // Data to save
    public DataToSave dataToSave = new DataToSave();

    // Awake is called before the Start method (and even if the script isn't loaded)
    // Awake is made for initialization. Start can then initialize things that need class or things that were previously initialize in the awake method
    void Awake()
    {
        if (Instance == null)
        {
            InitializeSingleton();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SaveGameData()
    {
        string saveFilePath = "D:/Unity Save data files/Data Persistence Project/saveFile.json";
        string json = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(saveFilePath, json);
    }

    public void InitializeSingleton()
    {
        Instance = this;
        string loadFilePath = "D:/Unity Save data files/Data Persistence Project/saveFile.json";

        if (File.Exists(loadFilePath))
        {
            string json = File.ReadAllText(loadFilePath);
            dataToSave = JsonUtility.FromJson<DataToSave>(json);
            
        }
        
    }

}
