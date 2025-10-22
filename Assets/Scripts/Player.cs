using UnityEngine;
using TMPro;
using System.Collections;
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 moveVector;
    private Animator animator;
    public static bool attack = false;
    private bool rotationBullet = false;




    [SerializeField] private float speed;
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject[] Hearts = new GameObject[5];
    [SerializeField] private TMP_Text gameOver;
    [SerializeField] private GameObject bullets;
    [SerializeField] private GameObject DeathAudio;



    [SerializeField] private AudioSource walkAudio;
    [SerializeField] private AudioSource shotAudio;
    [SerializeField] private AudioSource landingAudio; 
    [SerializeField] private AudioSource JumpAudio; 
    [SerializeField] private AudioSource DamageAudio;

    private int countHearts = 4;
    private int count_walking = 0;

    private bool isJump;
    private bool isIdle;
    private bool isRunning;
    private bool isClimbing = false;
    private bool onLadder = false;
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
        AttackHero();
        RotationBullet();
        ClimbLadder();

    }

    private void Walk()
    {
        if (isClimbing) return;


        moveVector.x = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveVector.x * speed, rb.linearVelocity.y);
        animator.SetFloat("moveVector", Mathf.Abs(moveVector.x));
        if (moveVector.x == 0)
        {
            walkAudio.Stop();
            isRunning = false;
            isIdle = true;
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isIdle", isIdle);
        }
        else
        {
            if(!walkAudio.isPlaying && isGround)
                walkAudio.Play();
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

            walkAudio.Stop();
            JumpAudio.Play();

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
            landingAudio.Play();
            isJump = false;
            animator.SetBool("isJump", isJump);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            walkAudio.Stop();
                        
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
            Instantiate(DeathAudio, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameOver.text = "game over";

        }
    }
    

    public void PlayerDamage()
    {
        DamageAudio.Play();
        Destroy(Hearts[countHearts]);
        countHearts--;
    }
    private void AttackHero()
    {
        if (Input.GetKeyDown(KeyCode.E) && !attack)
        {
            shotAudio.Play();

            if (rotationBullet)
            {
                Instantiate(bullets, transform.position, Quaternion.Euler(0, 180, 0));
                attack = true;
            }
            else
            {
                Instantiate(bullets, transform.position, Quaternion.Euler(0, 0, 0));
                attack = true;
            }
        }

    }
    private void RotationBullet()
    {
        if (moveVector.x >= 0)
        {
            rotationBullet = true;
        }
        else if (moveVector.x < 0)
        {
            rotationBullet = false;
        }
    }

    private void ClimbLadder()
    {
        if (!onLadder) return;

        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(vertical) > 0.1f)
        {
            isClimbing = true;
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
        }
        else if (isClimbing)
        {
            isClimbing = false;
            rb.linearVelocity = Vector2.zero;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            onLadder = false;
            isClimbing = false;
            rb.gravityScale = 1; 
        }
    }


}
