using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public string correctDropAreaTag; // Set this in Inspector to match the correct DropArea's tag
    private Vector3 startPosition;
    private Transform originalParent;
    private Canvas parentCanvas;

    public bool isPlacedCorrectly = false;
    public bool isPlaced = false; // New field to track if item has been placed at all

    void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        startPosition = transform.position;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isPlacedCorrectly = false;
        isPlaced = false;

        // Bring to front
        transform.SetParent(parentCanvas.transform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            DropArea dropArea = result.gameObject.GetComponent<DropArea>();
            if (dropArea)
            {
                // Snap to any drop area
                transform.position = dropArea.transform.position;
                transform.SetParent(dropArea.transform);
                isPlaced = true;

                // Only correct if tag matches
                isPlacedCorrectly = dropArea.CompareTag(correctDropAreaTag);
                DragDropManager.Instance.CheckAllItems();
                return;
            }
        }
        // If not dropped on any drop area, return to start
        transform.position = startPosition;
        transform.SetParent(originalParent);
        isPlaced = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Only allow reset if not currently being dragged and not already at original parent
        if (transform.parent != originalParent)
        {
            transform.position = startPosition;
            transform.SetParent(originalParent);
            isPlacedCorrectly = false;
            isPlaced = false;
            DragDropManager.Instance.CheckAllItems();
        }
    }
} 