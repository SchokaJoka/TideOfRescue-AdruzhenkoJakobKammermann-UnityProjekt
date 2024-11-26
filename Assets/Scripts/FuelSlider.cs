using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSlider : MonoBehaviour
{
    public Slider fuelSlider;
    public BoatController boatController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fuelSlider.value = boatController.currentFuel / boatController.maxFuel;
    }
}
