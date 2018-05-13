using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Fail : InterruptBase {
    private string template;
    private bool controlsActive;

    void Start () {
        template = text.text;
        controlsActive = false;
    }

    void Update () {



        if (controlsActive) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadSceneAsync(1);
                text.text = "Loading...";
                Difficulty.RetryLevel();
            } else if (Input.GetKeyDown(KeyCode.Q))
                SceneManager.LoadSceneAsync(0);
        }
    }
}
