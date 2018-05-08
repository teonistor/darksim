using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuPause : MonoBehaviour {
    
	void Start () {
        Time.timeScale = 0f;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.UnloadSceneAsync(2);
        else if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadSceneAsync(1);
        else if (Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadSceneAsync(0);
    }

    void OnDestroy () {
        Time.timeScale = 1f;
    }
}
