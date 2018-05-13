using UnityEngine.SceneManagement;
using UnityEngine;

public class Fail : InterruptBase {
    private string template;
    private int p, pT;

    void Start () {
        template = text.text;
        p = 0;
        Score.ComputeFail(out pT);
    }

    void Update () {
        if (p > pT) {
            p += (int)(Time.unscaledDeltaTime * pT);
            if (p < pT) p = pT;
        }

        text.text = string.Format(template,
            p < 0 ? show : hide, p,
            p == pT ? show : hide, p == pT ? showControls : hide
        );

        if (p==pT) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadSceneAsync(1);
                text.text = "Loading...";
                Difficulty.RetryLevel();
                enabled = false;
            } else if (Input.GetKeyDown(KeyCode.Q))
                SceneManager.LoadSceneAsync(0);
        }
    }
}
