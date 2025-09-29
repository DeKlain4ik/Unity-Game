using UnityEngine;
using TMPro;
using System.Collections;
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 moveVector;
    private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float leftForce;
    [SerializeField] private GameObject[] Hearts = new GameObject[5];
    [SerializeField] private TMP_Text gameOver;

    private int countHearts = 4;

    private bool isJump;
    private bool isIdle;
    private bool isRunning;

    private bool isLeft = false;
    private bool isGround = false;
    void Start()
    {
        gameOver.text = " ";
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {

        Walk();
        Jump();
        Flip();
        PlayerDeath();



    }



    private void Walk()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveVector.x * speed, rb.linearVelocity.y);
        animator.SetFloat("moveVector", Mathf.Abs(moveVector.x));
        if (moveVector.x == 0)
        {
            isRunning = false;
            isIdle = true;
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isIdle", isIdle);
        }
        else
        {
            isRunning = true;
            isIdle = false;
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isIdle", isIdle);
        }
    }



    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isJump = true;
            isRunning = false;
            isIdle = false;
            rb.AddForce(Vector2.up * jumpForce);
            animator.SetBool("isJump", isJump);
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isIdle", isIdle);
        }
    }

    private void Flip()
    {
        if ((!isLeft && moveVector.x < 0) || (isLeft && moveVector.x > 0))
        {
            transform.localScale *= new Vector2(-1, 1);
            isLeft = !isLeft;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isGround = true;
            animator.SetBool("isGround", isGround);

            isJump = false; 
            animator.SetBool("isJump", isJump);
            }
            

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isGround = false;
            animator.SetBool("isGround", isGround);
            
            isJump = true; 
            animator.SetBool("isJump", isJump);
            }
                    

    }


    public void PlayerDeath()
    {
        if (countHearts < 0)
        {
            Destroy(gameObject);
            gameOver.text = "game over";
        }
    }
    
    public void PlayerDamage()
    {
        Destroy(Hearts[countHearts]);
        countHearts--;
    }
}
