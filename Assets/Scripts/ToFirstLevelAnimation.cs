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
        SceneManager.LoadScene(3);
    }


    public void FadeToLevel()
    {
        anim.SetTrigger("fade");
    }
}
