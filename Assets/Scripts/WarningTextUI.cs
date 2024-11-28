using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WarningTextUI : MonoBehaviour
{
    private TextMeshProUGUI temp;
    public GameManager gM;
    

    private void Start()
    {
        temp = gameObject.GetComponent<TextMeshProUGUI>();
        temp.color = new Color(255, 0, 0, 0);

    }

    public void ShowIslandDropText()
    {
        StartCoroutine(IslandDropText());
    }
    
    public void ShowWarningText()
    {
       StartCoroutine(WarningText());
    }
    
    
    IEnumerator WarningText()
    {
        temp.text = "Boat capacity reached! Carry the people to the island.";
        temp.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.5f);
        temp.color = new Color(1, 0, 0, 0);
        yield return new WaitForSeconds(0.5f);
        temp.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.5f);
        temp.color = new Color(1, 0, 0, 0);
        yield return new WaitForSeconds(0.5f);
        temp.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(2f);
        temp.color = new Color(1, 0, 0, 0);
    }

    IEnumerator IslandDropText()
    {
        temp.text = "You have rescued " + gM.personsRescued + " people to safety";
        temp.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(5f);
        temp.color = new Color(1, 0, 0, 0);
    }
}
