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
    public GameObject target;
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
    
    private void OnTriggerEnter2D(Collider2D trig) {
        if(trig.gameObject.tag == "Player") 
        {
            target = trig.gameObject;
        }
        
    }

    public void DommageAttack(int attackDommage)
    {
        if(target != null) {
            target.GetComponent<Character>().takeDomage((pnj.domage + attackDommage));
            target = null;
        }
    }
}