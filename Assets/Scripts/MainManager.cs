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
/// TODO : Make a change player button

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text BestScoreText; // My doing
    public static string m_BestPlayerName; // I think it has to go in the MenuManager scene in order to be declared at the start of the session
    
    private bool m_Started = false;
    private int m_Points;
    public static int m_BestScore = 0;  // I think it has to go in the MenuManager scene in order to be declared at the start of the session

    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
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

        BestScoreText.text = $"Best Score : {m_BestPlayerName} : {m_BestScore}";
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    // Add point to m_Points
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    // Display the game over text
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (m_BestScore < m_Points)
        {
            m_BestScore = m_Points;
            m_BestPlayerName = MenuManager.playerName;
            BestScoreText.text = $"Best Score : {m_BestPlayerName} : {m_BestScore}";
            
            // Here you'll save the best score and bestPlayerName variable in a JSON file
        }
        
    }

    // Go Back to the menu scene
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
