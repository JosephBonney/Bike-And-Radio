using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using SBPScripts;

public class BicycleStatus : MonoBehaviour
{
    public bool isRidingBike = true;
    public GameObject cyclist;
    public BoxCollider Triggger;
    public BicycleController BC;

    // Start is called before the first frame update
    void Start()
    {
        BC = gameObject.GetComponent<BicycleController>();
    }

    // Update is called once per frame
    void Update()
    {
        RideBike();
    }

    public void RideBike()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (isRidingBike == true)
            {
                Debug.Log("Key Pressed, Off Bike");
                BC.enabled = false;
                cyclist.SetActive(false);
            }
            if (isRidingBike == false)
            {
                Debug.Log("Key Pressed, On Bike");
                BC.enabled = true;
                cyclist.SetActive(true);
            }
        }
    }
}
