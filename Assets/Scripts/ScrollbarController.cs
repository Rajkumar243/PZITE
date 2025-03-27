using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarController : MonoBehaviour
{
    RectTransform rectTransform;
    public int TopPosY;
    public int BottomPosY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {

        // Get current position
        Vector3 position = rectTransform.anchoredPosition;

        // Clamp the Y position
        position.y = Mathf.Clamp(position.y, TopPosY, BottomPosY);

        // Apply the clamped position
        rectTransform.anchoredPosition = position;
    }
}
