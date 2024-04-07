using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



public class GameData : MonoBehaviour
{
    // Persistent instance of the class
    public static GameData Instance;

    [Serializable]
    public class DataToSave
    {
        public string m_BestPlayerName;
        public int m_BestScore;
    }

    // Data to save
    public DataToSave dataToSave = new DataToSave();

    // Variable

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
#if UNITY_EDITOR_WIN
    string filePath = "D:/Unity Save data files/Data Persistence Project/saveFile.json";
#else
        string filePath = Application.persistentDataPath + "/saveFile.json";
#endif

        string json = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(filePath, json);
    }

    public void InitializeSingleton()
    {
        Instance = this;

#if UNITY_EDITOR_WIN
        string filePath = "D:/Unity Save data files/Data Persistence Project/saveFile.json";
#else
        string filePath = Application.persistentDataPath + "/saveFile.json";
#endif

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            dataToSave = JsonUtility.FromJson<DataToSave>(json);
            
        }
        
    }

}
