using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretController : MonoBehaviour
{
    private bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        Debug.Log(other.tag);
        if (!hasTriggered && IsChildOfTaggedParent(other.transform, "Player"))
        {
            Debug.Log("Player triggered");
            StatsController statsController = other.gameObject.GetComponent<StatsController>();
            if (statsController != null)
            {
                statsController.IncrementSecretsCount();
                hasTriggered = true;
            }
        }
    }

    private bool IsChildOfTaggedParent(Transform child, string tag)
    {
        Transform current = child;
        while (current != null)
        {
            if (current.CompareTag(tag))
            {
                return true;
            }
            current = current.parent;
        }
        return false;
    }
}
