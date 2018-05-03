using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField] private float speed = 0.1f;
    
	void Start () {
		
	}

	void Update () {
        Vector3 v = new Vector3();
        v.x = Input.GetAxis("Horizontal");
        v.z = Input.GetAxis("Vertical");
        v = Vector3.ClampMagnitude(v, 1f) * speed;

        transform.position += v;
    }
}
