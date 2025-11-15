using UnityEngine;

public class redEnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    private bool playerInZone = false;
    private bool attack = false;
    private bool rotationBullet = false;
    private bool canAttack = true;
    private bool walking = false;

    private Transform player;
    private Vector2 direction;

    private Vector3 originalScale;
    private Animator animator;

    private int HP = 5;

    [SerializeField] private float speed;
    [SerializeField] private GameObject bulletPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        if(HP <= 0)
        {
            Death();
        }

        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
            Flip(); 
            RotationBullet(); 

        }
        Walk();
    }

    private void Walk()
    {
        if (playerInZone && player != null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            RotationBullet();

            walking = false;

            animator.SetBool("walking", walking);

            attack = true;
            Attack();
        }
        else if (!playerInZone && player != null) 
        {
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
            attack = false; 

            walking = true;

            animator.SetBool("walking", walking);
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void Attack()
    {
        if (bulletPrefab == null || !canAttack) return; 

        if (!rotationBullet)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 180, 0));
        }
        else
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        }
        
        canAttack = false; 
        Invoke(nameof(ResetAttack), 0.5f); 
    }

    private void RotationBullet()
    {
        if (direction.x >= 0)
        {
            rotationBullet = false;
        }
        else if (direction.x < 0)
        {
            rotationBullet = true;
        }
    }
    private void ResetAttack() 
    {
        canAttack = true;
    }

    private void Death()
    {
        animator.SetTrigger("death");
        Invoke("Destroy", 1.5f);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void Flip()
    {
        float flipX = direction.x >= 0 ? originalScale.x : -originalScale.x;
        transform.localScale = new Vector3(flipX, originalScale.y, originalScale.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            player = other.transform;
        }
        if (other.CompareTag("bullet"))
        {
            HP -= 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            attack = false;
        }
    }
}