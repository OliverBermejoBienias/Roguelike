using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LvlMgr : MonoBehaviour {
    public  bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CargaNivel(string pNombreNivel) {

        SceneManager.LoadScene(pNombreNivel);

    }

    public void SalirNivel(string pNombreNivel)
    {
        Application.Quit();
    }
    public void Resume()
    {
        GameManager.instance.resta = 0.01f;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
}
