using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HiddenItemsSubmitButton : MonoBehaviour
{
    public Button submitButton;
    public string nextSceneName = "NextSceneName"; // Set this in the Inspector

    private HiddenItem[] hiddenItems;

    void Start()
    {
        submitButton.gameObject.SetActive(false); // Hide by default
        submitButton.onClick.AddListener(OnSubmit);

        hiddenItems = FindObjectsByType<HiddenItem>(FindObjectsSortMode.None);
        foreach (var item in hiddenItems)
        {
            item.gameObject.AddComponent<HiddenItemWatcher>().Init(this);
        }
    }

    public void CheckAllFound()
    {
        foreach (var item in hiddenItems)
        {
            if (item.gameObject.activeSelf)
                return;
        }
        submitButton.gameObject.SetActive(true);
    }

    void OnSubmit()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

public class HiddenItemWatcher : MonoBehaviour
{
    private HiddenItemsSubmitButton manager;

    public void Init(HiddenItemsSubmitButton mgr)
    {
        manager = mgr;
    }

    void OnDisable()
    {
        if (manager != null)
            manager.CheckAllFound();
    }
}