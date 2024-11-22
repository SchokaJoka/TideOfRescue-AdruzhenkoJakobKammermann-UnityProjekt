using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, ICollectible
{
    private GameObject player;
    public void Collect() 
    {
        // Logic for collecting the item (e.g., increase score or inventory)
        Destroy(gameObject); // Destroy the item after collection
        Debug.Log("Item collected!"); 
        if (player.gameObject.GetComponent<BoatController>() != null)
        { 
            player.gameObject.GetComponent<BoatController>().AddFuel();
        }
        else
        {
            Debug.LogError("BoatController Script not found");
        }    
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
