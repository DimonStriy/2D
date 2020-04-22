using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : Character
{

    private Rigidbody2D myRigidbody;

    private bool slide;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool attack;

    private bool jump;

    private bool jumpAttack;

    private bool throv;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;
    internal static object Instance;

    public new bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }


    // Use this for initialization
    public override void Start()
    {
        base.Start();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleAttacks();

        HandleLayers();

        ResetValues();

        isGrounded = IsGrounded();
    }

    private void HandleMovement(float horizontal)
    {
        if (myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }

        if (!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
        }


        if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", true);
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", false);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleAttacks()
    {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("attack");
            myRigidbody.velocity = Vector2.zero;
        }
        if(jumpAttack && !isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            myAnimator.SetBool("jumpAttack", true);
        }
        if (!jumpAttack && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            myAnimator.SetBool("jumpAttack", false);
        }
        if (throv && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Throw"))
        {
            myAnimator.SetTrigger("throv");
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
            jumpAttack = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            throv = true;
            ThrowKnife(0);
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
    private void ResetValues()
    {
        attack = false;
        slide = false;
        jump = false;
        jumpAttack = false;
        throv = false;
    }

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void ThrowKnife(int value)
    {
        base.ThrowKnife(value);
    }

    public override IEnumerator TakeDamage()
    {
        yield return null;
    }
}
