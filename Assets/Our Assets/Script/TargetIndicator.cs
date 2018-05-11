﻿using System;
using UnityEngine;

public class TargetIndicator : MonoBehaviour {

    private Renderer rend;
    private Transform target;
    private Camera cam;
    private Transform player;
    private Func<bool> condition;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
	}

    public void Init (Transform target, Camera cam, Transform player, Func<bool> condition) {
        this.target = target;
        this.cam = cam;
        this.player = player;
        this.condition = condition;
    }

    // Remember to delete indicator when collecting

	void Update () {
		Vector3 delta = cam.WorldToScreenPoint(target.position);

        // Target directly visible or condition not met => hide indicator
        if (!condition() ||
            delta.x > 0 && delta.x < cam.pixelWidth &&
            delta.y > 0 && delta.y < cam.pixelHeight) {
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
            delta = target.position - player.position;
            float angl = Mathf.Atan2(delta.z, delta.x);

            delta.x = Mathf.Cos(angl) * hw + hw;
            delta.y = Mathf.Sin(angl) * hh + hh;
            delta.z = 2f; // Magic?

            transform.position = cam.ScreenToWorldPoint(delta);
        }
    }


}
