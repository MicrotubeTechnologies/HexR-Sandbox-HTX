using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HaptGlove;
using TMPro;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using HexR;
public class PatientBodyHaptics : MonoBehaviour
{
    private HaptGloveHandler RightgloveHandler, LeftgloveHandler;
    public GameObject RightHand, LeftHand;
    private bool Hovering = false;
    public PressureTrackerMain RightTracker, LeftTracker;
    public TextMeshProUGUI BodyParts;
    //This allows an object to send a haptic feedback to the hexr glove.
    //Place this script in the gameobject with a trigger collider.

    // Start is called before the first frame update
    void Start()
    {
        RightgloveHandler = RightHand.GetComponent<HaptGloveHandler>();
        LeftgloveHandler = LeftHand.GetComponent<HaptGloveHandler>();  
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PressingBodyHover()
    {
        //Patient body haptics using fingers to press the different part of body
        //Exclude thumb and palm
        // ClutchState affecting all indenters
        if (RightTracker.PokeHovering == true)// to check which hand is interacting
        {
            if (Hovering == false)
            {
                Hovering = true;
                byte[][] ClutchState = new byte[][] {  new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }, new byte[] { 5, 0 } };
                byte[] btData = RightgloveHandler.haptics.ApplyHaptics(ClutchState, (byte)20, false);
                RightgloveHandler.BTSend(btData);
            }
        }
        if (LeftTracker.PokeHovering == true)// to check which hand is interacting
        {
            if (Hovering == false)
            {
                Hovering = true;
                byte[][] ClutchState = new byte[][] { new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }, new byte[] { 5, 0 } };
                byte[] btData = LeftgloveHandler.haptics.ApplyHaptics(ClutchState, (byte)20, false);
                LeftgloveHandler.BTSend(btData);
            }
        }

    }
    public void PressingBodyHoverExit()
    {
        //Patient body haptics using fingers to press the different part of body
        //Exclude thumb and palm
        if(Hovering == true)
        {
            Hovering = false;
            byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 2 },
                               new byte[] { 3, 2 }, new byte[] { 4, 2 } ,new byte[] { 5, 2 },};
            byte[] btData = RightgloveHandler.haptics?.ApplyHaptics(ClutchState, (byte)60, false);
            RightgloveHandler?.BTSend(btData);
        }
        if (Hovering == true)
        {
            Hovering = false;
            byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 2 },
                               new byte[] { 3, 2 }, new byte[] { 4, 2 } ,new byte[] { 5, 2 },};
            byte[] btData = LeftgloveHandler.haptics?.ApplyHaptics(ClutchState, (byte)60, false);
            LeftgloveHandler?.BTSend(btData);
        }

    }
    public void BodyLightPressed()
    {
        //Patient body haptics using fingers to press the different part of body
        //Exclude thumb and palm
        //Patient body haptics using fingers to press the different part of body
        //Exclude thumb and palm
        if (RightTracker.PokeHovering == true)// to check which hand is interacting
        {
            byte[][] ClutchState = new byte[][] {  new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }, new byte[] { 5, 0 } };
            byte[] btData = RightgloveHandler.haptics?.ApplyHaptics(ClutchState, (byte)30, false);
            RightgloveHandler?.BTSend(btData);
        }
        if (LeftTracker.PokeHovering == true)// to check which hand is interacting
        {
            byte[][] ClutchState = new byte[][] {  new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }, new byte[] { 5, 0 } };
            byte[] btData = LeftgloveHandler.haptics?.ApplyHaptics(ClutchState, (byte)30, false);
            LeftgloveHandler?.BTSend(btData);
        }
    }
    public void BodyMediumPressed()
    {
        //Patient body haptics using fingers to press the different part of body
        //Exclude thumb and palm
        //Patient body haptics using fingers to press the different part of body
        //Exclude thumb and palm
        if (RightTracker.PokeHovering == true)// to check which hand is interacting
        {
            byte[][] ClutchState = new byte[][] {  new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }, new byte[] { 5, 0 } };
            byte[] btData = RightgloveHandler.haptics?.ApplyHaptics(ClutchState, (byte)40, false);
            RightgloveHandler?.BTSend(btData);
        }
        if (LeftTracker.PokeHovering == true)// to check which hand is interacting
        {
            byte[][] ClutchState = new byte[][] {  new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }, new byte[] { 5, 0 } };
            byte[] btData = LeftgloveHandler.haptics?.ApplyHaptics(ClutchState, (byte)40, false);
            LeftgloveHandler?.BTSend(btData);
        }
    }
    public void BodyHardPressed()
    {
        //Patient body haptics using fingers to press the different part of body
        //Exclude thumb and palm
        //Patient body haptics using fingers to press the different part of body
        //Exclude thumb and palm
        if (RightTracker.PokeHovering == true)// to check which hand is interacting
        {
            byte[][] ClutchState = new byte[][] { new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }, new byte[] { 5, 0 } };
            byte[] btData = RightgloveHandler.haptics?.ApplyHaptics(ClutchState, (byte)60, false);
            RightgloveHandler?.BTSend(btData);
        }
        if (LeftTracker.PokeHovering == true)// to check which hand is interacting
        {
            byte[][] ClutchState = new byte[][] { new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }, new byte[] { 5, 0 } };
            byte[] btData = LeftgloveHandler.haptics?.ApplyHaptics(ClutchState, (byte)60, false);
            LeftgloveHandler?.BTSend(btData);
        }
    }
    public void PressingBodyUnpressed()
    {
        //Patient body haptics using fingers to press the different part of body
        //Exclude thumb and palm
        //Patient body haptics using fingers to press the different part of body
        //Exclude thumb and palm
        byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 2 },
                               new byte[] { 3, 2 }, new byte[] { 4, 2 } ,new byte[] { 5, 2 },};
        byte[] btData = RightgloveHandler?.haptics.ApplyHaptics(ClutchState, (byte)60, false);
        RightgloveHandler?.BTSend(btData);
        byte[] LbtData = LeftgloveHandler?.haptics.ApplyHaptics(ClutchState, (byte)60, false);
        LeftgloveHandler?.BTSend(LbtData);
        Hovering = false;
    }

    public void TextMiddle1()
    {
        BodyParts.text = "Right Lumbar";
    }
    public void TextMiddle2()
    {
        BodyParts.text = "Umbilical";
    }
    public void TextMiddle3()
    {
        BodyParts.text = "Left Lumbar";
    }
    public void TextTop1()
    {
        BodyParts.text = "Right Hypochondriac";
    }
    public void TextTop2()
    {
        BodyParts.text = "Epigastric";
    }
    public void TextTop3()
    {
        BodyParts.text = "Left Hypochondriac";
    }
    public void TextBtm1()
    {
        BodyParts.text = "Right Iliac";
    }
    public void TextBtm2()
    {
        BodyParts.text = "Hypogastric";
    }
    public void TextBtm3()
    {
        BodyParts.text = "Left Iliac";
    }
}
