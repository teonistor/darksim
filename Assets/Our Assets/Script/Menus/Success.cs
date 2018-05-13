using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Success : InterruptBase {
    private string template;
    //private bool controlsActive;

    private int c, cT, t, tT, h, hT, s, sT;

    void Start () {
        template = text.text;
        c = t = h = s = 0;

        Score.ComputeSuccess(out cT, out tT, out hT, out sT);
        if (Difficulty.IsLastTutorial)
            template = template
                .Replace("Level", "Tutorial")
                .Replace("<color={9}><E></color> Continue     ", "");
    }

    void Update () {
        if (c < cT) {
            c += (int)(Time.unscaledDeltaTime * cT);
            if (c > cT) c = cT;
        } else if (t < tT) {
            t += (int)(Time.unscaledDeltaTime * tT);
            if (t > tT) t = tT;
        } else if (h < hT) {
            h += (int)(Time.unscaledDeltaTime * hT);
            if (h > hT) h = hT;
        } else if (s < sT) {
            s += (int)(Time.unscaledDeltaTime * sT);
            if (s > sT) s = sT;
        }

        bool controlsActive = c == cT && t == tT && h == hT && s == sT;
        text.text = string.Format(template,
            c > 0 ? show : hide, c,
            t > 0 ? show : hide, t,
            h > 0 ? show : hide, h,
            s > 0 ? show : hide, s,
            controlsActive ? show : hide, controlsActive ? showControls : hide
        );

        if (controlsActive) {
            if (Input.GetKeyDown(KeyCode.E)) {
                SceneManager.LoadSceneAsync(1);
                text.text = "Loading...";
                Difficulty.NextLevel();
                enabled = false;
            } else if (Input.GetKeyDown(KeyCode.Q))
                SceneManager.LoadSceneAsync(0);
        }
    }
}
