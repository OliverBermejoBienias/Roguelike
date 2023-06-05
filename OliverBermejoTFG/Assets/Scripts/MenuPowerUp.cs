using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuPowerUp : MonoBehaviour
{
    public GameObject menuPowerUp;
    public MenuFinalJuego menuFinalJuego;
    public TextMeshProUGUI mensaje;
    public Controlador controlador;
    public bool recogido;

    // Start is called before the first frame update
    void Start()
    {
        menuPowerUp.SetActive(false);
        controlador = gameObject.GetComponent<Controlador>();
    }

    public void AbrirMenu(string mens)
    {
        menuPowerUp.SetActive(true);
        Time.timeScale = 0f;
        mensaje.text = mens;
    }

    public void Aceptar()
    {
        menuPowerUp.SetActive(false);
        Time.timeScale = 1f;
    }
}
