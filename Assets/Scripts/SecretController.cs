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
        Transform playerParent = GetTaggedParent(other.transform, "Player");
        if (!hasTriggered && playerParent != null)
        {
            Debug.Log("Player triggered");
            StatsController statsController = playerParent.gameObject.GetComponent<StatsController>();
            if (statsController != null)
            {
                statsController.IncrementSecretsCount();
                hasTriggered = true;
            }
            else
            {
                Debug.Log("StatsController not found");
            }
        }
    }

    private Transform GetTaggedParent(Transform child, string tag)
    {
        Transform current = child;
        while (current != null)
        {
            if (current.CompareTag(tag))
            {
                return current;
            }
            current = current.parent;
        }
        return null;
    }
}
