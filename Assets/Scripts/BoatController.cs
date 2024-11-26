using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BoatController : MonoBehaviour
{
    // Movement
    public float moveSpeed = 2f;            // Maximum forward speed
    public float rotationSpeed = 20f;       // Maximum rotation speed (degrees per second)
    public float smoothTime = 0.8f;         // Time it takes to ease movement
    private float _currentAngularVelocity;   // For smoothing rotation
    private Vector3 _targetPosition;
    public Vector3 currentVelocity;        // For smoothing position
    
    // Camera
    private Camera _mainCamera;              // Cache the Camera reference

    // Fuel
    public float currentFuel;
    public float maxFuel = 1000f;
    public TextMeshProUGUI fuelText;

    // Events
    public UnityEvent OnGameOver;
    
    void Start()
    {
        // Cache the Camera component
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.LogError("No camera tagged as MainCamera in the scene.");
        }

        currentFuel = maxFuel;
    }
    
    void Update()
    {
        GetMousePosition();
        RotateTowardsTarget();
        MoveTowardsTarget();

        currentFuel -= Time.deltaTime * 2 * currentVelocity.magnitude;
        UIFuelUpdater();
        
        if (currentFuel <= 0)
        {
            OnGameOver.Invoke();
        }
    }

    // Get the mouse position in world space
    void GetMousePosition()
    {
        // Use the cached camera to get mouse position
        _targetPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition.z = 0; // Ensure it's 2D
    }

    // Rotate smoothly towards the target
    void RotateTowardsTarget()
    {
        // Calculate the direction to the target
        Vector3 direction = _targetPosition - transform.position;

        // Get the target angle (facing the mouse)
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        // Smoothly rotate towards the target angle with easing
        float angle = Mathf.SmoothDampAngle(
            transform.eulerAngles.z, 
            targetAngle, 
            ref _currentAngularVelocity, 
            smoothTime);

        // Apply the rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Move smoothly towards the target position
    void MoveTowardsTarget()
    {
        // Smoothly move towards the target position with easing
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            _targetPosition, 
            ref currentVelocity, 
            smoothTime, 
            moveSpeed);
    }

    // What to do when the boat collides with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the Collision Object has ICollectible interface implemented
        ICollectible collectible = collision.gameObject.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.Collect();
        }
        else
        {
            Debug.Log("Object not collectible.");
        }      
    }
    
    // Update the 
    private void UIFuelUpdater()
    {
        fuelText.text = "Fuel: " + currentFuel.ToString("F0");
        
    }

    public void AddFuel()
    {
        currentFuel += 100f;
    }
}
