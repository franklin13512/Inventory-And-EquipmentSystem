using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class Move : MonoBehaviour
{

    private Rigidbody2D RB;
    public float MoveValue;
    public float MoveSpeed;
    public SpriteRenderer PlayerBody;
    public SpriteRenderer PlayerHead;
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
            UpdateState();
        }

    }


    private void UpdateState()
    {
        if (MoveValue > 0f)
        {
            PlayerBody.flipX = false;
            PlayerHead.flipX = false;
            //Debug.Log("no turn around");
        }
        else if (MoveValue < -0f)
        {
            PlayerBody.flipX = true;
            PlayerHead.flipX = true;
            //Debug.Log("turn around");
        }

    }

    private bool OnGround()
    {
        return Physics2D.BoxCast(BoxColl.bounds.center, BoxColl.bounds.size, 0f, Vector2.down, 0.5f, Ground);
    }
}
