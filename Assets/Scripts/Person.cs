using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

public class Person : MonoBehaviour, ICollectible
{
    public float survivalTime = 3f; // Time in seconds before the person drowns
    private float timer;
    private GameManager gameManager;
    private float sliderFillAmount;

    public GameObject sliderBar; // Reference to the slider bar (parent object)
    public GameObject fillBar; // Reference to the fill bar (child object)

    void Start()
    {
        sliderBar.transform.localScale = new Vector2(2f, 0.2f);
        timer = survivalTime;
        gameManager = FindObjectOfType<GameManager>(); // Find GameManager instance
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Drown();
            Debug.Log("Person drowned!");
        }
        
        // Update the fill bar's scale to match the remaining time
        if (sliderBar)
        {
            UpdateSliderBar();
        }
        else
        {
            Debug.LogError("Slider bar or fill bar not assigned in the Inspector!");
        }
        
        
    }

    private void Drown()
    {
        Destroy(gameObject);
    }

    public void Collect()
    {
            // Check if the game manager can add a person
            if (gameManager.IsCapacityReached())
            {
                Debug.Log("Cannot rescue person. Capacity reached!");
            }
            else
            {
                gameManager.BringOnBoard(); // Call BringOnBoard to update count
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