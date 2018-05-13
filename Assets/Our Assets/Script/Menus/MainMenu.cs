using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.E)) {
            GetComponentInChildren<Text>().text = "Loading...";
            Difficulty.BeginGame();
            Score.Reset();
            SceneManager.LoadSceneAsync(1);
        }
    }
}
