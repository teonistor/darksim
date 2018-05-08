using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObstacleDetector : MonoBehaviour {
    [SerializeField] private Player.Side side;

    private Player parent;

    void Start () {
        parent = GetComponentInParent<Player>();
    }

    void OnTriggerEnter (Collider other) {
        parent[side]++;
    }

    void OnTriggerExit(Collider other) {
        parent[side]--;
    }
}
