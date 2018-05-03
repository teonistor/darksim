using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject wall;
	// Use this for initialization
	void Start () {
        Instantiate(wall, new Vector3(-3,0.5f,-3), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
