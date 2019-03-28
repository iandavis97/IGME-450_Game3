//handles changing scenes via button press
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour {

	// changes the scene
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ends the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
