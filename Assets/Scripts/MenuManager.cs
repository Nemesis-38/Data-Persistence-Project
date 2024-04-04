/// Summary
///     During menu phase : 
///         Display the best score
/// DONE    Get the player name     
///     On Start button click
/// DONE    Load the Main scene     
///     On Quit button click 
///         Exit the Game

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
        // if the variable are not null (this will prevent from fetching the file if we juste got back to the menu with my new "change player" button
        if (MainManager.m_BestPlayerName != null) // && file exists
        {
            Debug.Log("The Best player Name is not null");
            bestScoreText.text = $"Best Score : {MainManager.m_BestPlayerName} : {MainManager.m_BestScore}";
        }
        else // If the variables are null
        {
            // First you'll load the MainManager.bestScore and .bestPlayerName variable from a JSON file

            // Then you'll display : 
            // bestScoreText.text = $"Best Score : {MainManager.m_BestPlayerName} : {MainManager.m_BestScore}";
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
            // Make a TMP To display instead of the consol that is not visible by the players
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
