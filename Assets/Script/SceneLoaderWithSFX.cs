using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoaderWithSFX : MonoBehaviour
{
    public string sceneToLoad;
    public AudioClip clickSound;
    public float delayBeforeLoad = 0.5f; // Set to your sound's length

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlaySoundAndLoadScene()
    {
        StartCoroutine(PlaySoundThenLoad());
    }

    IEnumerator PlaySoundThenLoad()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);

        yield return new WaitForSeconds(delayBeforeLoad);

        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
    }
}