using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexR;

public class ReleaseAllHaptics : MonoBehaviour
{
    public PressureTrackerMain pressureTrackerMain;
    private float timer = 0f;
    private bool HapticsRemoving = false;

    //This allows an object to send a haptic feedback to the hexr glove.
    //Place this script in the gameobject with a trigger collider.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            
        }
        else if(timer <= 0 && HapticsRemoving == true)
        {
            pressureTrackerMain.RemoveAllHaptics();
            HapticsRemoving = false;
        }
    }

    //Trigger 
    //0-6 (Thumb, Index, Middle, Ring, Pinky, Palm)
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Haptic Trigger")
        {
            timer = 2;
            HapticsRemoving = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Haptic Trigger")
        {
            timer = 2;
            HapticsRemoving = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {

    }
}
