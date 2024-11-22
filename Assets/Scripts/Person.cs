using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour, ICollectible
{
    public float survivalTime = 3f; // Time in seconds before the person drowns
    private bool _isRescued = false;
    private float timer;
    private float sliderFillAmount;
    
    public GameObject sliderBar; // Reference to the slider bar (parent object)
    public GameObject fillBar; // Reference to the fill bar (child object of the slider)

    // Start is called before the first frame update
    void Start()
    {
        sliderBar.transform.localScale = new Vector2(2f, 0.2f);
        timer = survivalTime;
        Debug.Log("Timer started with " + timer + " seconds!");
    }

    // Update is called once per frame
    void Update()
    {
        // Stop timer if the target is rescued
        if (!_isRescued)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            { 
                Drown();
                Debug.Log("Timer stopped!");
            }
            
        }
        
        // Update the fill bar's scale to match the remaining time
        if (sliderBar != null && fillBar != null)
        {
            UpdateSliderBar();
        }
        else
        {
            Debug.LogError("Slider bar or fill bar not assigned in the Inspector!");
        }
    }

    // Destroy the target    
    private void Drown()
    {
        Destroy(gameObject);
        Debug.Log("Person drowned!");
    }
    
    // Collect the target
    public void Collect()
    {
        if (!_isRescued)
        {
            _isRescued = true;
            Destroy(gameObject);
            Debug.Log("Person rescued!");
        }
    }
    
    private void UpdateSliderBar()
    {
        // Get the fraction of remaining time
        sliderFillAmount = timer / survivalTime;
        
        // Adjust fill based on time remaining
        fillBar.transform.localScale = new Vector2(sliderFillAmount * 2f, 0.2f);
    }
}
