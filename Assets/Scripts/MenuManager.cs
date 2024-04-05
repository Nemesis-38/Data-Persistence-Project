/// Summary
///     During menu phase : 
///         Display the best score
///         Get the player name     
///     On Start button click
///         Load the Main scene     
///     On Quit button click 
///         Exit the Game
/// TO DO 
///     Make a TMP To display a message when you forgot to input a name instead of the console message that is not visible by the players
///     Load the name and the score of the best player in a JSON file


using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static string playerName;
    public TMP_InputField playerEntry;
    public TextMeshProUGUI bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Instance.dataToSave.m_BestPlayerName != string.Empty)
        {
            Debug.Log("The Best player Name is not null");
            bestScoreText.text = $"Best Score : {GameData.Instance.dataToSave.m_BestPlayerName} : {GameData.Instance.dataToSave.m_BestScore}";
        }
        else // If the variables are null
        {
            Debug.Log("It's the first time you play, there's no file to load");

        }
    }

    // Update is called once per frame
    void Update()
    {
        playerName = playerEntry.text;
        // Debug.Log($"Player Name: {playerName}");
    }

    public void LoadMainScene()
    {
        //Debug.Log($"Attempting to load main scene with player name: '{playerName}' and input field text: '{playerEntry.text}'");

        // if no playerName : You must enter a name !! 
        if (!string.IsNullOrEmpty(playerName)) // && playerName != "")
        {
            SceneManager.LoadScene(1); // Load the main scene
        }
        else
        {
            // Make a TMP To display instead of the console that is not visible by the players
            Debug.Log("You must enter a name to proceed");
        }
        
    }

    public void ExitGame()
    {
        // Needs to do a conditional compiling
#if UNITY_EDITOR
        // Compiled when in Unity Editor 
        EditorApplication.ExitPlaymode();
#else
        // compiled when the app is built
        Application.Quit();
#endif
    }
}
