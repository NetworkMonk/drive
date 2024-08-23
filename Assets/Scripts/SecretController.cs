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
        Transform playerParent = GetTaggedParent(other.transform, "Player");
        if (!hasTriggered && playerParent != null)
        {
            StatsController statsController = playerParent.gameObject.GetComponent<StatsController>();
            if (statsController != null)
            {
                statsController.IncrementSecretsCount();
                hasTriggered = true;
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
