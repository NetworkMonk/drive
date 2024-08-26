using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchResetController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("TouchResetController: OnPointerDown");

        // Find the Player GameObject
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Get the CheckpointController component
            CheckpointController checkpointController = player.GetComponent<CheckpointController>();
            if (checkpointController != null)
            {
                // Call the ResetPlayer method
                checkpointController.ResetPlayer();
            }
            else
            {
                Debug.LogError("CheckpointController component not found on Player GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }
}
