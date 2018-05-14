using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioSource occasionalSound;
    [SerializeField] private AudioClip bite, end, collection;

    private static SoundManager instance = null;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public static void PlayEndSound () {
        instance.occasionalSound.PlayOneShot(instance.end);
    }

    public static void PlayBiteSound () {
        instance.occasionalSound.PlayOneShot(instance.bite);
    }

    public static void PlayCollectionSound () {
        instance.occasionalSound.PlayOneShot(instance.collection);
    }
}
