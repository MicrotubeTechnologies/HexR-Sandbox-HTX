using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMatch : MonoBehaviour
{
    [Header("Hinge Limits")]
    public GameObject targetObject;  // The target object to mirror the rotation from
    public Vector3 axisMask = Vector3.one;  // Axis mask to control which axis to mirror (1 = mirror, 0 = ignore)
    public float hingeAngleOffset = 0;

    private Quaternion lastRotation = Quaternion.identity;

    void Update()
    {
        if (targetObject != null)
        {
            MirrorRotation();
        }
    }

    /*
    * Mirror the rotation from the target object based on the desired axis
    * */
    public void MirrorRotation()
    {
        // Get the local rotation of the target object
        Vector3 targetEulerAngles = targetObject.transform.localEulerAngles;

        // Get the current local rotation of this object
        Vector3 currentEulerAngles = transform.localEulerAngles;

        // Apply the axis mask to only affect the desired axes
        currentEulerAngles.x = axisMask.x == 1 ? targetEulerAngles.x + hingeAngleOffset : currentEulerAngles.x;
        currentEulerAngles.y = axisMask.y == 1 ? targetEulerAngles.y + hingeAngleOffset : currentEulerAngles.y;
        currentEulerAngles.z = axisMask.z == 1 ? targetEulerAngles.z + hingeAngleOffset : currentEulerAngles.z;

        // Apply the new mirrored rotation
        transform.localEulerAngles = currentEulerAngles;
    }

    public Quaternion LimitHinge(Quaternion rotation)
    {
        // Example of limiting the hinge's rotation (optional)
        return rotation;  // You can keep the original rotation limiting logic here if needed
    }

    public Quaternion LimitRotation(Quaternion rotation, float jointLimitStrength = 1)
    {
        lastRotation = LimitHinge(rotation);
        return lastRotation;
    }

    public bool Apply(float jointLimitStrength = 1)
    {
        // Example of applying hinge limits
        return false;  // Implement limit checks here if needed
    }
}

