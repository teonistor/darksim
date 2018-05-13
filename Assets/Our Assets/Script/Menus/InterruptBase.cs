using UnityEngine.UI;
using UnityEngine;

public class InterruptBase : MonoBehaviour {
    protected static readonly string
        hide = "#0000",
        show = "white",
        showControls = "aqua";

    protected Text text;

    void Awake () {
        text = GetComponentInChildren<Text>();
    }

    void OnEnable () {
        Time.timeScale = 0f;
    }

    void OnDisable () {
        Time.timeScale = 1f;
    }
}
