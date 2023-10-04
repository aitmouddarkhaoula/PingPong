using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PaddleController player;
    [SerializeField] List<PaddleController> playersList;

    [SerializeField] private Score score0;
    [SerializeField] private Score score1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
       // playersList.AddRange(GameObject.FindObjectsOfType<PaddleController>()); 
    }

    public void Init()
    {
        rb.AddForce(Vector3.down*10, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb.AddForce(Vector3.up, ForceMode2D.Impulse);
        }
        if(other.gameObject.CompareTag("Player2"))
        {
            rb.AddForce(Vector3.down, ForceMode2D.Impulse);
        }
        if(other.gameObject.CompareTag("UpWall"))
        {
            score0.AddScore(1);
        }
        if(other.gameObject.CompareTag("DownWall"))
        {
            score1.AddScore(1);
        }
    }

    public void Reset()
    {
        
    }
}
