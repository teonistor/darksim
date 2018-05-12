using System;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider))]
public class Door : MonoBehaviour {
    [SerializeField] private GameObject indicatorPrefab;

    /// <summary>
    /// Open the door
    /// </summary>
    public static Action Open { get; private set; }

	void Start () {
        Open = () => {
            GetComponent<Animator>().enabled = true;
            GetComponent<Collider>().enabled = true;
            Instantiate(indicatorPrefab).GetComponent<TargetIndicator>()
                .Init(transform, Camera.main, WorldGenerator.Player.transform, () => true);
        };
	}
	
	void OnTriggerEnter(Collider other) {
        WorldGenerator.SuccessCanvas.SetActive(true);
    }
}
