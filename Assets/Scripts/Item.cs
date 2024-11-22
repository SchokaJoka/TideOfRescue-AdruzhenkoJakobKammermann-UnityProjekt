using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectible
{ 
    public void Collect() 
    {
        // Logic for collecting the item (e.g., increase score or inventory)
        Destroy(gameObject); // Destroy the item after collection
        Debug.Log("Item collected!");
        
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
