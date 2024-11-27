using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int personsOnBoard = 0;
    public int capacity;
    public UnityEvent OnPersonRescued;

    public void BringOnBoard()
    {
        personsOnBoard++;
        OnPersonRescued.Invoke();
        Debug.Log("Person brought on board!");
    }

    public bool IsCapacityReached()
    {
        return personsOnBoard >= capacity;
    }
}