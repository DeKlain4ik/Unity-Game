using UnityEngine;

public class DeathEnemyAudioDestroy : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && audioSource.clip != null)
        {
            Destroy(gameObject, audioSource.clip.length);
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }
}
