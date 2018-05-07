using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour {

    //private RectTransform mask, bar;
    private SpriteMask mask;

	void Start () {
        //mask = GetComponent<RectTransform>();
        //transform.GetChild(0).GetComponent<RectTransform>();
        mask = GetComponent<SpriteMask>();
	}
	
	void LateUpdate () {
        // 1f -> 0.001, 0f -> 0.999
        mask.alphaCutoff = 0.999f - Movement.stamina * 0.998f;

        //Vector3 p = mask.position;
        //p.y = Movement.stamina * -100;
        //mask.position = p;

        //p = bar.position;
        //p.y = Movement.stamina * 100;
        //bar.position = p;
    }
}
