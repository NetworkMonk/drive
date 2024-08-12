using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EngineScript : MonoBehaviour
{
    [SerializeField] private AnimationCurve engineCurve;
    public float maxSpeed = 50.0f;
    public float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;
    }

    public float GetEngineTorque(float currentVelocity)
    {
        float normalizedVelocity = currentVelocity / maxSpeed;
        float torque = engineCurve.Evaluate(normalizedVelocity);
        return torque;
    }
}
