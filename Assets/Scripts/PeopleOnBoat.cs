using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleOnBoat : MonoBehaviour
{
    public GameObject[] children; // Array to hold child objects

    private void Start()
    {
        // Deactivate all child objects at the start
        foreach (GameObject child in children)
        {
            child.SetActive(false);
        }
    }

    public void AddPersontoBoat()
    {
        // Activate the first inactive child object
        foreach (GameObject child in children)
        {
            if (!child.activeSelf)
            {
                child.SetActive(true);
                break; 
            }
            
        }
    }
    
    public void ResetPersononBoat()
    {
        // Deactivate all child objects at the start
        foreach (GameObject child in children)
        {
            child.SetActive(false);
        }
    }
    
}