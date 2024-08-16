using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripModifier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This method is called when a collision occurs
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the tag "player"
        if (other.gameObject.tag == "SlippyGround")
        {
            // Get all WheelPhysicsScript components attached to the player game object
            WheelPhysicsScript[] wheelPhysicsScripts = GetComponentsInChildren<WheelPhysicsScript>();

            // Set the gripModifier value to 0.1f
            foreach (var script in wheelPhysicsScripts)
            {
                script.gripModifier = 0.1f;
            }
        }
    }

    // This method is called when a collision ends
    void OnTriggerExit(Collider other)
    {
        // Check if the exited object has the tag "player"
        if (other.gameObject.tag == "SlippyGround")
        {
            bool isStillCollidingWithSlippyGround = false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.tag == "SlippyGround" && collider != other)
                {
                    isStillCollidingWithSlippyGround = true;
                    break;
                }
            }

            if (!isStillCollidingWithSlippyGround)
            {
                // Get all WheelPhysicsScript components attached to the player game object
                WheelPhysicsScript[] wheelPhysicsScripts = GetComponentsInChildren<WheelPhysicsScript>();

                // Set the gripModifier value back to 0.0f
                foreach (var script in wheelPhysicsScripts)
                {
                    script.gripModifier = 0.0f;
                }

            }
        }
    }
}
