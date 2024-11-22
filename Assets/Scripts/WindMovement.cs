using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMovement : MonoBehaviour
{

    public GameObject player;
    private float windSpeed;
    private BoatController _boatController;

    // Start is called before the first frame update
    void Start()
    {
        _boatController = player.gameObject.GetComponent<BoatController>();
    }

    // Update is called once per frame
    void Update()
    {
        windSpeed = _boatController.currentVelocity.magnitude;
        
        transform.localPosition = new Vector3(0, 0, windSpeed * Time.deltaTime);
    }

}
