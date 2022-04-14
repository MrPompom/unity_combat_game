using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Player2dmovement : Character
{
    public float MovementSpeed = 300;
    public float JumpForce = 100;
    public float TimeBetweenAttack;
    public float AttackTime;
    public Animator animator;
    Rigidbody2D rigidebody;
    bool FacingRight = true;
    float moveX;
    public bool isGrounded;
    public bool canMoving;
    public Transform checkGround;
    public LayerMask WhastIsGround;
    public Transform checkPnj;
    public LayerMask layerPnj;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        rigidebody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
        Attack(); 
    }

    private void FixedUpdate() {
        //Move();
    }

    private void Move() 
    { if (view.IsMine){
        if (!canMoving)
         {  
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * MovementSpeed * Time.fixedDeltaTime, rigidebody.velocity.y);
            rigidebody.velocity = movement;
            if (Input.GetAxis("Horizontal") > 0 && !FacingRight)
                {
                Flip();
                }
            else if (Input.GetAxis("Horizontal") < 0 && FacingRight)
                {
                Flip();
                }
            if (Input.GetAxis("Horizontal") != 0)
            {
                animator.SetBool("isMooving", true);
            }else 
            {
                animator.SetBool("isMooving", false);
            }
        }
    } }

    void Jump()
    { if (view.IsMine){
        isGrounded = Physics2D.OverlapCircle(checkGround.position, 0.5f, WhastIsGround);
        if(isGrounded) 
        {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigidebody.velocity = new Vector2(rigidebody.velocity.x, JumpForce);
        }
        }
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("verticalSpeed", rigidebody.velocity.y);
       
    } }

    void crounch()
    {
        if(isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator.SetTrigger("isCrounched");
            }
        }
    }

    void Flip()
	{
		FacingRight = !FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
	    transform.localScale = theScale;
	}
    public void canMove()
    {
        canMoving = !canMoving;
        Debug.Log(canMoving);
    }
    void Attack()
    { if (view.IsMine){
        if(Time.time > TimeBetweenAttack)
        {
            if(Input.GetKeyDown(KeyCode.E) && !canMoving )
            {
                //rigidebody.velocity = Vector2.zero;
                animator.SetTrigger("attack1");
                TimeBetweenAttack = Time.time + AttackTime;
            }
            if(Input.GetKeyDown(KeyCode.R) && !canMoving )
            {
                //rigidebody.velocity = Vector2.zero;
                animator.SetTrigger("attack2");
                TimeBetweenAttack = Time.time + AttackTime;
            }
            if(Input.GetKeyDown(KeyCode.T) && !canMoving )
            {
                //rigidebody.velocity = Vector2.zero;
                animator.SetTrigger("attack3");
                TimeBetweenAttack = Time.time + AttackTime;
            }
            if(Input.GetKeyDown(KeyCode.Y) && !canMoving )
            {
                //rigidebody.velocity = Vector2.zero;
                animator.SetTrigger("speAttack");
                TimeBetweenAttack = Time.time + AttackTime;
            }
        }
        
    } }
    
    public void DommageAttack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(checkPnj.position, 0.2f, layerPnj);
        foreach (Collider2D col in enemy)
        {
            //Debug.Log(col.GetComponent<Character>().takeDomage(pnj.domage));
            col.GetComponent<Character>().takeDomage(pnj.domage);
        }
    }
}