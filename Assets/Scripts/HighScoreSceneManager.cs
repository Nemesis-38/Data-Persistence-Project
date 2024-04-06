using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreSceneManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreTitleText;
    public TextMeshProUGUI highScore1stText;
    public TextMeshProUGUI highScore2ndText;
    public TextMeshProUGUI highScore3rdText;
    public TextMeshProUGUI yourScoreTitleText;
    public TextMeshProUGUI yourScoreText;

    //Sequence mySequence = DOTween.Sequence();


    // Start is called before the first frame update
    void Start()
    {
        // Set up the texts
        highScoreTitleText.text = "High Score";
        highScore1stText.text = "1st\t(Playername)\tscore";
        highScore2ndText.text = "2nd\t(Playername)\tscore";
        highScore3rdText.text = "3rd\t(Playername)\tscore";
        yourScoreTitleText.text = "Your score";
        yourScoreText.text = "\t(Playername)\tscore";


        // Animate the texts
        highScoreTitleText.rectTransform.DOAnchorPosX(1200f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1).From();
        highScore1stText.rectTransform.DOAnchorPosX(1200f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.1f).From();
        highScore2ndText.rectTransform.DOAnchorPosX(1200f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.2f).From();
        highScore3rdText.rectTransform.DOAnchorPosX(1200f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.3f).From();
        yourScoreTitleText.rectTransform.DOAnchorPosX(1200f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.4f).From();
        yourScoreText.rectTransform.DOAnchorPosX(1200f, 0.3f).SetEase(Ease.InOutQuad).SetDelay(1.5f).From();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}