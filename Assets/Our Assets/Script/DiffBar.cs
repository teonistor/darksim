using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffBar : MonoBehaviour {

    [SerializeField] private Transform indicator;
    [SerializeField] private float maxX;
    private float minX;

    void Start () {
        minX = indicator.localPosition.x;
    }
	

	void LateUpdate () {
        Vector3 p = indicator.localPosition;
        p.x = Mathf.Lerp(minX, maxX, Difficulty.CurrentDifficulty);
        indicator.localPosition = p;
	}
}
