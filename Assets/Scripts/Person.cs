using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation

public class Person : MonoBehaviour, ICollectible
{
    public float survivalTime; // Time in seconds before the person drowns
    private float timer;
    private GameManager gameManager;
    private float sliderFillAmount;
    
    public GameObject sliderBar; // Reference to the slider bar (parent object)
    public GameObject fillBar; // Reference to the fill bar (child object)
    
    public UnityEvent OnPersonCollect;
    public UnityEvent OnPersonDrown;
    
    void Start()
    {
        Debug.Log(survivalTime + " seconds");
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
        OnPersonDrown.Invoke();
        Destroy(gameObject);
    }

    public void Collect()
    {
            // Check if the game manager can add a person
            if (gameManager.IsCapacityReached())
            {
                Debug.Log("Cannot rescue person. Capacity reached!");
                gameManager.WarningText.Invoke();
            }
            else
            {
                gameManager.BringOnBoard(); // Call BringOnBoard to update count
                OnPersonCollect.Invoke();
                Destroy(gameObject);
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