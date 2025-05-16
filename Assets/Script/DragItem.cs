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

    private DropArea lastDropArea = null;

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
            if (dropArea && dropArea.currentItem == null)
            {
                // Snap to any empty drop area
                transform.position = dropArea.transform.position;
                transform.SetParent(dropArea.transform);
                isPlaced = true;

                // Only correct if tag matches
                isPlacedCorrectly = dropArea.CompareTag(correctDropAreaTag);
                DragDropManager.Instance.CheckAllItems();

                // Mark this drop area as occupied
                dropArea.currentItem = this;

                // If previously in another drop area, clear it
                if (lastDropArea != null && lastDropArea != dropArea)
                    lastDropArea.currentItem = null;

                lastDropArea = dropArea;

                // Show correct/wrong image
                if (dropArea.correctImage != null)
                    dropArea.correctImage.SetActive(isPlacedCorrectly);
                if (dropArea.wrongImage != null)
                    dropArea.wrongImage.SetActive(!isPlacedCorrectly);

                // Play SFX
                if (isPlacedCorrectly)
                    dropArea.PlayCorrectSFX();
                else
                    dropArea.PlayWrongSFX();

                return;
            }
        }
        // If not dropped on any drop area, return to start
        if (lastDropArea != null)
        {
            // Hide images if returning to start
            if (lastDropArea.correctImage != null)
                lastDropArea.correctImage.SetActive(false);
            if (lastDropArea.wrongImage != null)
                lastDropArea.wrongImage.SetActive(false);

            lastDropArea.currentItem = null;
            lastDropArea = null;
        }
        transform.position = startPosition;
        transform.SetParent(originalParent);
        isPlaced = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.parent != originalParent)
        {
            // If currently in a drop area, free it and hide images
            if (lastDropArea != null)
            {
                if (lastDropArea.correctImage != null)
                    lastDropArea.correctImage.SetActive(false);
                if (lastDropArea.wrongImage != null)
                    lastDropArea.wrongImage.SetActive(false);

                lastDropArea.currentItem = null;
                lastDropArea = null;
            }
            transform.position = startPosition;
            transform.SetParent(originalParent);
            isPlacedCorrectly = false;
            isPlaced = false;
            DragDropManager.Instance.CheckAllItems();
        }
    }
} 