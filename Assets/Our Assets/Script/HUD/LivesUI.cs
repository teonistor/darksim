using UnityEngine;

public class LivesUI : MonoBehaviour {
	void Update () {
        if (transform.childCount > Player.Lives && transform.childCount > 0) {
            Destroy(transform.GetChild(0).gameObject);
        }
	}
}
