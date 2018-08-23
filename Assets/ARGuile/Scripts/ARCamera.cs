using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCamera : MonoBehaviour {

    public static Camera main;

    private void Awake()
    {
        main = GetComponent<Camera>();
    }

    public static Vector3 offsetPosition
    {
        get
        {
            return ARCamera.position + ARCamera.forward * 0.7f - ARCamera.up * 0.06f;
        }
    }

    public static Vector3 position
    {
        set
        {
            main.transform.position = value;
        }
        get
        {
            return main.transform.position;
        }
    }

    public static Vector3 forward
    {
        set
        {
            main.transform.forward = value;
        }
        get
        {
            return main.transform.forward;
        }
    }

    public static Vector3 up
    {
        set
        {
            main.transform.up = value;
        }
        get
        {
            return main.transform.up;
        }
    }
}
