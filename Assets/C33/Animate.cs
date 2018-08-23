using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {

    public Vector3 rotate;

	void LateUpdate () {
        transform.Rotate(rotate * Time.deltaTime, Space.Self);
	}
}
