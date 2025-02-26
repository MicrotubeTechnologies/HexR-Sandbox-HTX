
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.UIElements;
using AshqarApps.DynamicJoint;
using Unity.VisualScripting;
//using Microsoft.MixedReality.Toolkit.Input;
//using Microsoft.MixedReality.Toolkit.Utilities;
//using UnityEngine.XR.OpenXR.Input;


public class HandMatch : MonoBehaviour
{
    public GameObject RobotTarget;
    private string hand;
    private string hand_Short, Hexr_hand_Short;
    private Transform[] targetJoints = new Transform[26];
    public Transform MetahandRoot, RoboHandRoot;
    private Transform[] followingJoints = new Transform[26];
    private Vector3 targePosition = new Vector3();
    private Quaternion targeRotation = new Quaternion();
    private Rigidbody rb;
    private bool isNear = false;
    //public bool leftHand;
    public Vector3 rotOffsetPalm = new Vector3(0, 0, 0);
    public Vector3 rotOffsetFinger = new Vector3(0, 0, 0);
    public Material Red, Green;
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    private Vector3 PreviousLocation;
    public AudioController Controller;
    private float timer = 0f;  // Timer to track time
    private float interval = 0.16f;  // Interval of 0.1 seconds
    void Start()
    {
        PreviousLocation = RobotTarget.transform.position;
        GameObject RoboHand = RoboHandRoot.gameObject;
        GameObject ParenHand = MetahandRoot.gameObject;
        hand = "Right";
            hand_Short = "b_r";
            Hexr_hand_Short = "R";

        rb = GetComponent<Rigidbody>();

        // Link Hexr hand position and rotation to Meta hand position and rotation
        #region Meta Hands Mapping
        targetJoints[0] = FindChildRecursive(ParenHand, hand_Short + "_thumb0").transform;
        targetJoints[1] = targetJoints[0].GetChild(0);
        targetJoints[2] = targetJoints[1].GetChild(0);
        targetJoints[3] = targetJoints[2].GetChild(0);

        targetJoints[4] = FindChildRecursive(ParenHand, hand_Short + "_index1").transform;
        targetJoints[5] = targetJoints[4].GetChild(0);
        targetJoints[6] = targetJoints[5].GetChild(0);
        targetJoints[7] = targetJoints[6].GetChild(2);

        targetJoints[8] = FindChildRecursive(ParenHand, hand_Short + "_middle1").transform;
        targetJoints[9] = targetJoints[8].GetChild(0);
        targetJoints[10] = targetJoints[9].GetChild(0);
        targetJoints[11] = targetJoints[10].GetChild(2);

        targetJoints[12] = FindChildRecursive(ParenHand, hand_Short + "_ring1").transform;
        targetJoints[13] = targetJoints[12].GetChild(0);
        targetJoints[14] = targetJoints[13].GetChild(0);
        targetJoints[15] = targetJoints[14].GetChild(2);

        targetJoints[16] = FindChildRecursive(ParenHand, hand_Short + "_pinky0").transform;
        targetJoints[17] = targetJoints[16].GetChild(0);
        targetJoints[18] = targetJoints[17].GetChild(0);
        targetJoints[19] = targetJoints[18].GetChild(0);
        targetJoints[20] = targetJoints[19].GetChild(0);

        if (hand_Short == "b_l")
        {
            targetJoints[21] = FindChildRecursive(ParenHand, "l_palm_center_marker").transform;
        }
        else
        {
            targetJoints[21] = FindChildRecursive(ParenHand, "r_palm_center_marker").transform;
        }
        targetJoints[22] = MetahandRoot;
        #endregion

        #region HexR Hands Mapping
        followingJoints[0] = FindChildRecursive(RoboHand, hand_Short + "_thumb0").transform;
        followingJoints[1] = followingJoints[0].GetChild(0);
        followingJoints[2] = followingJoints[1].GetChild(0);
        followingJoints[3] = followingJoints[2].GetChild(0);

        followingJoints[4] = FindChildRecursive(RoboHand, hand_Short + "_index1").transform;
        followingJoints[5] = followingJoints[4].GetChild(0);
        followingJoints[6] = followingJoints[5].GetChild(0);
        followingJoints[7] = followingJoints[6].GetChild(2);

        followingJoints[8] = FindChildRecursive(RoboHand, hand_Short + "_middle1").transform;
        followingJoints[9] = followingJoints[8].GetChild(0);
        followingJoints[10] = followingJoints[9].GetChild(0);
        followingJoints[11] = followingJoints[10].GetChild(2);

        followingJoints[12] = FindChildRecursive(RoboHand, hand_Short + "_ring1").transform;
        followingJoints[13] = followingJoints[12].GetChild(0);
        followingJoints[14] = followingJoints[13].GetChild(0);
        followingJoints[15] = followingJoints[14].GetChild(2);

        followingJoints[16] = FindChildRecursive(RoboHand, hand_Short + "_pinky0").transform;
        followingJoints[17] = followingJoints[16].GetChild(0);
        followingJoints[18] = followingJoints[17].GetChild(0);
        followingJoints[19] = followingJoints[18].GetChild(0);
        followingJoints[20] = followingJoints[19].GetChild(0);
       if (hand_Short == "b_l")
        {
            followingJoints[21] = FindChildRecursive(RoboHand, "l_palm_center_marker").transform;
        }
        else
        {
            followingJoints[21] = FindChildRecursive(RoboHand, "r_palm_center_marker").transform;
        }
        followingJoints[22] = RoboHandRoot;
        #endregion
    }


