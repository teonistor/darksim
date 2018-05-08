using System.Collections;
using System;
using UnityEngine;

public class TargetIndicator : MonoBehaviour {

    private Renderer rend;
    private Transform target;
    private Camera cam;
    private Transform player;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
	}

    public void Init (Transform target, Camera cam, Transform player) {
        this.target = target;
        this.cam = cam;
        this.player = player;
    }

    // Remember to delete indicator when collecting

	void Update () {
		Vector3 scrp = cam.WorldToScreenPoint(target.position);

        // Target directly visible => hide indicator
        if (scrp.x > 0 && scrp.x < cam.pixelWidth && scrp.y > 0 && scrp.y < cam.pixelHeight) {
            rend.enabled = false;

        // Position indicator on the edge of screen
        } else {
            rend.enabled = true;

            float hw = cam.pixelWidth * 0.5f;
            float hh = cam.pixelHeight * 0.5f;

            //scrp.x -= hw;
            //scrp.y -= hh;

            //scrp.x = hw - scrp.x;
            //scrp.y = hh - scrp.y;

            //float angl = Mathf.Atan(scrp.y/ scrp.x);
            scrp = target.position - player.position;
            float angl = Mathf.Atan2(scrp.z, scrp.x);

            scrp.x = Mathf.Cos(angl) * hw + hw;
            scrp.y = Mathf.Sin(angl) * hh + hh;
            scrp.z = 2f; // Magic?

            transform.position = cam.ScreenToWorldPoint(scrp);
        }
    }


}
