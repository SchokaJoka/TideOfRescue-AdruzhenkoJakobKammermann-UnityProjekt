using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse2 : MonoBehaviour
{
    public float moveSpeed = 2f;        // Maximum forward speed
    public float rotationSpeed = 20f;  // Maximum rotation speed (degrees per second)
    public float smoothTime = 0.8f;     // Time it takes to ease movement

    private Vector3 currentVelocity;    // For smoothing position
    private float currentAngularVelocity; // For smoothing rotation
    private Vector3 targetPosition;

    void Update()
    {
        // Get the mouse position in world space
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0; // Ensure it's 2D

        // Rotate smoothly towards the target
        RotateTowardsTarget();

        // Move smoothly towards the target position
        MoveTowardsTarget();
    }

    void RotateTowardsTarget()
    {
        // Calculate the direction to the target
        Vector3 direction = targetPosition - transform.position;

        // Get the target angle (facing the mouse)
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        // Smoothly rotate towards the target angle with easing
        float angle = Mathf.SmoothDampAngle(
            transform.eulerAngles.z, 
            targetAngle, 
            ref currentAngularVelocity, 
            smoothTime);

        // Apply the rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void MoveTowardsTarget()
    {
        // Smoothly move towards the target position with easing
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            targetPosition, 
            ref currentVelocity, 
            smoothTime, 
            moveSpeed);
    }
}
