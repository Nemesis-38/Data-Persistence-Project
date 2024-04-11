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
        public string playerName;
        public int playerScore;
    }

    //Now I make a class that contain a list of type datatosave
    [Serializable]
    public class BestPlayerList
    {
        public List<DataToSave> dataToSave; // = new List<DataToSave>();
    }

    // Data to save
    public BestPlayerList bestPlayerList = new BestPlayerList();


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

        string json = JsonUtility.ToJson(bestPlayerList);
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
            bestPlayerList = JsonUtility.FromJson<BestPlayerList>(json);

        }
        else
        {
            bestPlayerList.dataToSave.Add(new DataToSave() { playerName = "Empty", playerScore = 0 });
            bestPlayerList.dataToSave.Add(new DataToSave() { playerName = "Empty", playerScore = 0 });
            bestPlayerList.dataToSave.Add(new DataToSave() { playerName = "Empty", playerScore = 0 });
        }
        
    }

}
