using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    
    public float maxSpeed = 2f;
    public float rotationSpeed = 100f;
   
    public float smoothTime = 1f;    // Time it takes to ease movement

    private Vector3 currentVelocity;   // For smoothing movement
    private float currentAngularVelocity; // For smoothing rotation
    
    private Vector3 targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in world space
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;

        // Rotate smoothly towards the target
        RotateTowardsTarget();

        // Move the player forward in the direction it's facing
        MoveForward();
        
        /*
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);        //Get mouse position in world coordinates
        
        float distance = Vector2.Distance(transform.position, mousePosition);               // Calculate distance between current player position and mouse position
        float speed = Mathf.Clamp(distance, 0, maxSpeed);                                  // Adjust speed based on distance

        transform.position = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
        
        
        Quaternion direction = Quaternion.LookRotation(Vector3.forward, mousePosition - (Vector2)transform.position); // Calculate rotation based on mouse position
        transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotationSensitivity * Time.deltaTime); // Rotate the player towards the mouse position
        */
    }
    
    void RotateTowardsTarget()
    {
        // Calculate the direction to the target
        Vector3 direction = targetPosition - transform.position;

        // Get the target angle (facing the mouse)
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        // Get the current angle
        float currentAngle = transform.eulerAngles.z;

        // Smoothly rotate towards the target angle, capped by rotationSpeed
        float angle = Mathf.MoveTowardsAngle(
            currentAngle,
            targetAngle,
            rotationSpeed * Time.deltaTime
        );

        // Apply the rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void MoveForward()
    {
        float distance = Vector2.Distance(transform.position, targetPosition);               // Calculate distance between current player position and mouse position
        float speed = Mathf.Clamp(distance, 0, maxSpeed);                                  // Adjust speed based on distance
        
        
        // Move the player forward in the direction it's facing
        transform.position += transform.up * (speed * Time.deltaTime);
    }
    
}
