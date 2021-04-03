using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   

    // Update is called once per frame
    public Rigidbody2D rb;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            rb.velocity= new Vector2(-5,rb.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            rb.velocity= new Vector2(5,rb.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity= new Vector2(rb.velocity.x,10f);
        }     
    }
}
