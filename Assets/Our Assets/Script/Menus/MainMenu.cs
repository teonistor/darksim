using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private Text text;

    void Start () {
        text = GetComponentInChildren<Text>();

#if UNITY_EDITOR
        text.text = text.text.Replace("<color=aqua><Q></color> Quit", "<size=30>(Quit only available in standalone)</size>");
#elif UNITY_WEBGL
        text.text = text.text.Replace("<color=aqua><Q></color> Quit", " ");
#endif
    }


    void Update () {
        if (Input.GetKeyDown(KeyCode.E)) {
            text.text = "Loading...";
            Difficulty.BeginGame();
            Score.Reset();
            Enemy.Reset();
            SceneManager.LoadSceneAsync(1);

        } else if (Input.GetKeyDown(KeyCode.T)) {
            text.text = "Loading...";
            Difficulty.BeginGame(true);
            Score.Reset();
            Enemy.Reset();
            SceneManager.LoadSceneAsync(1);
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            Application.Quit();
        }
    }
}
