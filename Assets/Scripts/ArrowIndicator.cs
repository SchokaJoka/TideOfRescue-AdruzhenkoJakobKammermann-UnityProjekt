using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowIndicator : MonoBehaviour
{
    public Transform target; // The object the arrow points to
    public RectTransform arrowTransform; // The arrow's UI RectTransform
    public Camera mainCamera; // Reference to the main camera
    public GameObject arrowImage;

    void Start()
    {
        if (!mainCamera) mainCamera = Camera.main;
    }

    void Update()
    {
        ArrowAnimation();
    }
    
    private void ArrowAnimation()
    {
        if (!target) return;

        // Convert target world position to screen position
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(target.position);

        // Check if the target is off-screen
        bool isOffScreen = screenPosition.z < 0 ||
                           screenPosition.x < 0 || screenPosition.x > Screen.width ||
                           screenPosition.y < 0 || screenPosition.y > Screen.height;

        if (isOffScreen)
        {
            arrowImage.SetActive(true);
            // Clamp the arrow to the screen edges
            Vector3 clampedPosition = screenPosition;
            clampedPosition.x = Mathf.Clamp(screenPosition.x, 0, Screen.width);
            clampedPosition.y = Mathf.Clamp(screenPosition.y, 0, Screen.height);

            // Update arrow position and rotation
            arrowTransform.position = clampedPosition;

            // Rotate arrow to point towards target
            Vector3 direction = target.position - mainCamera.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrowTransform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        else
        {
            // Hide arrow if the target is on-screen
            arrowImage.SetActive(false);
        }
    }

    public void ArrowDelete()
    {
        arrowImage.SetActive(false);
    }
}