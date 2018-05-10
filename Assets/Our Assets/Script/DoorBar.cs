using UnityEngine;

/// <summary>
/// Floating UI sprite monitoring progress towards opening the door
/// </summary>
public class DoorBar : MonoBehaviour {

    [SerializeField] private SpriteRenderer icon;
    private SpriteRenderer rend;
    private SpriteMask mask;
    private float t;
    private bool incomplete;

    void Start () {
        rend = GetComponent<SpriteRenderer>();
        mask = GetComponent<SpriteMask>();
        t = 0f;
        incomplete = true;
    }

    void LateUpdate () {
        if (incomplete) {
            t = Mathf.MoveTowards(t, (float)Difficulty.KeysCollected / Difficulty.KeysNecessary, 0.7f * Time.deltaTime);
            mask.alphaCutoff = 1f - t;
            if (t == 1f) {
                icon.color = rend.color;
                incomplete = false;
            }
        }
    }
}
