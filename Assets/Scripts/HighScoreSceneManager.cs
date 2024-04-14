using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreSceneManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreTitleText;
    public TextMeshProUGUI highScore1stText;
    public TextMeshProUGUI highScore2ndText;
    public TextMeshProUGUI highScore3rdText;
    public TextMeshProUGUI yourScoreTitleText;
    public TextMeshProUGUI yourScoreText;
    public GameObject menuButton;

    //Sequence mySequence = DOTween.Sequence();
    Sequence mySequence = DOTween.Sequence();

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Instance != null)
        {
            // Get the maxLengthOfName
            int biggestNameLength = 0;
            foreach (GameData.DataToSave player in GameData.Instance.bestPlayerList.dataToSave)
            {
                if (biggestNameLength < player.playerName.Length)
                {
                    biggestNameLength = player.playerName.Length;
                }
            }

            if (biggestNameLength < MenuManager.playerName.Length)
            {
                biggestNameLength = MenuManager.playerName.Length;
            }

            Debug.Log("biggest name length : " + biggestNameLength);

            // Display the different text in the Highscore 
            highScoreTitleText.text = "High Score";
            highScore1stText.text = FormatHighScoreText("1st", GameData.Instance.bestPlayerList.dataToSave[0].playerName, GameData.Instance.bestPlayerList.dataToSave[0].playerScore, biggestNameLength); // $"1st\t{GameData.Instance.bestPlayerList.dataToSave[0].playerName}\t{GameData.Instance.bestPlayerList.dataToSave[0].playerScore}";
            highScore2ndText.text = FormatHighScoreText("2nd", GameData.Instance.bestPlayerList.dataToSave[1].playerName, GameData.Instance.bestPlayerList.dataToSave[1].playerScore, biggestNameLength); // $"2nd\t{GameData.Instance.bestPlayerList.dataToSave[1].playerName}\t{GameData.Instance.bestPlayerList.dataToSave[1].playerScore}";
            highScore3rdText.text = FormatHighScoreText("3rd", GameData.Instance.bestPlayerList.dataToSave[2].playerName, GameData.Instance.bestPlayerList.dataToSave[2].playerScore, biggestNameLength); // $"3rd\t{GameData.Instance.bestPlayerList.dataToSave[2].playerName}\t{GameData.Instance.bestPlayerList.dataToSave[2].playerScore}";
            yourScoreTitleText.text = "Your score";
            yourScoreText.text = FormatHighScoreText("---", MenuManager.playerName, MainManager.m_Points, biggestNameLength); // $"\t{MenuManager.playerName}\t{MainManager.m_Points}";
        }
        else
        {
            Debug.Log("GameData.Instance is null, So you couldn't use its variable");
        }

        // Animate the texts
        mySequence
            .Append(highScoreTitleText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).From())
            .Insert(0.1f, highScore1stText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).From())
            .Insert(0.2f, highScore2ndText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).From())
            .Insert(0.3f, highScore3rdText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).From())
            .Insert(0.4f, yourScoreTitleText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).From())
            .Insert(0.5f, yourScoreText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).From())
            .Insert(0.6f, menuButton.transform.DOLocalMoveX(1300f, 0.3f).SetEase(Ease.InOutQuad).From())
            .PrependInterval(0.2f);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (mySequence.IsActive())
            {
                mySequence.Kill();
            }

            SceneManager.LoadScene(0);
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public string FormatHighScoreText(string label, string playerName, int playerScore, int lengthOfName)
    {
        return $"{label}\t{playerName.PadRight(lengthOfName, ' ')}{" "}\t{playerScore}";
    }
}
