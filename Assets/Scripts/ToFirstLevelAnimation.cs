using UnityEngine;
using UnityEngine.SceneManagement;


public class ToFirstLevelAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FirstLevel()
    {
        // SceneManager.LoadScene(2);
        House1.isActiveH1 = true;
    }


    public void FadeToLevel()
    {
        anim.SetTrigger("fade");
    }
}
