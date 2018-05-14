using UnityEngine;

/// <summary>
/// Floating UI sprite monitoring player stamina
/// </summary>
public class StaminaBar : MonoBehaviour {
    private SpriteMask mask;
    private SpriteRenderer rend;
    private Color full;

    [SerializeField] private Color empty;

    void Start () {
        mask = GetComponent<SpriteMask>();
        rend = GetComponent<SpriteRenderer>();
        full = rend.color;
	}
	
	void LateUpdate () {
        mask.alphaCutoff = 0.999f - Player.Stamina * 0.998f;
        rend.color = Color.Lerp(empty, full, 2.5f * Player.Stamina);
    }
}
