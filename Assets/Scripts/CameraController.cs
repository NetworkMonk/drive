using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smooth = 2.0f;
    public bool smoothing = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (smoothing)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x - 3.0f, target.position.y + 5.0f, target.position.z - 5.0f), Time.deltaTime * smooth);
        }
        else
        {
            transform.position = new Vector3(target.position.x - 3.0f, target.position.y + 5.0f, target.position.z - 5.0f);
        }
    }
}
