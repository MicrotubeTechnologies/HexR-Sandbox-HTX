using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowPulse : MonoBehaviour
{
    private Material material;
    private float frequency = 2.0f;
    private float amplitude = 0.6f;
    public bool Pulseit = false;

    void Update()
    {
        if(Pulseit)
        {
            Renderer renderer = GetComponent<Renderer>();
            material = renderer.material;
            // Your logic for adjusting emission intensity dynamically
            // For example, you can use Input, time-based changes, etc.
            // For demonstration purposes, we'll use a simple pulsating intensity.

            float pulseValue = amplitude * Mathf.Sin(Time.time * frequency) + 0.5f;
            Color currentColor = Color.Lerp(Color.yellow, Color.black, pulseValue);

            // Set the emission color
            SetEmissionColor(currentColor);
        }

    }

    void SetEmissionColor(Color color)
    {
        // Set the emission color
        material.SetColor("_EmissionColor", color);

        // Enable emission on the material
        material.EnableKeyword("_EMISSION");
    }
    public void AdjustPulse(bool condition)
    {
        Pulseit = condition;
        //material.DisableKeyword("_EMISSION");
    }
}
