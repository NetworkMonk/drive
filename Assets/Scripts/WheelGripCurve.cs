using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGripCurve : MonoBehaviour
{
    [SerializeField] private AnimationCurve gripCurve;
    public float maxSlip = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetGrip(float slipVelocity)
    {
        return gripCurve.Evaluate(slipVelocity / maxSlip);
    }
}
