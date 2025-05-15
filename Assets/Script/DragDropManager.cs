using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DragDropManager : MonoBehaviour
{
    public static DragDropManager Instance;
    public DragItem[] dragItems;
    public string nextSceneName;
    public GameObject submitButton; // Reference to the submit button GameObject
    public TextMeshProUGUI feedbackText; // Reference to the feedback text UI element

    void Awake()
    {
        Instance = this;
        if (submitButton != null)
        {
            submitButton.SetActive(false); // Hide submit button initially
        }
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }

    public void CheckAllItems()
    {
        bool allPlaced = true;
        foreach (var item in dragItems)
        {
            if (!item.isPlaced)
            {
                allPlaced = false;
                break;
            }
        }

        // Show submit button only when all items are placed
        if (submitButton != null)
        {
            submitButton.SetActive(allPlaced);
        }
    }

    public void OnSubmitButtonClick()
    {
        bool allCorrect = true;
        foreach (var item in dragItems)
        {
            if (!item.isPlacedCorrectly)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            // All items are correct, proceed to next scene
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Show try again message
            if (feedbackText != null)
            {
                feedbackText.text = "Try Again!";
                feedbackText.gameObject.SetActive(true);
                // Hide the message after 2 seconds
                Invoke("HideFeedback", 2f);
            }
        }
    }

    private void HideFeedback()
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
    }
} 