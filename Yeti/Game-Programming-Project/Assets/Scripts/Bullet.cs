using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed=20f;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right*speed; 
        Destroy(gameObject,0.6f);   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Enemy_Bear")
        {
            Enemy enemy=other.gameObject.GetComponent<Enemy>();
            enemy.JumpedOn();
        }
        else if(other.gameObject.tag=="Enemy_Bird")
        {
            Enemy enemy=other.gameObject.GetComponent<Enemy>();
            enemy.JumpedOn();
        }    
    }

    
}
