using UnityEngine;

public class DropArea : MonoBehaviour
{
    // This can be empty, just used for tagging and reference
    [HideInInspector]
    public DragItem currentItem = null; // Track the item currently in this drop area

    public GameObject correctImage; // Assign in Inspector
    public GameObject wrongImage;   // Assign in Inspector

    public AudioClip correctSFX; // Assign in Inspector
    public AudioClip wrongSFX;   // Assign in Inspector

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCorrectSFX()
    {
        if (audioSource != null && correctSFX != null)
            audioSource.PlayOneShot(correctSFX);
    }

    public void PlayWrongSFX()
    {
        if (audioSource != null && wrongSFX != null)
            audioSource.PlayOneShot(wrongSFX);
    }
} 