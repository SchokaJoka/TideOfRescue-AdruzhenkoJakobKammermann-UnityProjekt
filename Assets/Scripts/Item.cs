using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, ICollectible
{
    private GameObject player;
    public UnityEvent OnItemCollect;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }
    
    public void Collect() 
    {
        if (player.gameObject.GetComponent<BoatController>() != null)
        { 
            player.gameObject.GetComponent<BoatController>().AddFuel();
        }
        else
        {
            Debug.LogError("BoatController Script not found");
        }

        Destroy(gameObject);
        OnItemCollect.Invoke();
    }
}
