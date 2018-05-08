using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
            SceneManager.LoadSceneAsync(1);
    }
}
