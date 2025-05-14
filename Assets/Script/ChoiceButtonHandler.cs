using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChoiceButtonHandler : MonoBehaviour
{
    public bool isCorrectAnswer = false; // Set this in the Inspector for each button
    public string nextSceneName;         // Set this to the next scene for the correct answer
    public AudioClip clickSound;         // Assign the click sound in the Inspector
    public float delayBeforeLoad = 0.5f; // Set to your sound's length

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnButtonClick()
    {
        StartCoroutine(PlaySoundThenLoad());
    }

    IEnumerator PlaySoundThenLoad()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);

        yield return new WaitForSeconds(delayBeforeLoad);

        if (isCorrectAnswer)
        {
            if (!string.IsNullOrEmpty(nextSceneName))
                SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Save the current scene name to PlayerPrefs so we can return to it
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Level WrongPick");
        }
    }
} 