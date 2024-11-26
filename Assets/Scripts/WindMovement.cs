using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMovement : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public float smoothTime = 0.3f;
    private Vector3 _targetPosition;
    public Vector3 currentVelocity;
    
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        // Cache the Camera component
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.LogError("No camera tagged as MainCamera in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();
        MoveTowardsTarget();
    }

    void GetMousePosition()
    {
        // Use the cached camera to get mouse position
        _targetPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition.z = 0; // Ensure it's 2D
    }
    
    
    void MoveTowardsTarget()
    {
        // Smoothly move towards the target position with easing
        transform.localPosition = Vector3.SmoothDamp(
            transform.position, 
            _targetPosition, 
            ref currentVelocity, 
            smoothTime, 
            moveSpeed);
    }
    
    
}
