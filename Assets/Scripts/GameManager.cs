using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startText;
    public Slider gameTimeSlider;
    public PersonSpawner personSpawner;
    public TextMeshProUGUI uiText;

    public int capacity;
    public int maxPersons;
    public int personsOnBoard = 0;
    public int personAlmostRescued;
    public int personsRescued = 0;
    private int remainingPeople;

    public float setgameTimer;
    public float gameTimer;
    private bool warningShown = false;      // Flag to track if the warning has been shown

    public UnityEvent OnPersonRescued;
    public UnityEvent OnGameOver;
    public UnityEvent OnGameWon;
    public UnityEvent WarningText;
    public UnityEvent PeopleOnIsland;


    void Start()
    {
        gameTimer = setgameTimer;
        Debug.Log("Game Timer: " + gameTimer);
        maxPersons = personSpawner.numberOfObjects;
        StartCoroutine(StartText());
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;
        gameTimeSlider.value = gameTimer / setgameTimer;
        uiText.text = "People on board: " + personsOnBoard + "/" + capacity + "\n" + "Remaining " + (maxPersons - personsRescued);

        if (gameTimer <= 0)
        {
            OnGameOver.Invoke();
        }

        if (personAlmostRescued >= maxPersons)
        {
            gameTimer = setgameTimer;
        }

        if (personsRescued >= maxPersons)
        {
            //Debug.Log(personsRescued);
            OnGameWon.Invoke();
        }

        // Trigger warning only once when capacity is reached
        if (IsCapacityReached() && !warningShown)
        {
            WarningText.Invoke();
            warningShown = true; // Set flag to true so the warning is not repeated
        }
        else if (!IsCapacityReached() && warningShown)
        {
            warningShown = false; // Reset flag when capacity is no longer reached
        }
    }

    public void BringOnBoard()
    {
        personsOnBoard++;
        personAlmostRescued++;
        OnPersonRescued.Invoke();
    }

    public bool IsCapacityReached()
    {
        return personsOnBoard >= capacity;
    }

    public void ResetCapacity()
    {
        if (personsOnBoard == 0)
        {
            return;
        }
        else
        {
            personsRescued += personsOnBoard;
            personsOnBoard = 0;
            
            if(personsRescued != maxPersons)
            {
                PeopleOnIsland.Invoke();
            }
        }
        
    }
    
    IEnumerator StartText()
    {
        yield return new WaitForSeconds(4);
        startText.SetActive(false);
    }
}
