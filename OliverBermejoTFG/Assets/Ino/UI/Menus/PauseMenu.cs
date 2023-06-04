using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private void Start(){
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }
    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
	}

    void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    void Pause()
    {
        GameManager.instance.resta = 0;
        Debug.Log("pausa");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
}
