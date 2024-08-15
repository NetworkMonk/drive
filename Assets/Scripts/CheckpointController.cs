using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public GameObject currentCheckpoint;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            // Get the CheckpointConfettiController component and call RestartAllParticleSystems
            CheckpointConfettiController confettiController = currentCheckpoint.GetComponent<CheckpointConfettiController>();
            if (confettiController != null)
            {
                confettiController.RestartAllParticleSystems();
            }
        }
    }
}
