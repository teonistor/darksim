using UnityEngine;

public class TileTexture : MonoBehaviour {
	void Start () {
        GetComponent<Renderer>().material.mainTextureScale = transform.localScale;
        // GetComponent<Renderer>().material.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.z);
	}
}
