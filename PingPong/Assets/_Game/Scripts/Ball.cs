using System;
using System.Collections.Generic;
using DG.Tweening;
using GameSystems;
using Oknaa.Scripts;
using UnityEngine;

public class Ball : Singleton<Ball>
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PaddleController player;
    [SerializeField] List<PaddleController> playersList;

    [SerializeField] private Score score0;
    [SerializeField] private Score score1;
    [SerializeField] private Vector3 _ballScale;
    [SerializeField] private TrailRenderer _ballTrail;
    
  

    public Action<int, int> OnScoreChanged;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        _ballScale = transform.localScale;
        _ballTrail= GetComponent<TrailRenderer>();
        score0 = GameManager.Instance.score0;
        score1 = GameManager.Instance.score1;
        
       // playersList.AddRange(GameObject.FindObjectsOfType<PaddleController>()); 
    }

    public void Init()
    {
        transform.DOScale(_ballScale, 0.7f).SetEase(Ease.OutBack).onComplete += () =>
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(  Vector3.down * 10, ForceMode2D.Impulse);
            _ballTrail.emitting = true;
        };
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       
        if(other.gameObject.CompareTag("UpWall"))
        {
            score0.AddScore(1);
            OnScoreChanged?.Invoke(score0.score, score1.score);
            ReSpawn();
        }
        if(other.gameObject.CompareTag("DownWall"))
        {
            score1.AddScore(1);
            OnScoreChanged?.Invoke(score0.score, score1.score);
            ReSpawn(true);
        }
    }
    public void UpdateUI(int playerScore1, int playerScore2)
    {
        bool isServer = GameManager.Instance.serverManager.IsServer;
        score0.UpdateUI(isServer?playerScore1: playerScore2);
        score1.UpdateUI(isServer?playerScore2: playerScore1);
        if (score0.IsMaxScore())
        {
            GameManager.Instance.Reset();
            score0.ResetScore();
            score1.ResetScore();
            if(isServer)
                GameStateSystem.SetState(GameState.GameWon);
            else
                GameStateSystem.SetState(GameState.GameOver);
        }

        
        if (score1.IsMaxScore())
        {
            GameManager.Instance.Reset();
            score0.ResetScore();
            score1.ResetScore();
            if(isServer)
                GameStateSystem.SetState(GameState.GameOver);
            else
                GameStateSystem.SetState(GameState.GameWon);
        }
    }


    public void ReSpawn(bool up =false)
    {
        _ballTrail.Clear();
        _ballTrail.emitting = false;
        rb.velocity = Vector3.zero;
        transform.DOScale(Vector3.zero, 0.4f).onComplete += () =>
        {
            transform.position = Vector3.zero;
            transform.DOScale(_ballScale, 0.7f).SetEase(Ease.OutBack).onComplete += () =>
            {
                rb.AddForce(up ? Vector3.up * 10 : Vector3.down * 10, ForceMode2D.Impulse);
                _ballTrail.emitting = true;
            };
        };
    }
    public void Reset()
    {
        _ballTrail.Clear();
        _ballTrail.emitting = false;
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Static;
        transform.DOScale(Vector3.zero, 0.4f).onComplete += () =>
        {
            transform.position = Vector3.zero;
        };
    }
}
