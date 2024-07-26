using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    private Rigidbody2D RB;
    public float MoveValue;
    public float MoveSpeed;
    public SpriteRenderer Player;
    public float JumpValue = 7f;
    public LayerMask Ground;

    private BoxCollider2D BoxColl;
    private Animator Anim;

    private enum State
    {
        Idle,
        Running,
        Jumping,
        Falling
    }

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        //Player = GetComponent<SpriteRenderer>();
        BoxColl = GetComponent<BoxCollider2D>();
        //Anim = GetComponent<Animator>();
        MoveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveValue = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && OnGround())
        {
            RB.velocity = transform.up * JumpValue;
        }
        if((MoveValue > 0.1) || (MoveValue < -0.1))
        {
            //RB.velocity = transform.right * MoveValue * MoveSpeed * Time.deltaTime;
            RB.velocity = new Vector2(MoveValue * MoveSpeed, RB.velocity.y);
        }

        
        
    }


    //private void UpdateState()
    //{
    //    State MoveStatement;

    //    if (MoveValue > 0f)
    //    {
    //        MoveStatement = State.Running;
    //        Player.flipX = false;
    //    }
    //    else if (MoveValue < -0f) 
    //    {
    //        MoveStatement = State.Running;
    //        Player.flipX = true;
    //    }
    //    else
    //    {
    //        MoveStatement = State.Idle;
    //    }

    //    if (RB.velocity.y > 0.5f)
    //    {
    //        MoveStatement = State.Jumping;
    //    }
    //    else if(RB.velocity.y < -0.5f)
    //    {
    //        MoveStatement = State.Falling;
    //    }

    //    Anim.SetInteger("MoveStatement",(int)MoveStatement);
    //}

    private bool OnGround()
    {
        return Physics2D.BoxCast(BoxColl.bounds.center, BoxColl.bounds.size, 0f, Vector2.down, 0.5f, Ground);
    }
}
