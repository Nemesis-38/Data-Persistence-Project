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


    // Start is called before the first frame update
    void Start()
    {
        if (GameData.Instance != null)
        {
            Debug.Log("It worked !");
            // Set up the texts
            highScoreTitleText.text = "High Score";
            highScore1stText.text = $"1st\t{GameData.Instance.dataToSave.m_BestPlayerName}\t{GameData.Instance.dataToSave.m_BestScore}";
            highScore2ndText.text = "2nd\t(Playername)\tscore";
            highScore3rdText.text = "3rd\t(Playername)\tscore";
            yourScoreTitleText.text = "Your score";
            yourScoreText.text = $"\t{MenuManager.playerName}\tscore";
        }
        else
        {
            Debug.Log("GameData.Instance is null, So you couldn't use its variable");
        }
        


        // Animate the texts
        highScoreTitleText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1).From();
        highScore1stText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.1f).From();
        highScore2ndText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.2f).From();
        highScore3rdText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.3f).From();
        yourScoreTitleText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.4f).From();
        yourScoreText.rectTransform.DOAnchorPosX(1300f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.5f).From();
        menuButton.transform.DOLocalMoveX(1300f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.6f).From();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
