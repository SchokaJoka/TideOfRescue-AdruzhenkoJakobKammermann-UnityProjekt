using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;                          // Prefab to spawn   
    public GameObject arrowPrefab;                      // Prefab for the UI arrow
    public int numberOfObjects = 15;                    // How many objects to spawn
    public Vector2 minBounds = new Vector2(-40, -40);   // Bottom-left of the area
    public Vector2 maxBounds = new Vector2(40, 40);     // Top-right of the area
    public float radius = 15;                           // Minimum distance between objects
    public Transform canvasTransform;                   // Reference to the Canvas
    private GameObject spawnedObject;
    
    void Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    { 
        // Generate a random position within bounds
        float x = Random.Range(minBounds.x, maxBounds.x);
        float y = Random.Range(minBounds.y, maxBounds.y);
        Vector2 spawnPosition = new Vector2(x, y);

        // Check if spawn point has no collisions
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, radius);
        if (colliders.Length == 0)
        {
            // Instantiate the object
            spawnedObject = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Item spawned!");
        }
        else
        {
            Debug.LogError("Cannot spawn item. Too close to another object!");
            return;
        }

        if (arrowPrefab)
        {
            // Instanciate UI Arrow
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

