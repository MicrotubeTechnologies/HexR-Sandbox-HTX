using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passthrough : MonoBehaviour
{
    private bool passthroughEnabled = false;
    public Camera cam;
    public GameObject Garden;
    //public Material skyboxGarden;
   // public Material skyboxBoileroom;
    void Start()
    {

    }

    void Update()
    {
        // Toggle Passthrough when the user presses the A button on the right controller
        if (Garden.activeInHierarchy && cam.clearFlags == CameraClearFlags.SolidColor)
        {
            EnablePassthrough();
           //RenderSettings.skybox = skyboxGarden;
        }
        else if(!Garden.activeInHierarchy && cam.clearFlags == CameraClearFlags.Skybox)
        {
            DisablePassthrough();
        }
    }


    private void EnablePassthrough()
    {
        cam.clearFlags = CameraClearFlags.Skybox;
        cam.backgroundColor = Color.white;

    }

    private void DisablePassthrough()
    {
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.clear;
    }
}
