using UnityEngine;

public class House1 : MonoBehaviour
{
    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private Sprite normalSprite;            
    [SerializeField] private Sprite activeSprite;            

    public static bool isActiveH1 = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActiveH1)
        {
            buttonAnimator.SetTrigger("In");
        }

    }


    void Update()
    {
        if (isActiveH1)
        {
            spriteRenderer.sprite = activeSprite;
            buttonAnimator.SetTrigger("Out");
        }
        else if (!isActiveH1)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buttonAnimator.SetTrigger("Out");
        }
    }

    public void  SetActiveSprite()
    {
        spriteRenderer.sprite = activeSprite;
        isActiveH1 = true;
        
    }
}
