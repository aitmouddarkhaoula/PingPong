using System;
using DG.Tweening;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Score : MonoBehaviour
{
    public PaddleController paddleController;
    public int highScore;
    public TextMeshProUGUI scoreText;


    public int score;

    private void Start()
    {
        ResetScore();
    }

    
    public void UpdateUI(int playerScore)
    {
        scoreText.transform.DOScale(1.4f, 0.2f).OnComplete(() => scoreText.transform.DOScale(1f, 0.2f));
        scoreText.text = " " + playerScore + "";
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public bool IsMaxScore()
    {
        return score >= GameManager.Instance.maxScore;
    }

    public void RemoveScore(int amount)
    {
        score -= amount;
        if (score < 0) score = 0;

        scoreText.text = "" + score + "";
        //scoreText2.text = "" + score;
    }

    public void ResetScore()
    {
        score = 0;
        highScore = 0;
        UpdateUI(0);
        //scoreText2.text = "";
        //highScoreText.text = "";
    }
}

