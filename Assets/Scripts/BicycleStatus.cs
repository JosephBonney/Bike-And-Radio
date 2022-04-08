using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SBPScripts;

public class BicycleStatus : MonoBehaviour
{
    public bool onBike = true;
    public GameObject cyclist;
    [Header("Box Collider")]
    public GameObject collisionBox;
    bool prevOnBike;
    BicycleController bicycleController;
    // Start is called before the first frame update
    void Start()
    {
        bicycleController = GetComponent<BicycleController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onBike != prevOnBike)
        {
            if(onBike)
            {
                bicycleController.enabled = true;
                cyclist.SetActive(true);
                bicycleController.fPhysicsWheel.GetComponent<SphereCollider>().enabled = true;
                bicycleController.rPhysicsWheel.GetComponent<SphereCollider>().enabled = true;
                collisionBox.GetComponent<BoxCollider>().enabled = false;
                
            }
            else
            {
                bicycleController.rb.centerOfMass = GetComponent<BoxCollider>().center;
                bicycleController.enabled = false;
                cyclist.SetActive(false);
                bicycleController.fPhysicsWheel.GetComponent<SphereCollider>().enabled = false;
                bicycleController.rPhysicsWheel.GetComponent<SphereCollider>().enabled = false;
                collisionBox.GetComponent<BoxCollider>().enabled = true;

            }
        }
        prevOnBike = onBike;
        
    }
}
