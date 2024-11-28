using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PersonSpawner : MonoBehaviour
{
    public GameObject[] prefabs;                            // Array of prefabs to spawn    
    public GameObject arrowPrefab;                          // Prefab for the UI arrow
    private GameObject spawnedObject;                       // Reference to the spawned object
    public GameManager gM;                                  // Reference to the GameManager
    public Transform canvasTransform;                       // Reference to the Canvas
    private int attempts = 0;                                // Number of spawn attempts
    
    public int numberOfObjects = 8;                         // How many objects to spawn
    private int spawnedObjectCounter = 0;                         // Temporary counter for Debugging
    private float radius = 1f;                           // Minimum distance between objects
    
    public float dieTime = 80f;                             // Time before the person dies

    private Vector2 minBounds = new Vector2(-30, -30);    // Bottom-left of the area
    private Vector2 maxBounds = new Vector2(30, 30);      // Top-right of the area
    private Vector2 spawnPosition;                          // Random spawn position


    void Awake()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnObject();
        }
        Debug.Log(spawnedObjectCounter + " People spawned!");
        
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
            if (attempts >= 40)
            {
                return; // Exit the method if unable to find a valid position
            }
        } while (Physics2D.OverlapCircle(spawnPosition, radius) != null);

        // Randomly select a prefab from the array
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        
        // Check if prefab has a Person script
        Person person = prefabToSpawn.gameObject.GetComponent<Person>();
            if (person)
            {
                // Instantiate the object
                spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                spawnedObject.gameObject.GetComponent<Person>().survivalTime = dieTime;
                gM.setgameTimer = dieTime;
                spawnedObjectCounter++;
            }
            else
            {
                Debug.LogError("Person script not found on the spawned object!");
            }
            
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

