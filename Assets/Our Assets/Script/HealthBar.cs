using UnityEngine;

/// <summary>
/// Floating UI sprite monitoring player health
/// </summary>
public class HealthBar : MonoBehaviour {

    private SpriteMask mask;
    private SpriteRenderer rend;
    private Color full;
    private float t;

    [SerializeField] private Color empty;

    void Start () {
        mask = GetComponent<SpriteMask>();
        rend = GetComponent<SpriteRenderer>();
        full = rend.color;
        t = 0.9f;
    }

    void LateUpdate () {
        t = Mathf.MoveTowards(t, Player.Health, 0.7f * Time.deltaTime);
        mask.alphaCutoff = 0.999f - t * 0.998f;
        rend.color = Color.Lerp(empty, full, 2 * t);
    }
}
