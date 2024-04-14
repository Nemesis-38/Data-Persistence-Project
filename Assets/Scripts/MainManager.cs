/// Summary
/// At the Start :
///   Position the bricks
///   Display the name of the Player next to the score
/// During the game :
///   Randomly choose the direction of the ball on launch of the game (hit space bar)
///   Reload the scene on input when game over
/// Variable : 
///   m_Points
///
/// TODO : 
///     Make the Name of the player that is playing visible (you'll see the bestplayer name and the name of the player that is playing
///     Save the name and the score of the best player in a JSON file

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text BestScoreText; // My doing
    public GameObject top3Text;
    public GameObject newHighScoreText;
    Tween newHighScoreTween;
    Tween top3Tween;

    private bool m_Started = false;
    public static int m_Points;

    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        m_Points = 0;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        BestScoreText.text = $"Best Score : {GameData.Instance.bestPlayerList.dataToSave[0].playerName} : {GameData.Instance.bestPlayerList.dataToSave[0].playerScore}";
        ScoreText.text = $"{MenuManager.playerName} Score : {m_Points}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            StartCoroutine(DisplayGameOver());
        }

    }

    // Add point to m_Points
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{MenuManager.playerName} Score : {m_Points}";
    }

    // Display the game over text
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        

        // Make a conditional if the player got the 1st place
        // Make an other conditional if the player is in the top 3
        // Else nothing happen : you don't have to call the SaveGameData() method
        if (GameData.Instance.bestPlayerList.dataToSave[0].playerScore < m_Points) // if the player got the 1st place
        {
            Debug.Log("New High Score !!");

            newHighScoreText.SetActive(true);

            newHighScoreTween = newHighScoreText.transform.DOShakeScale(3, 0.2f, 5);

            UpdateTheScoreList();
            
            // Old way with only one player and score to save
            //GameData.Instance.dataToSave.m_BestScore = m_Points;
            //GameData.Instance.dataToSave.m_BestPlayerName = MenuManager.playerName;
            //BestScoreText.text = $"Best Score : {GameData.Instance.dataToSave.m_BestPlayerName} : {GameData.Instance.dataToSave.m_BestScore}";

            GameData.Instance.SaveGameData();
        }
        else if (GameData.Instance.bestPlayerList.dataToSave[2].playerScore < m_Points)
        {
            Debug.Log("You made it in the top 3 !!");

            top3Text.SetActive(true);

            top3Tween = top3Text.transform.DOShakeScale(3, 0.2f, 5);

            UpdateTheScoreList();

            GameData.Instance.SaveGameData();
        }

    }

    // Go Back to the menu scene
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator DisplayGameOver()
    {
        m_GameOver = false;

        Tween gameOverTween = GameOverText.transform.DOScale(1.5f, 3).SetEase(Ease.OutCubic);

        float countdown = 3;

        while (countdown > 0 && !Input.anyKeyDown)
        {
            countdown -= Time.deltaTime;

            yield return null;
        }

        if (gameOverTween.IsActive())
        {
            gameOverTween.Kill();
        }

        if (newHighScoreTween.IsActive())
        {
            newHighScoreTween.Kill();
        }

        if (top3Tween.IsActive())
        {
            top3Tween.Kill();
        }
        
        SceneManager.LoadScene(2);
    }


    void UpdateTheScoreList()
    {
        // Add to the list (new DataToSave item (player name & player score) in bestPlayerList)
        GameData.Instance.bestPlayerList.dataToSave.Add(new GameData.DataToSave() { playerName = MenuManager.playerName, playerScore = m_Points });

        // Sort the list
        GameData.Instance.bestPlayerList.dataToSave.Sort(GameData.ComparePlayerScoreDescendingOrder); ; // You must pass a delegate in the sort function that tells it how to compare two elements of dataToSave.

        // Remove last item from list
        GameData.Instance.bestPlayerList.dataToSave.RemoveAt(3);
    }

}
