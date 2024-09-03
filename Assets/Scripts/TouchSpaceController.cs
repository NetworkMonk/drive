using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchSpaceController : MonoBehaviour, IPointerDownHandler
{
    private GameObject[] signposts;

    // Start is called before the first frame update
    void Start()
    {
        signposts = GameObject.FindGameObjectsWithTag("Signpost");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("TouchSpaceController: OnPointerDown");

        // Loop through each game object
        foreach (GameObject signpost in signposts)
        {
            // Get the SignTriggerScript component
            SignTriggerScript signTrigger = signpost.GetComponent<SignTriggerScript>();

            // If the component is found, call the TouchTriggered method
            if (signTrigger != null)
            {
                signTrigger.TouchTriggered();
            }
        }
    }
}
