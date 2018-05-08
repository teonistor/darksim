using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    [SerializeField] private Transform indicator;
    [SerializeField] private Camera mainCam;

    private Renderer indicatorRenderer;
	
    void Start () {
        indicatorRenderer = indicator.GetComponent<Renderer>();
    }

	void FixedUpdate () {
        spin();
        updateIndicator();
	}

    private void spin () {
        Vector3 rot = transform.localEulerAngles;
        rot.y += 0.5f;
        transform.localEulerAngles = rot;
    }

    private void updateIndicator () {
        Vector3 scrp = mainCam.WorldToScreenPoint(transform.position);
        if (scrp.x > 0 && scrp.x < mainCam.pixelWidth && scrp.y > 0 && scrp.y < mainCam.pixelHeight) {
            print("Target in view");
        }
    }
}
 