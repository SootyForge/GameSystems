using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    public Transform[] lookObjects;     // Array of objects to look at
    public bool smooth = true;          // Is smoothing enabled?
    public float damping = 6;           // Smoothness value of camera

    [Header("GUI Button")]
    public int x = 10;
    public int y = 70;
    public int width = 50;
    public int height = 30;

    private int camIndex;               // Index into array of lookObjects
    private int camMax;                 // Stores the total amount of lookObjects
    private Transform target;           // Current target look object

    void Start()
    {
        // Last index of array = Array.Length - 1
        camMax = lookObjects.Length - 1;
    }

    void LateUpdate()
    {
        // Get current object to look at
        target = lookObjects[camIndex];

        // If target is not null
        if (target)
        {
            // Is smoothing enabled?
            if (smooth)
            {
                // Calculate direction to look at target
                Vector3 lookDirection = target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(lookDirection);
                // Look at and dampen the rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                // Just look at the target (without dampen)
                transform.LookAt(target);
            }
        }
        else
        {
            // Keep swapping cameras until a valid target is found
            CamSwap();
        }
    }

    // Use this for initialization
    void CamSwap()
    {
        // If camIndex exceeds array size
        if (camIndex >= camMax)
        {
            // Reset camIndex back to zero
            camIndex = 0;
        }
        else
        {
            // Increase camIndex to next look object
            camIndex++;
        }
    }

    void OnGUI()
    {
        // Render a button to the screen & check if it's pressed
        if (GUI.Button(new Rect(x, y, width, height), "Swap"))
        {
            // Swap the cameras
            CamSwap();
        }
    }
}