    void FixedUpdate()
    {
        try
        {
            if(isNear == true)
            {
                timer += Time.deltaTime;

                // Check if the timer has reached or passed the interval
                if (timer >= interval)
                {
                    // position
                    rb.velocity = (targePosition - transform.position) / Time.fixedDeltaTime;

                    // Reset the timer (if exact timing isn't critical, you can also use `timer -= interval;`)
                    timer = 0f;
                }

                // rotation
              Quaternion deltaRotation = targeRotation * Quaternion.Inverse(rb.rotation);
                deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
                if (angle > 180f) { angle -= 360f; };
                Vector3 angularVelocity = angle * axis * Mathf.Deg2Rad / Time.fixedDeltaTime;
                if (float.IsNaN(angularVelocity.x) | float.IsNaN(angularVelocity.y) | float.IsNaN(angularVelocity.z)) { return; }
                rb.angularVelocity = angle * axis * Mathf.Deg2Rad / Time.fixedDeltaTime;
            }

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            //logText2.text += "\n" + e.ToString();
        }
    }


    void Update()
    {
        SyncronizeChecker();
        try
        {
            if(isNear == true)
            {
                //MatchWorldRotationXOnly();
                targePosition = targetJoints[22].position;
                targeRotation = targetJoints[22].rotation * Quaternion.Euler(rotOffsetPalm);
                for (int i = 0; i < 20; i++)
                {
                    followingJoints[i].localPosition = targetJoints[i].localPosition;
                    followingJoints[i].localRotation = targetJoints[i].localRotation * Quaternion.Euler(rotOffsetFinger);
                    targetJoints[i].gameObject.SetActive(false);

                }
            }

        }
        catch
        {

        }
    }
    void MatchWorldRotationXOnly()
    {
        // Get the world rotation of the target object
        Vector3 targetRotation = targetJoints[22].transform.eulerAngles;

        // Get the current world rotation of this object
        Vector3 currentRotation = followingJoints[22].eulerAngles;

        // Set this object's X rotation to match the target object's X rotation
        currentRotation.x = targetRotation.x;

        // Apply the new world rotation
        followingJoints[22].eulerAngles = currentRotation;
    }
    public void SyncronizeChecker()
    {
        // Calculate the distance between the two objects
        float distance1 = Vector3.Distance(targetJoints[22].transform.position, followingJoints[22].transform.position);
        float distance2 = Vector3.Distance(targetJoints[21].transform.position, followingJoints[21].transform.position);
        float proximityThreshold = 0.2f;
        // Check if the distance is less than or equal to the proximity threshold
        if (distance1 <= proximityThreshold && distance2 <= proximityThreshold)
        {
            isNear = true;
            SkinnedMeshRenderer.material = Green;
            RobotTarget.transform.position = targetJoints[22].transform.position;
            PreviousLocation = targetJoints[22].transform.position;
            if(Controller != null)
            {
                Controller.PlayClipTwo();
            }
        }
        else
        {
            isNear = false;
            SkinnedMeshRenderer.material = Red;
            RobotTarget.transform.position = PreviousLocation;
        }
    }
    public GameObject FindChildRecursive(GameObject parent, string childName)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.gameObject.name == childName)
            {
                return child.gameObject;
            }
            GameObject result = FindChildRecursive(child.gameObject, childName);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

}

