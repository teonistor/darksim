using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {

    public bool activate = false;
    public Transform cmr;
    
	void Start () {
		
	}
	
	void Update () {
		if (activate) {
            activate = false;
            StartCoroutine(collectAnimation());
        }
	}

    private Vector3 tp = new Vector3(-8f, 4f, -8f),
                    tr = new Vector3(900f, -900f, 900f),
                    ts = new Vector3(1f, 1f, -1f);

    private IEnumerator collectAnimation() {
        float frame = 1f / 30;
        WaitForSeconds wait = new WaitForSeconds(frame);
        transform.SetParent(cmr, true);

        Vector3 sp = transform.localPosition,
                sr = transform.localEulerAngles,
                ss = transform.localScale;

        for (float t=0f;t<=1f ; t += frame) {
            transform.localPosition = Vector3.Lerp(sp, tp, t);
            transform.localEulerAngles = Vector3.Lerp(sr, tr, t);
            transform.localScale = Vector3.Lerp(ss, ts, t);
            yield return wait;
        }
    }
}
