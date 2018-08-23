using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RigidInfo
{
    public Vector3 velocity;
    public Vector3 acceleration;
}
public class RigidBehaviour : MonoBehaviour {

    public RigidInfo rbody;

    protected virtual void Update()
    {
        rbody.velocity += ( rbody.acceleration + Physics.gravity ) * Time.deltaTime;
        transform.position += rbody.velocity * Time.deltaTime;
    }

}
