using DG.Tweening;
using System;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

    public class Score : NetworkBehaviour
    {
        public int score=0;
        public int highScore;

    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI scoreText2;
    //public TextMeshProUGUI highScoreText;

    private void Start()
        {
            ResetScore();
        }


        public void AddScore(int amount)
        {
            score += amount;
            if (score > highScore)
            {
                //highScore = score;
                //highScoreText.text = "" + highScore;
            }
            scoreText.transform.DOScale(1.4f, 0.2f).OnComplete(() => scoreText.transform.DOScale(1f, 0.2f));
            scoreText.text = " " + score + "";
            //scoreText2.text = "" + score;
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
            scoreText.text = ""+0 + "";
            //scoreText2.text = "";
            //highScoreText.text = "";
        }




    }
