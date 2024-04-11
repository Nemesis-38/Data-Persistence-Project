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
            Debug.Log("It worked !");
            // Set up the texts
            highScoreTitleText.text = "High Score";
            highScore1stText.text = $"1st\t{GameData.Instance.bestPlayerList.dataToSave[0].playerName}\t{GameData.Instance.bestPlayerList.dataToSave[0].playerScore}";
            highScore2ndText.text = $"2nd\t{GameData.Instance.bestPlayerList.dataToSave[1].playerName}\t{GameData.Instance.bestPlayerList.dataToSave[1].playerScore}";
            highScore3rdText.text = $"3rd\t{GameData.Instance.bestPlayerList.dataToSave[2].playerName}\t{GameData.Instance.bestPlayerList.dataToSave[2].playerScore}";
            yourScoreTitleText.text = "Your score";
            yourScoreText.text = $"\t{MenuManager.playerName}\t{MainManager.m_Points}";
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
        if (Input.anyKey)
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
}
