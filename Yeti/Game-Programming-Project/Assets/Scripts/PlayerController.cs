using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
   

    // Update is called once per frame
    //Start variables()
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    

    
    //FSM
    private enum State {idle,running,jumping,falling,hurt,climb}
    private State state=State.idle;
    

    //Ladder variables
    [HideInInspector] public bool canClimb=false;
    [HideInInspector] public bool bottomLadder=false;
    [HideInInspector] public bool topLadder=false;
    public Ladder ladder;
    private float naturalGravity;
    [SerializeField] float climbSpeed=3f;


    //Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed=5f;
    [SerializeField] private float jumpForce=15f;
   
    
    [SerializeField] private float hurtForce=10f;
    [SerializeField] private AudioSource iceCreamSound;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource hurtSound;

    private bool flip=false;
    
    private void Start()

    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        coll=GetComponent<Collider2D>();
        PermanentUI.perm.healthAmount.text=PermanentUI.perm.health.ToString();
        naturalGravity=rb.gravityScale;
    }

    private void Update()
    {
        if(state==State.climb)
        {
            Climb();
        }
        else if(state!=State.hurt)
        {
            Movement(); 
        }
        
        AnimationState();
        anim.SetInteger("state",(int)state);//sets animation based on Enumerator state
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="collectible")
        {
            Destroy(collision.gameObject);
            PermanentUI.perm.icecreams+=1;
            iceCreamSound.Play();
            PermanentUI.perm.iceCreamText.text=PermanentUI.perm.icecreams.ToString();
        }
        if(collision.tag=="PowerBar")
        {
            Destroy(collision.gameObject);
            PermanentUI.perm.isPower=true;
            iceCreamSound.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Enemy_Bear")
        {
            Enemy enemy=other.gameObject.GetComponent<Enemy>();
            
            if(state==State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state=State.hurt;
                HandleHealth();
                hurtSound.Play();
                if(other.gameObject.transform.position.x>transform.position.x)
                {
                    //Enemy is at right therefore I should be damaged and move to left
                    rb.velocity=new Vector2(-hurtForce,rb.velocity.y);
                }
                else
                {
                    //Enemy is at left therefore I should be damaged and move to right
                    rb.velocity=new Vector2(hurtForce,rb.velocity.y);
                }
            }
        }
        if(other.gameObject.tag=="Enemy_Bird")
        {
            Enemy enemy=other.gameObject.GetComponent<Enemy>();
            
            if(state==State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state=State.hurt;
                HandleHealth();
                hurtSound.Play();
                if(other.gameObject.transform.position.x>transform.position.x)
                {
                    //Enemy is at right therefore I should be damaged and move to left
                    rb.velocity=new Vector2(-hurtForce,rb.velocity.y);
                }
                else
                {
                    //Enemy is at left therefore I should be damaged and move to right
                    rb.velocity=new Vector2(hurtForce,rb.velocity.y);
                }
            }
        }
        
    }

    private void HandleHealth()
    {
        PermanentUI.perm.health-=1;
        PermanentUI.perm.isPower=false;
        PermanentUI.perm.healthAmount.text=PermanentUI.perm.health.ToString();
        if(PermanentUI.perm.health<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Movement()
    {
        float hDirection=Input.GetAxis("Horizontal");
        
        if(canClimb && Mathf.Abs(Input.GetAxis("Vertical"))>.1f)
        {
            state=State.climb;
            rb.constraints=RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            transform.position=new Vector3(ladder.transform.position.x,rb.position.y);
            rb.gravityScale=0f;
        }
        //Moving Left
        else if(hDirection<0)
        {
            
            rb.velocity= new Vector2(-speed,rb.velocity.y);
            if(flip==false)
            {
                transform.Rotate(0f,180f,0f);
                flip=true;
            }
        }
        //Moving Right
        else if(hDirection>0)
        {
            rb.velocity= new Vector2(speed,rb.velocity.y);
            if(flip==true)
            {
                transform.Rotate(0f,180f,0f);
                flip=false;
            }
        }
        //Jumping
        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity= new Vector2(rb.velocity.x,jumpForce);
        // jumpSound.Play(); 
        state=State.jumping;
    }

    private void AnimationState()
    {

        if(state==State.climb)
        {

        }
        else if(state==State.jumping)
        {
            if(rb.velocity.y<.1f)
            {
                state=State.falling;
            }
        }
        else if(state==State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state=State.idle;
            }
        }
        else if(state==State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x)<.1f)
            {
                state=State.idle;
            }
        }
        else if(Mathf.Abs(rb.velocity.x)>Mathf.Epsilon)
        {
            state=State.running;
        }
        else
        {
            state=State.idle;
        }
    }
    
    private void Footstep()
    {
        footstep.Play();
    }
    private void Climb()
    {
        if(Input.GetButtonDown("Jump"))
        {
            
            
            rb.constraints=RigidbodyConstraints2D.FreezeRotation;
            canClimb=false;
            rb.gravityScale=naturalGravity;
            anim.speed=1f;
            Jump();
            return;

        }
        float vDirection=Input.GetAxis("Vertical");
        //Climbing up
        if(vDirection>.1f && !topLadder)
        {
            rb.velocity = new Vector2(0f,vDirection*climbSpeed);
            anim.speed=1f;
        }
        else if(vDirection< -.1f && !bottomLadder)
        {
            rb.velocity=new Vector2(0f,vDirection*climbSpeed);
            anim.speed=1f;
        }
        else
        {
            
            
            rb.velocity=Vector2.zero;
            anim.speed=0f;
        }
    }
   
    
}
