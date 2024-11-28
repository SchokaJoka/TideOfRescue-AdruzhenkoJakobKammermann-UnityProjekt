using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;                       // Prefab to spawn   
    public GameObject arrowPrefab;                      // Prefab for the UI arrow
    private GameObject spawnedObject;
    public Transform canvasTransform;                   // Reference to the Canvas
    private int attempts = 0;                            // Number of spawn attempts
    
    public int numberOfObjects = 15;                    // How many objects to spawn
    private int spawnedObjectCounter = 0;               // Temporary counter for Debugging
    private float radius = 1f;                          // Minimum distance between objects

    private Vector2 minBounds = new Vector2(-30, -30);   // Bottom-left of the area
    private Vector2 maxBounds = new Vector2(30, 30);     // Top-right of the area
    private Vector2 spawnPosition;
    
    void Awake()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnObject();
        }
        Debug.Log(spawnedObjectCounter + " Items spawned!");
    }

    public void SpawnObject()
    {
        // Attempts to find a random position with no collisions
        do
        {
            // Generate a random position within bounds
            float x = Random.Range(minBounds.x, maxBounds.x);
            float y = Random.Range(minBounds.y, maxBounds.y);
            spawnPosition = new Vector2(x, y);
            
            attempts++;
            if (attempts >= 50)
            {
                return; // Exit the method if unable to find a valid position
            }
            
        } while (Physics2D.OverlapCircle(spawnPosition, radius) != null);
        
        // Instantiate the object
        spawnedObject = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        spawnedObjectCounter++;
            
        if (arrowPrefab)
        {
            GameObject arrow = Instantiate(arrowPrefab, canvasTransform);
            ArrowIndicator arrowIndicator = arrow.GetComponent<ArrowIndicator>();
            arrowIndicator.target = spawnedObject.transform; // Assign target
        }
        else
        {
            Debug.LogError("Arrow prefab not assigned in the Inspector!");
        }
    }
}

