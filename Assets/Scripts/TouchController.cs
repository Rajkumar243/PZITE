using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TouchController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Canvas canvas;
    public RectTransform rectTransform;

    public RectTransform validDropArea;  // The valid drop area for the food icon
    private Vector3 originalPosition;    // To store the original position of the food icon

    public AnimationController _animationController;

    //public GameObject VfxEffectCanvas;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        // Store the original position when the drag begins
        originalPosition = rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/ canvas.scaleFactor;
    }

   
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin");
    }

   
    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if the food icon is within the valid drop area
        if (IsWithinDropArea())
        {
            // If it's within the valid drop area, keep it at the new position
            Debug.Log("Item placed correctly.");

            rectTransform.gameObject.SetActive(false);
            rectTransform.position = originalPosition;
            Debug.Log(rectTransform.gameObject);
            if(rectTransform.gameObject.name == "OnDesk-Spoon")
            {
                _animationController.PlayEatingAnimation();
            }
        }
        else
        {
            // If it's outside the valid drop area, return it to the original position
            rectTransform.position = originalPosition;
            Debug.Log("Item placed incorrectly. Returning to original position.");
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
    }

    public void OnDrop(PointerEventData eventData)
    {
       
    }

    // Check if the food icon is within the valid drop area
    private bool IsWithinDropArea()
    {
        // Check if the food icon is within the bounds of the valid drop area
        return validDropArea.rect.Contains(validDropArea.InverseTransformPoint(rectTransform.position));
    }

}

        
    

