using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugColliders : MonoBehaviour
{
    public Material transparentMaterial; // Assign a transparent material in the inspector
    public bool enableVisualizer = true; // Toggle to enable/disable visualization

    void Start()
    {
        if (enableVisualizer)
        {
            if (TryGetComponent(out BoxCollider box))
            {
                CreateColliderVisualizer(PrimitiveType.Cube, box.size);
            }
            else if (TryGetComponent(out CapsuleCollider capsule))
            {
                CreateColliderVisualizer(PrimitiveType.Capsule, new Vector3(capsule.radius * 2, capsule.height, capsule.radius * 2));
            }
        }


    }

    void CreateColliderVisualizer(PrimitiveType shape, Vector3 size)
    {
        GameObject visualizer = GameObject.CreatePrimitive(shape);
        Destroy(visualizer.GetComponent<Collider>()); // Remove unnecessary collider
        visualizer.transform.SetParent(transform);
        visualizer.transform.localPosition = Vector3.zero;
        visualizer.transform.localRotation = Quaternion.identity;
        visualizer.transform.localScale = size;

        if (transparentMaterial != null)
            visualizer.GetComponent<Renderer>().material = transparentMaterial;
    }
}
