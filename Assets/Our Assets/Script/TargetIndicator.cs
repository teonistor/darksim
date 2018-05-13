using System;
using UnityEngine;

public class TargetIndicator : MonoBehaviour {

    [SerializeField] private bool hideWhenTargetVisible;

    private Renderer rend;
    private Transform target;
    private Camera cam;
    private Transform player;
    private Func<bool> condition;

    // Use this for initialization
    void Start () {
	}

    public void Init (Transform target, Camera cam, Transform player, Func<bool> condition, Material material=null) {
        rend = GetComponent<Renderer>();
        this.target = target;
        this.cam = cam;
        this.player = player;
        this.condition = condition;
        if (material != null)
            rend.material = material;
    }

	void Update () {
		Vector3 delta = cam.WorldToScreenPoint(target.position);

        // Condition not met => hide indicator
        if (!condition()) {
            rend.enabled = false;

        // Target directly visible...
        } else if (delta.x > 0 && delta.x < cam.pixelWidth &&
                   delta.y > 0 && delta.y < cam.pixelHeight) {

            // Hiding required => hide
            if (hideWhenTargetVisible)
                rend.enabled = false;

            // Hiding not required => position indicator above target
            else {
                rend.enabled = true;

                delta.z = 2f; // Magic?
                transform.position = cam.ScreenToWorldPoint(delta);
            }

        // Target not on screen => Position indicator on the edge of screen
        } else {
            rend.enabled = true;

            float hw = cam.pixelWidth * 0.5f;
            float hh = cam.pixelHeight * 0.5f;

            delta = target.position - player.position;
            float angl = Mathf.Atan2(delta.z, delta.x);

            delta.x = Mathf.Cos(angl) * hw * 0.9f + hw;
            delta.y = Mathf.Sin(angl) * hh * 0.9f + hh;
            delta.z = 2f; // Magic

            transform.position = cam.ScreenToWorldPoint(delta);
        }
    }


}
