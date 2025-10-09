using UnityEngine;

public class House1 : MonoBehaviour
{
    [SerializeField] private Animator buttonAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonAnimator.SetTrigger("In");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonAnimator.SetTrigger("Out");
        }
    }
}
