using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collect : MonoBehaviour {

    //public Transform cmr;
    private TargetIndicator indicator;
    
	void Start () {
        indicator = WorldGenerator.CreateTargetIndicator(this);
	}
	
	void Update () {

	}

    void OnTriggerEnter(Collider other) {
        print ("Collected something!");

        StartCoroutine(destroyAnimation());
        GetComponent<Collider>().enabled = false;
        Destroy(indicator);
    }

    private IEnumerator destroyAnimation () {
        float frame = 1f / 30;
        WaitForSeconds wait = new WaitForSeconds(frame);
        frame *= 2;

        Vector3 s = transform.localScale;

        for (float t = 0f; t <= 1f; t += frame) {
            transform.localScale = Vector3.Lerp(s, Vector3.zero, t);
            yield return wait;
        }

        Destroy(gameObject);
    }

    // Disused
    // For demo unity logo
    private Vector3 tp = new Vector3(-8f, 4f, -8f),
                    tr = new Vector3(22.8f, 96.8f, -26.6f),
                    ts = new Vector3(1f, 1.03f, -1f);

    // Disused
    private IEnumerator collectAnimation() {
        float frame = 1f / 30;
        WaitForSeconds wait = new WaitForSeconds(frame);
        //transform.SetParent(cmr, true);

        Vector3 sp = transform.localPosition,
                sr = transform.localEulerAngles,
                ss = transform.localScale;

        for (float t=0f;t<1.1f ; t += frame) {
            transform.localPosition = Vector3.Lerp(sp, tp, t);
            transform.localEulerAngles = Vector3.Lerp(sr, tr, t);
            transform.localScale = Vector3.Lerp(ss, ts, t);
            yield return wait;
        }
    }
}
