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


using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject blueArrow;
    public static string playerName;
    public TMP_InputField playerEntry;
    public TextMeshProUGUI bestScoreText;
    public bool inputFieldIsAnimated;
    public Tween inputFieldTween;
    private Tween blueArrowTween;
    private Coroutine displayBlueArrowCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        // If you already have entered a playername, put it in the input field
        if (!string.IsNullOrEmpty(playerName))
        {
            playerEntry.text = playerName;
        }

        // I think I will be able to remove this conditional since even if I never played, I initialized the list with player 1 2 3 and score zero to each of them...
        // So there will always be something to display
        if (GameData.Instance.bestPlayerList.dataToSave[0].playerName != string.Empty) // If there is best player
        {
            Debug.Log("The Best player Name is not null");
            bestScoreText.text = $"Best Score : {GameData.Instance.bestPlayerList.dataToSave[0].playerName} : {GameData.Instance.bestPlayerList.dataToSave[0].playerScore}"; // display its name and score
        }
        else // If there is no best player
        {
            Debug.Log("It's the first time you play, there's no file to load");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadMainScene();
        }

        if (EventSystem.current.currentSelectedGameObject != playerEntry.gameObject && !inputFieldIsAnimated && string.IsNullOrEmpty(playerEntry.text))
        {
            Debug.Log("Animate the input field");
            inputFieldTween = playerEntry.transform.DOScale(2.5f, 0.5f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
            
            inputFieldIsAnimated = true;
        }
        else if (EventSystem.current.currentSelectedGameObject == playerEntry.gameObject && inputFieldIsAnimated)
        {
            Debug.Log("put the object to default state and stop to animate");
            inputFieldTween.Rewind();

            inputFieldIsAnimated = false;
        }
    }


    public void LoadMainScene()
    {
        playerName = playerEntry.text;

        if (!string.IsNullOrEmpty(playerName)) // If player name is not null or empty
        {
            if (displayBlueArrowCoroutine != null)
            {
                StopCoroutine(displayBlueArrowCoroutine);
                displayBlueArrowCoroutine = null;

                if (blueArrowTween.IsActive())
                {
                    blueArrowTween.Kill();
                }
            }

            SceneManager.LoadScene(1); // Load the main scene
        }
        else // If player name is null or empty
        {
            Debug.Log("You must enter a name to proceed");
            if (displayBlueArrowCoroutine == null)
            {
                displayBlueArrowCoroutine = StartCoroutine(DisplayBlueArrow(3));
            }
        }
        
    }

    IEnumerator DisplayBlueArrow(float time)
    {
        blueArrow.SetActive(true);
        blueArrowTween = blueArrow.transform.DOScaleX(1.5f, 0.5f).SetEase(Ease.InOutCubic).SetLoops(-1,LoopType.Yoyo);

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        blueArrowTween.Kill();
        blueArrow.SetActive(false);

        displayBlueArrowCoroutine = null;

    }

    public void LoadHighScoreScene()
    {
        playerName = playerEntry.text;

        inputFieldTween.Kill();

        // if no playerName : You must enter a name !! 
        if (!string.IsNullOrEmpty(playerName)) // && playerName != "")
        {
            SceneManager.LoadScene(2); // Load the main scene
        }
        else
        {
            // Make a TMP To display instead of the console that is not visible by the players
            // playerName = "No name entered";
            SceneManager.LoadScene(2);
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
