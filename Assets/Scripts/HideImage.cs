using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
public class HideImage : MonoBehaviour
{
    public Image introImage;
    public float displayTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (introImage != null)
            Invoke("Hide", displayTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Hide()
    {
        introImage.gameObject.SetActive(false);
    }
}
