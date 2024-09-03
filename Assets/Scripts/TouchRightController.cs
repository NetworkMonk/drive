using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchRightController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
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
        Debug.Log("TouchRightController: OnPointerDown");
        if (steeringControllers != null)
        {
            foreach (var physicsController in steeringControllers)
            {
                physicsController.touchRight = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (steeringControllers != null)
        {
            foreach (var physicsController in steeringControllers)
            {
                physicsController.touchRight = false;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (steeringControllers != null)
        {
            foreach (var physicsController in steeringControllers)
            {
                physicsController.touchRight = false;
            }
        }
    }
}
