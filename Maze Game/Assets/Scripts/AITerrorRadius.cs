using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITerrorRadius : MonoBehaviour
{
    public AIController aiController;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the radius and trigger the terror growl
        if (other.CompareTag("Player") && !aiController.IsChasing)
        {
            aiController.PlayTerror();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the player is still within the radius and terror growl is not playing
        if (other.CompareTag("Player") && !aiController.IsChasing && !aiController.IsTerrorRadiusPlaying)
        {
            aiController.PlayTerror(); // Start the growl if not already playing
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Stop playing the growl when the player exits the terror radius
        if (other.CompareTag("Player"))
        {
            aiController.StopTerrorGrowl();
        }
    }
}
