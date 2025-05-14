using UnityEngine;
using UnityEngine.SceneManagement;

public class DragDropManager : MonoBehaviour
{
    public static DragDropManager Instance;
    public DragItem[] dragItems;
    public string nextSceneName;

    void Awake()
    {
        Instance = this;
    }

    public void CheckAllItems()
    {
        foreach (var item in dragItems)
        {
            if (!item.isPlacedCorrectly)
                return;
        }
        // All items are correct, proceed to next scene
        SceneManager.LoadScene(nextSceneName);
    }
} 