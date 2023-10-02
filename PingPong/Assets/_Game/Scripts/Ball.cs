using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.left*50, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision2D other)
    {
            if (other.gameObject.CompareTag("Player"))
            {
                rb.AddForce(Vector3.left*50, ForceMode.Impulse);
            }
            if(other.gameObject.CompareTag("Player2"))
            {
                rb.AddForce(Vector3.right*50, ForceMode.Impulse);
            }
        
    }
}
