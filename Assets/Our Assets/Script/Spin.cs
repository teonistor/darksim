using UnityEngine;

public class Spin : MonoBehaviour {

    void FixedUpdate () {
        Vector3 rot = transform.localEulerAngles;
        rot.y += 0.5f;
        transform.localEulerAngles = rot;
    }
}
 