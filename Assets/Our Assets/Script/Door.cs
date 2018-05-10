using System;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider))]
public class Door : MonoBehaviour {
   
    /// <summary>
    /// Open the door
    /// </summary>
    public static Action Open { get; private set; }

	void Start () {
        Open = () => {
            GetComponent<Animator>().enabled = true;
            GetComponent<Collider>().enabled = true;
            WorldGenerator.CreateTargetIndicator(this);
        };
	}
	
	void OnTriggerEnter(Collider other) {
        WorldGenerator.SuccessCanvas.SetActive(true);
    }
}
