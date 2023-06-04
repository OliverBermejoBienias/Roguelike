using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject menuGameOver;
    public Controlador controlador;
    public static bool fin;

    // Start is called before the first frame update
    void Start()
    {
        menuGameOver.SetActive(false);
        controlador = gameObject.GetComponent<Controlador>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (fin)
        {
            gameOver();
        }
    }

    public void gameOver()
    {
        menuGameOver.SetActive(true);
        Time.timeScale = 0f;
        fin = true;
    }

    public void salirJuego()
    {
        Application.Quit();
    }
}
