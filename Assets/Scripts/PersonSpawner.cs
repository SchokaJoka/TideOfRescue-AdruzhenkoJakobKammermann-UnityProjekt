using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PersonSpawner : MonoBehaviour
{
    public GameObject[] prefabs;                        // Array of prefabs to spawn    
    public GameObject arrowPrefab;                      // Prefab for the UI arrow
    public int numberOfObjects = 15;                    // How many objects to spawn
    public Vector2 minBounds = new Vector2(-40, -40);   // Bottom-left of the area
    public Vector2 maxBounds = new Vector2(40, 40);     // Top-right of the area
    public float radius = 15;                            // Minimum distance between objects
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

        // Randomly select a prefab from the array
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        
        // Instantiate the object
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, radius);
        if (colliders.Length == 0)
        {
            Person person = prefabToSpawn.gameObject.GetComponent<Person>();
            if (person != null)
            {
                spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                spawnedObject.gameObject.GetComponent<Person>().survivalTime = Random.Range(50, 80);
                Debug.Log("Person spawn successful!");
            }
            else
            {
                Debug.LogError("Person script not found on the spawned object!");
            }
        }
        else
        {
            Debug.LogError("Cannot spawn person. Too close to another object!");
            return;
        }
        

        if (arrowPrefab)
        {
            GameObject arrow = Instantiate(arrowPrefab, canvasTransform);
            ArrowIndicator arrowIndicator = arrow.GetComponent<ArrowIndicator>();
            arrowIndicator.target = spawnedObject.transform; // Assign target
            Debug.Log("PersonArrow spawned!");
        }
        else
        {
            Debug.LogError("Arrow prefab not assigned in the Inspector!");
        }
    }
}

