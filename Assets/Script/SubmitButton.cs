using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SubmitButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        DragDropManager.Instance.OnSubmitButtonClick();
    }

    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnClick);
        }
    }
} 