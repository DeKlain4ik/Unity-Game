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
    [SerializeField] private float jetpackForce = 6f;
    [SerializeField] private float maxFuel = 2f;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject[] Hearts = new GameObject[5];
    [SerializeField] private TMP_Text gameOver;
    [SerializeField] private GameObject bullets;
    [SerializeField] private GameObject DeathAudio;


    [SerializeField] private Animator chestPanelAnimator; // firstTask
    [SerializeField] private Animator secondTaskAnimator; 
    [SerializeField] private Animator thirdTaskAnimator; 
    


    [SerializeField] private Animator buff;
    private bool makingTasks = false;
    private float fuel;

    public TMP_Text countKeys;


    [SerializeField] private AudioSource walkAudio;
    [SerializeField] private AudioSource shotAudio;
    [SerializeField] private AudioSource landingAudio;
    [SerializeField] private AudioSource JumpAudio;
    [SerializeField] private AudioSource DamageAudio;

    [SerializeField] private AudioSource MishaAudio;
    [SerializeField] private AudioSource V1tecAudio;


    private int countHearts = 4;

    private int count_key = 0;

    private bool isJump;
    private bool isIdle;
    private bool isRunning;
    private bool isClimbing = false;
    private bool onLadder = false;
    private bool isLeft = false;
    private bool isGround = false;
    private bool isJetpack = false;


    [SerializeField]public bool firstTask = false;
    [SerializeField]public bool secondTask = false;
    [SerializeField]public bool thirdTask = false;


    void Start()
    {
        gameOver.text = " ";
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fuel = maxFuel;

        UpdateCountKeysText();
    }


    void Update()
    {
        if (makingTasks)
        {
            rb.linearVelocity = Vector2.zero;
            moveVector.x = 0;
            animator.SetFloat("moveVector", Mathf.Abs(moveVector.x));
            walkAudio.Stop();
            isRunning = false;
            isIdle = true;
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isIdle", isIdle);
            return;
        }

        Walk();
        Jump();
        Flip();
        PlayerDeath();
        AttackHero();
        RotationBullet();
        ClimbLadder();
        Jetpack();

        UpdateCountKeysText();

    }

    private void Walk()
    {
        if (isClimbing || makingTasks) return;



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
            if (!walkAudio.isPlaying && isGround)
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
        else if (collision.CompareTag("key"))
        {
            count_key++;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("firstTask") && !firstTask && count_key > 0)
        {
            makingTasks = true;
            chestPanelAnimator.SetTrigger("Open");
            count_key--;
        }
        else if (collision.CompareTag("secondTask") && !secondTask && count_key > 0)
        {
            makingTasks = true;
            secondTaskAnimator.SetTrigger("Open");
            count_key--;
        }
        else if (collision.CompareTag("thirdTask") && !thirdTask && count_key > 0)
        {
            makingTasks = true;
            thirdTaskAnimator.SetTrigger("Open");
            count_key--;
        }

        else if (collision.CompareTag("Finish"))
        {
            MishaAudio.Play();
        }
        else if (collision.CompareTag("Respawn"))
        {
            V1tecAudio.Play();
        }
        else if (collision.CompareTag("EditorOnly"))
        {
            Destroy(collision.gameObject);
            isJetpack = true;
            buff.SetTrigger("in");
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
        else if (collision.CompareTag("Finish"))
        {
            MishaAudio.Stop();
        }
        else if (collision.CompareTag("Respawn"))
        {
            V1tecAudio.Stop();
        }

    }


    public void firstTaskFalse()
    {
        makingTasks = false;
        chestPanelAnimator.SetTrigger("Close");
    }
    
    public void firstTaskTrue()
    {
        firstTask = true;
        makingTasks = false;
        chestPanelAnimator.SetTrigger("Close");
    }

    public void secondTaskFalse()
    {
        makingTasks = false;
        secondTaskAnimator.SetTrigger("Close");
    }

    public void secondTaskTrue()
    {
        secondTask = true;
        makingTasks = false;
        secondTaskAnimator.SetTrigger("Close");
    }
    public void thirdTaskFalse()
    {
        makingTasks = false;
        thirdTaskAnimator.SetTrigger("Close");
    }

    public void thirdTaskTrue()
    {
        thirdTask = true;
        makingTasks = false;
        thirdTaskAnimator.SetTrigger("Close");
    }


    private void Jetpack()
    {
        if (!isJetpack) return;

        if (Input.GetKey(KeyCode.Space) && !isGround && fuel > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jetpackForce);
            fuel -= Time.deltaTime;
        }
        else if (isGround)
        {
            fuel += Time.deltaTime * 1f;
            fuel = Mathf.Clamp(fuel, 0, maxFuel);
        }
    }


    
    void UpdateCountKeysText()
    {
        countKeys.text = count_key.ToString();
    }

}
