using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSteeringScript : MonoBehaviour
{
    public Transform carTransform;
    public Rigidbody carRigidBody;
    public SteeringCurve steeringCurve;

    public float maxSteeringAngle = 50.0f;
    public float smooth = 20.0f;

    public bool touchLeft = false;
    public bool touchRight = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float carSpeed = Vector3.Dot(carTransform.forward, carRigidBody.velocity);
        float maxSteeringAmount = steeringCurve.GetSteeringAmount(carSpeed);
        float desiredSteering = 0.0f;
        if (Input.GetKey(KeyCode.LeftArrow) || touchLeft)
        {
            desiredSteering -= maxSteeringAngle * maxSteeringAmount;
        }
        if (Input.GetKey(KeyCode.RightArrow) || touchRight)
        {
            desiredSteering += maxSteeringAngle * maxSteeringAmount;
        }

        float newAngle = Mathf.LerpAngle(transform.localEulerAngles.y, desiredSteering, Time.deltaTime * smooth);
        transform.localEulerAngles = new Vector3(0.0f, newAngle, 0.0f);
    }
}
