using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float dir;
    [SerializeField] private float speed;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        float dir = transform.eulerAngles.y;
        Move();

    }
    private void Move()
    {
        if (dir == 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            Invoke("Destroy", 0.9f);
        }
        else if (dir == 180)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            Invoke("Destroy", 0.9f);
        }
    }
    private void Destroy()
    {
        Destroy(gameObject);
        Player.attack = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Destroy();
        }
    }


}





