using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReturnToPreviousScene : MonoBehaviour
{
    public AudioClip clickSound;         // Assign the click sound in the Inspector
    public float delayBeforeLoad = 0.5f; // Set to your sound's length

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnReturnButtonClick()
    {
        StartCoroutine(PlaySoundThenReturn());
    }

    IEnumerator PlaySoundThenReturn()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);

        yield return new WaitForSeconds(delayBeforeLoad);

        string previousScene = PlayerPrefs.GetString("PreviousScene");
        SceneManager.LoadScene(previousScene);
    }
} 