using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private GameObject endSound;
    private static bool first = true;

    private void Awake() {
        if (first) {
            first = false;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

    }

    public void playEndSound() {
        endSound.SetActive(true);
    }
}
