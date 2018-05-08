using UnityEngine;

public class Spin : MonoBehaviour {
    [SerializeField] private float speed = 0.5f;

    void FixedUpdate () {
        Vector3 rot = transform.localEulerAngles;
        rot.y += speed;
        transform.localEulerAngles = rot;
    }
}
