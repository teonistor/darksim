using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Pause : InterruptBase {
    void Update () {
        if (Input.GetButtonDown("Pause"))
            gameObject.SetActive(false);
        else if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadSceneAsync(1);
            GetComponentInChildren<Text>().text = "Loading...";
            Difficulty.RetryLevel();
        } else if (Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadSceneAsync(0);
    }
}
