using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_APath : Enemy
{
    //[SerializeField] private float leftCap=202f;
    //[SerializeField] private float rightCap=213f;
    [SerializeField] private LayerMask ground;
    //[SerializeField] private float jumpLength = 5;
    //[SerializeField] private float jumpHeight = 0;
    


    private Collider2D coll;
    //private bool facingLeft = true;
    
    

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        
        
    }

    
    
}
