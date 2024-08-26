using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class TouchControlsController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool IsMobileDevice();

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if (IsMobileDevice())
            {
                EnableTouchControls();
            }
            else
            {
                DisableTouchControls();
            }
        }
        else
        {
            gameObject.SetActive(Input.touchSupported);
        }
    }

    void EnableTouchControls()
    {
        gameObject.SetActive(true);
    }

    void DisableTouchControls()
    {
        gameObject.SetActive(false);
    }

}
