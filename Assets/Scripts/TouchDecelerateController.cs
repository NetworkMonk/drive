using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDecelerateController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private GameObject player;
    private List<WheelPhysicsScript> physicsControllers;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            physicsControllers = new List<WheelPhysicsScript>(player.GetComponentsInChildren<WheelPhysicsScript>());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("TouchDecelerateController: OnPointerDown");
        if (physicsControllers != null)
        {
            foreach (var physicsController in physicsControllers)
            {
                physicsController.touchBrake = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (physicsControllers != null)
        {
            foreach (var physicsController in physicsControllers)
            {
                physicsController.touchBrake = false;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (physicsControllers != null)
        {
            foreach (var physicsController in physicsControllers)
            {
                physicsController.touchBrake = false;
            }
        }
    }
}
