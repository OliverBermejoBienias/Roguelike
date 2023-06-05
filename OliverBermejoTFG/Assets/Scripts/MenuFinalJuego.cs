using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFinalJuego : MonoBehaviour
{
    public GameObject menuFinalizarJuego;
    public Controlador controlador;

    // Start is called before the first frame update
    void Start()
    {
        menuFinalizarJuego.SetActive(false);
        controlador = gameObject.GetComponent<Controlador>();
    }

    private void Update()
    {
        if (controlador.devolverJuegoCompletado())
        {
            finalizarJuego();
        }
    }

    public void finalizarJuego() 
    {
        menuFinalizarJuego.SetActive(true);
        Time.timeScale = 0f;
    }

    public void aceptar()
    {
        Application.Quit();
    }
}
