using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringCurve : MonoBehaviour
{
    [SerializeField] private AnimationCurve steeringCurve;
    public float maxSpeed = 50.0f;
    public float velocity = 0.0f;
    public float steeringAngle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetSteeringAmount(float currentVelocity)
    {
        velocity = currentVelocity >= 0.0f ? currentVelocity : -(currentVelocity);
        steeringAngle = steeringCurve.Evaluate(velocity / maxSpeed);
        return steeringAngle;
    }
}
