using UnityEngine;

public class EnemyPlants : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.GetComponent<Player>().PlayerDamage();
        }
    }
}
