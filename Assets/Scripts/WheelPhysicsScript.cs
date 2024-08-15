using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WheelPhysicsScript : MonoBehaviour
{
    public float restLength = 0.25f;
    public Transform carTransform;
    public Rigidbody carRigidBody;
    public EngineScript engineScript;
    public WheelGripCurve wheelGripCurve;
    public Transform wheelMeshTransform;
    public float springStrength = 100.0f;
    public float springDamper = 15.0f;
    public float wheelMeshRadius = 0.275f;

    public float wheelMass = 0.2f;

    public bool driveWheel = true;

    public float brakeStrength = 0.25f;
    public float decelerationStrength = 0.05f;
    public float gripModifier = 0.0f;

    private float currentSpringLength;


    // Start is called before the first frame update
    void Start()
    {
        currentSpringLength = restLength;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the position of the wheel so that we can render it correctly.
        if (!wheelMeshTransform)
        {
            return;
        }

        //wheelMeshTransform.position = transform.position + (Vector3.down * currentSpringLength);
        wheelMeshTransform.localPosition = new Vector3(wheelMeshTransform.localPosition.x, -currentSpringLength, wheelMeshTransform.localPosition.z);
        wheelMeshTransform.rotation = transform.rotation;
    }

    void FixedUpdate()
    {
        RaycastHit wheelRay;
        bool didHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out wheelRay, restLength);

        // Draw the ray cast to determine if there is force to be applied.
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * (didHit ? wheelRay.distance : restLength), didHit ? Color.red : Color.white);
        // Draw the direction the wheel is pointing in.
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * restLength, Color.blue);

        currentSpringLength = restLength - wheelMeshRadius;

        // Apply the force for the wheel to the car.
        if (didHit)
        {
            ApplySpringForce(wheelRay);
            ApplySlippageForce();
            ApplyDriveForce();
            ApplyBrakeForce();
            ApplyDecelerationForce();
            ApplyDownhillForce(wheelRay);
        }
    }

    private void ApplySpringForce(RaycastHit wheelRay)
    {
        Vector3 springDir = transform.up;
        Vector3 tireWorldVel = carRigidBody.GetPointVelocity(transform.position);
        float offset = restLength - wheelRay.distance;
        float vel = Vector3.Dot(springDir, tireWorldVel);
        float force = (offset * springStrength) - (vel * springDamper);
        carRigidBody.AddForceAtPosition(Vector3.up * force, transform.position);
        currentSpringLength = restLength - offset - wheelMeshRadius;
    }

    private void ApplySlippageForce()
    {
        Vector3 steeringDir = transform.right;
        Vector3 wheelWorldVelocity = carRigidBody.GetPointVelocity(transform.position);
        float steeringVel = Vector3.Dot(steeringDir, wheelWorldVelocity);
        float wheelGripFactor = wheelGripCurve.GetGrip(steeringVel);
        if (gripModifier > 0.0f)
        {
            wheelGripFactor = Mathf.Min(wheelGripFactor, gripModifier);
        }
        float desiredVelChange = -steeringVel * wheelGripFactor;
        float desiredAcceleration = desiredVelChange / Time.fixedDeltaTime;
        carRigidBody.AddForceAtPosition(steeringDir * desiredAcceleration * wheelMass, transform.position);
    }

    private void ApplyDriveForce()
    {
        if (driveWheel && Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 accelerationDir = transform.forward;
            float carSpeed = Vector3.Dot(carTransform.forward, carRigidBody.velocity);
            float availableTorque = engineScript.GetEngineTorque(Mathf.Abs(carSpeed));
            carRigidBody.AddForceAtPosition(accelerationDir * availableTorque, transform.position);
        }
    }

    private void ApplyBrakeForce()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 forwardDir = transform.forward;
            Vector3 wheelWorldVelocity = carRigidBody.GetPointVelocity(transform.position);
            float forwardVel = Vector3.Dot(forwardDir, wheelWorldVelocity);
            float desiredVelChange = -forwardVel * brakeStrength;
            float desiredAcceleration = desiredVelChange / Time.fixedDeltaTime;

            float stopThreshold = 0.1f; // Threshold to consider the car as stopped
            if (forwardVel > stopThreshold)
            {
                carRigidBody.AddForceAtPosition(forwardDir * desiredAcceleration * wheelMass, transform.position);
            }
            else
            {
                float carSpeed = Vector3.Dot(-carTransform.forward, carRigidBody.velocity) * 2.0f;
                float availableTorque = engineScript.GetEngineTorque(Mathf.Abs(carSpeed));
                carRigidBody.AddForceAtPosition(-forwardDir * availableTorque, transform.position);
            }
        }
    }

    private void ApplyDecelerationForce()
    {
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 forwardDir = transform.forward;
            Vector3 wheelWorldVelocity = carRigidBody.GetPointVelocity(transform.position);
            float forwardVel = Vector3.Dot(forwardDir, wheelWorldVelocity);
            float desiredVelChange = -forwardVel * decelerationStrength;
            float desiredAcceleration = desiredVelChange / Time.fixedDeltaTime;
            carRigidBody.AddForceAtPosition(forwardDir * desiredAcceleration * wheelMass, transform.position);
        }
    }

    private void ApplyDownhillForce(RaycastHit wheelRay)
    {
        Vector3 gravity = Physics.gravity;
        Vector3 groundNormal = wheelRay.normal;
        Vector3 gravityAlongIncline = Vector3.ProjectOnPlane(gravity, groundNormal);

        // Calculate the gradient of the hill
        float gradientAngle = Vector3.Angle(Vector3.up, groundNormal);

        // Compute an exponential factor based on the gradient
        float exponentialFactor = Mathf.Exp(gradientAngle / 45.0f); // Adjust the divisor to control the rate of increase
        carRigidBody.AddForce(gravityAlongIncline * (carRigidBody.mass / 16.0f) * exponentialFactor);
    }
}
