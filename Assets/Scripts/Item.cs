using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, ICollectible
{
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }
    
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
}
