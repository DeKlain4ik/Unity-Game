using UnityEngine;

public class EnemyPlants : MonoBehaviour
{

    private int HP = 3;

    private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource DeathAudio; 


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
        {
            
            animator.SetTrigger("Death");
            Invoke("Death", 1.0f);

        }

    }
    private void Death()
    {
        Instantiate(DeathAudio, transform.position, Quaternion.identity);

        Destroy(gameObject);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
            player.GetComponent<Player>().PlayerDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            HP -= 1;
            animator.SetTrigger("Damage");
        }
    }
}
