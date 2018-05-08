using UnityEngine;

/// <summary>
/// Floating UI sprite monitoring player health
/// </summary>
public class HealthBar : MonoBehaviour {

    private SpriteMask mask;
    private float t;

    void Start () {
        mask = GetComponent<SpriteMask>();
        t = 0.9f;
    }

    void LateUpdate () {
        t = Mathf.MoveTowards(t, Player.Health, 0.7f * Time.deltaTime);
        mask.alphaCutoff = 0.999f - t * 0.998f;
    }
}
