using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
[SerializeField]
    private GameObject playerAvatar;

    Actions playerActions;

    public enum Side { Up, Right, Down, Left };


    [SerializeField] private float speed = 0.1f;

    private int[] blockedMove = new int[] { 0,0,0,0 };


void Start () {
        playerActions = playerAvatar.GetComponent<Actions>();
}


    public int this[Side s] {
        get {
            return blockedMove[(int)s];
        } set {
            blockedMove[(int)s] = value;
        }
    }

    private bool blockedUp {
        get {
            return this[Side.Up] > 0;
        }
    }

    private bool blockedRight {
        get {
            return this[Side.Right] > 0;
        }
    }

    private bool blockedDown {
        get {
            return this[Side.Down] > 0;
        }
    }

    private bool blockedLeft {
        get {
            return this[Side.Left] > 0;
        }
    }


	void Update () {
        Vector3 v = new Vector3();

        // Get movement input from axes
        v.x = Input.GetAxis("Horizontal");
        v.z = Input.GetAxis("Vertical");

        // Block movement when hitting stuff
        if (blockedUp && v.z > 0f) v.z = 0f;
        if (blockedRight && v.x > 0f) v.x = 0f;
        if (blockedDown && v.z < 0f) v.z = 0f;
        if (blockedLeft && v.x < 0f) v.x = 0f;

        // Normalise (no diagonal speedup)
        v = Vector3.ClampMagnitude(v, 1f) * speed;

        if (v.x != 0 || v.z != 0) {
            playerActions.Run();
            float angle = Mathf.Atan2(v.x, v.z) * Mathf.Rad2Deg;
            //playerAvatar.transform.rotation = Quaternion.Euler(Vector3.up * angle);

            Vector3 r = playerAvatar.transform.localEulerAngles;
            r.y = Mathf.MoveTowardsAngle(r.y, angle, 666 * Time.deltaTime);
            playerAvatar.transform.localEulerAngles = r;
        } else {
            playerActions.Stay();
        }
        transform.position += v;
    }
}
