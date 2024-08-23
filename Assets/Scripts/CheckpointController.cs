using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public GameObject currentCheckpoint;
    public GameObject finalCheckpoint;
    private Rigidbody rb;
    private StatsController statsController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        statsController = GetComponent<StatsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentCheckpoint)
            {
                transform.position = currentCheckpoint.transform.position + (Vector3.up * 0.5f);
                transform.rotation = currentCheckpoint.transform.rotation;
            }
            else
            {
                transform.position = Vector3.up * 0.5f;
                transform.eulerAngles = Vector3.zero;
            }
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            if (statsController != null)
            {
                statsController.IncrementResetCount(); // Call the incrementResetCount method
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            if (currentCheckpoint == other.gameObject)
            {
                return;
            }

            currentCheckpoint = other.gameObject;
            if (currentCheckpoint == finalCheckpoint)
            {
                statsController.StopTimer(); // Call the StopTimer method
            }
            
            // Get the CheckpointConfettiController component and call RestartAllParticleSystems
            CheckpointConfettiController confettiController = currentCheckpoint.GetComponent<CheckpointConfettiController>();
            if (confettiController != null)
            {
                confettiController.RestartAllParticleSystems();
            }
        }
    }
}
