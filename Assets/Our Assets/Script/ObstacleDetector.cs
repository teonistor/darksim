﻿using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObstacleDetector : MonoBehaviour {
    [SerializeField] private Movement.Side side;

    private Movement parent;

    void Start () {
        parent = GetComponentInParent<Movement>();
        print(parent);
    }

    void OnTriggerEnter (Collider other) {
        parent.blockedMove[(int)side]++;
        print(other);
    }

    void OnTriggerExit(Collider other) {
        parent.blockedMove[(int)side]--;
    }
}
