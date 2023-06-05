using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public GameObject controladorGeneral;
    public GameObject jugador;
    public Jugador jugadorScript;
    public Controlador controladorScript;
    public TextMeshProUGUI vidaMaxima;
    public TextMeshProUGUI atkSpeed;
    public TextMeshProUGUI enemyDmg;
    public TextMeshProUGUI lifeSteal;
    public TextMeshProUGUI atkDmg;

    // Start is called before the first frame update
    void Start()
    {
        controladorScript = controladorGeneral.GetComponent<Controlador>();
        jugador = GameObject.FindGameObjectWithTag("player");
        jugadorScript = jugador.GetComponent<Jugador>();
    }

    // Update is called once per frame
    void Update()
    {
        vidaMaxima.text = jugadorScript.vidaMaxima.ToString();
        atkSpeed.text = controladorScript.devolverIntervaloDisparo().ToString();
        enemyDmg.text = controladorScript.devolverDmgEnemigo().ToString();
        lifeSteal.text = controladorScript.devolverRoboVida().ToString();
        atkDmg.text = controladorScript.devolverDmgProyectil().ToString();
    }
}
