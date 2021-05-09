using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Enemy
{
    [SerializeField] private float leftCap=202f;
    [SerializeField] private float rightCap=213f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float jumpLength = 5;
    [SerializeField] private float jumpHeight = 0;
    


    private Collider2D coll;
    private bool facingLeft = true;
    
    

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        
        
    }

    private void Update()
    {
        if(facingLeft)
        {
            //ensures sprite is facing the correct way

            if (transform.position.x > leftCap)
            {

                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                //test to see if frog is on ground, if so jump
                
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                
            }
            else
            {
                facingLeft = false;
            }

            //if it is not greater then frog will face right
        }

        else
        {
            
            
                //check if the x value is greater than the leftCap

                //ensures sprite is facing the correct way


                if (transform.position.x < rightCap)
                {
                    //test to see if frog is on ground, if so jump
                    if (transform.localScale.x != -1)
                    {
                        transform.localScale = new Vector3(-1, 1);
                    }


                   
                        rb.velocity = new Vector2(jumpLength, jumpHeight);

                    
            }   
                 
                else
                {
                    facingLeft = true;
                }
            



        }
    }
    
}
