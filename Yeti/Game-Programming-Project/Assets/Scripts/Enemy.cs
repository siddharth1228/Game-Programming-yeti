using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected Animator anim;
    protected Rigidbody2D rb;
    protected AudioSource death;

    protected virtual void Start()
    {
        anim=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        death=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void JumpedOn()
    {
        anim.SetTrigger("Death");
        death.Play();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
