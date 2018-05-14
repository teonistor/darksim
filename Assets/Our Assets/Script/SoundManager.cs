using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClip bite, end, collection;
    [SerializeField] private AudioClip[] backgroundClips;
    [SerializeField] private AudioSource occasionalSound;
    private AudioSource backgroundSound;

    private static SoundManager instance = null;

    private void Awake() {
        if (instance == null) {
            backgroundSound = GetComponent<AudioSource>();
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void LateUpdate () {
        if (!backgroundSound.isPlaying) {
            backgroundSound.PlayOneShot(backgroundClips[Random.Range(0, backgroundClips.Length)]);
        }
    }

    public static void PlayEndSound () {
        if (!Difficulty.IsTutorial)
            instance.occasionalSound.PlayOneShot(instance.end);
    }

    public static void PlayBiteSound () {
        instance.occasionalSound.PlayOneShot(instance.bite);
    }

    public static void PlayCollectionSound () {
        instance.occasionalSound.PlayOneShot(instance.collection);
    }
}
