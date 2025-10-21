using UnityEngine;

public class EnemyPlants : MonoBehaviour
{

    private int HP = 3;
    [SerializeField] private GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.GetComponent<Player>().PlayerDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            HP -= 1;
        }
    }
}
