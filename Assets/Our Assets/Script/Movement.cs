using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public enum Side { Up, Right, Down, Left };


    [SerializeField] private float speed = 0.1f;

    public int[] blockedMove = new int[] { 0,0,0,0 };
    

    private bool blockedUp {
        get {
            return blockedMove[(int)Side.Up] > 0;
        }
    }

    private bool blockedRight {
        get {
            return blockedMove[(int)Side.Right] > 0;
        }
    }

    private bool blockedDown {
        get {
            return blockedMove[(int)Side.Down] > 0;
        }
    }

    private bool blockedLeft {
        get {
            return blockedMove[(int)Side.Left] > 0;
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

        // Move
        transform.position += v;
    }
}
