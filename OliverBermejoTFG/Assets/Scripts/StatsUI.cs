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
    public TextMeshProUGUI movSpeed;
    public TextMeshProUGUI atkSpeed;
    public TextMeshProUGUI enemySpeed;
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
        movSpeed.text = jugadorScript.playerSpeed.ToString();
        atkSpeed.text = controladorScript.devolverIntervaloDisparo().ToString();
        enemySpeed.text = controladorScript.devolverVelocidadEnemigos().ToString();
        lifeSteal.text = controladorScript.devolverRoboVida().ToString();
        atkDmg.text = controladorScript.devolverDmgProyectil().ToString();
    }
}
