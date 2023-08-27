using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScript : MonoBehaviour
{
    public Material newMaterial; // The material you want to apply

    private Renderer objectRenderer; // Reference to the Renderer component

    private void Start()
    {
        // Get the Renderer component of the object
        objectRenderer = GetComponent<Renderer>();

        // Check if a material is assigned to the script
        if (newMaterial != null)
        {
            // Apply the new material to the object's renderer
            objectRenderer.material = newMaterial;
        }
        else
        {
            Debug.LogWarning("No material assigned to the MaterialScript.");
        }
    }
}
