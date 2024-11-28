using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BoatController : MonoBehaviour
{
    
    public AudioSource collectSound;
    
    public float moveSpeed = 3f;            // Maximum forward speed
    public float smoothTime = 0.9f;         // Time it takes to ease movement
    
    public float currentFuel;
    public float maxFuel = 1000f;
    
    public UnityEvent OnGameOver;
    public UnityEvent OnIslandEnter;
    
    // Private variables
    public float currentVelocity;           // For smoothing position
    public float _currentAngularVelocity;   // For smoothing rotation
    public Vector3 _targetPosition;
    public Camera _mainCamera;              // Cache the Camera reference
    
    void Start()
    {
        SetCamera();
        currentFuel = maxFuel;
    }
    
    void Update()
    {
        currentFuel -= Time.deltaTime * currentVelocity;
        GetMousePosition();
        RotateTowardsTarget();
        MoveTowardsTarget();
        
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
        // Calculate distance between current player position and mouse position
        float distance = Vector2.Distance(transform.position, _targetPosition); 
        
        // Adjust speed based on distance
        currentVelocity = Mathf.Clamp(distance, 0, moveSpeed);                                  
        
        // Move the player forward in the direction it's facing
        transform.position += transform.up * (currentVelocity * Time.deltaTime);
    }

    // What to do when the boat collides with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the Collision Object has ICollectible interface implemented
        ICollectible collectible = collision.gameObject.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.Collect();
            collectSound.Play();
        }
        else
        {
            OnIslandEnter.Invoke();
        }      
    }
    
    public void AddFuel()
    {
        currentFuel = maxFuel;
    }
    
    private void SetCamera()
    {
        // Cache the Camera component
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.LogError("No camera tagged as MainCamera in the scene.");
        }
    }
}
