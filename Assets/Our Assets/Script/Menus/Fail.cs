using UnityEngine.SceneManagement;
using UnityEngine;

public class Fail : InterruptBase {
    private string template;
    private bool controlsActive;
    private int p, pT;

    void Start () {
        template = text.text;
        p = 0;
        Score.ComputeFail(out pT);
    }

    void Update () {
        if (!controlsActive) {
            if (p > pT) {
                p += (int)(Time.unscaledDeltaTime * pT);
                if (p < pT) p = pT;
            }
            controlsActive = p == pT;

            text.text = string.Format(template,
                p < 0 ? show : hide, p,
                p == pT ? show : hide, p == pT ? showControls : hide
            );
        }

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
