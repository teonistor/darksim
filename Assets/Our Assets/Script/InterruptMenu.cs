using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class InterruptMenu : MonoBehaviour {
    enum Menu { Pause, Success, Fail}
    [SerializeField] private Menu menu;
    
    void OnEnable () {
        Time.timeScale = 0f;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && menu == Menu.Pause)
            gameObject.SetActive(false);
        else if (Input.GetKeyDown(KeyCode.R) && menu != Menu.Success) {
            GetComponentInChildren<Text>().text = "Loading...";
            SceneManager.LoadSceneAsync(1);
        } else if (Input.GetKeyDown(KeyCode.E) && menu == Menu.Success) {
            GetComponentInChildren<Text>().text = "Loading...";
            // TODO level progression
            SceneManager.LoadSceneAsync(1);
        } else if (Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadSceneAsync(0);
    }

    void OnDisable () {
        Time.timeScale = 1f;
    }


}
