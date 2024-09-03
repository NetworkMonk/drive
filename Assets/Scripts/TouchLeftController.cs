using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchLeftController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private GameObject player;
    private List<WheelSteeringScript> steeringControllers;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            steeringControllers = new List<WheelSteeringScript>(player.GetComponentsInChildren<WheelSteeringScript>());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("TouchLeftController: OnPointerDown");
        if (steeringControllers != null)
        {
            foreach (var physicsController in steeringControllers)
            {
                physicsController.touchLeft = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (steeringControllers != null)
        {
            foreach (var physicsController in steeringControllers)
            {
                physicsController.touchLeft = false;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (steeringControllers != null)
        {
            foreach (var physicsController in steeringControllers)
            {
                physicsController.touchLeft = false;
            }
        }
    }
}
