using UnityEngine;

/// <summary>
/// Floating UI sprite monitoring player stamina
/// </summary>
public class StaminaBar : MonoBehaviour {
    private SpriteMask mask;

	void Start () {
        mask = GetComponent<SpriteMask>();
	}
	
	void LateUpdate () {
        mask.alphaCutoff = 0.999f - Player.Stamina * 0.998f;
    }
}
