using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HiddenItem : MonoBehaviour, IPointerClickHandler
{
    public Image panelIcon; // Assign the panel icon in the Inspector
    public Material grayscaleMaterial; // Assign your grayscale material in the Inspector

    private bool found = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!found)
        {
            found = true;
            if (panelIcon != null && grayscaleMaterial != null)
                panelIcon.material = grayscaleMaterial;
            gameObject.SetActive(false);
        }
    }
} 