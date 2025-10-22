using UnityEngine;

public class House2 : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private Sprite normalSprite;            
    [SerializeField] private Sprite activeSprite;     


    void Start()
    {
        
    }

    
    void Update()
    {
        if (House1.isActiveH1)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }
}
