using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Person : MonoBehaviour, ICollectible
{
    public float survivalTime = 3f;
    private bool _isRescued = false;
    protected float timer;
    
    public GameObject sliderBar; // Reference to the slider bar (parent object)
    public GameObject fillBar; // Reference to the fill bar (child object of the slider)

    // Start is called before the first frame update
    void Start()
    {
        timer = survivalTime;
        StartCoroutine(DrowningCountdown());
        Debug.Log("Timer started!");
        
        
        // Initialize the slider fill based on survivalTime
        if (sliderBar != null && fillBar != null)
        {
            // Set the initial scale of the fill bar
            //fillBar.transform.localScale = new Vector3(1, 1, 1); // Set to full scale initially
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Stop timer if the target is rescued
        if (_isRescued)
        {
            StopCoroutine(DrowningCountdown());
            Debug.Log("Timer stopped!");
        }
        
        // Update the fill bar's scale to match the remaining time
        if (sliderBar != null && fillBar != null)
        {
            float fillAmount = timer / survivalTime; // Get the fraction of remaining time
            fillBar.transform.localScale = new Vector2(fillAmount, 0.2f); // Adjust fill based on time
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
    
    IEnumerator DrowningCountdown()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        Drown();
        Debug.Log("Timer exceeded!");
    }
}
