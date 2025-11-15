using UnityEngine;

public class redBulletScript : MonoBehaviour
{
    private float dirY;
    [SerializeField] private float speed = 5f;

    void Start()
    {
        float dirY = transform.eulerAngles.y;
        Vector2 dir = Mathf.Approximately(dirY, 0) ? Vector2.left : Vector2.right;
        GetComponent<Rigidbody2D>().linearVelocity = dir * speed; 
        Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // используем Mathf.Approximately, чтобы избежать неточного сравнения
        if (Mathf.Approximately(dirY, 0))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (Mathf.Approximately(dirY, 180))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.PlayerDamage(); 
            }
            Destroy(gameObject);
        }
    }
}
