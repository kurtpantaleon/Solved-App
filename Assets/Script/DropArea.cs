using UnityEngine;

public class DropArea : MonoBehaviour
{
    // This can be empty, just used for tagging and reference
    [HideInInspector]
    public DragItem currentItem = null; // Track the item currently in this drop area

    public GameObject correctImage; // Assign in Inspector
    public GameObject wrongImage;   // Assign in Inspector
} 