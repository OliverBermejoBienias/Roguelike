using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vidaRata;
    public float velocidadRata;
    public GameObject controladorGeneral;
    public Controlador controlador;
    public GameObject jugador;
    public Jugador jugadorScript;
    public bool dmgListo = false;
    public HealthBar barraVida;

    // Start is called before the first frame update
    void Start()
    {
        controladorGeneral = GameObject.FindGameObjectWithTag("controlador");
        jugador = GameObject.FindGameObjectWithTag("player");
        jugadorScript = jugador.GetComponent<Jugador>();
        controlador = controladorGeneral.GetComponent<Controlador>();
        vidaRata = controlador.devolverVidaEnemigo();
        barraVida.inicializeMaxHealth(controlador.devolverVidaEnemigo());
        velocidadRata = controlador.devolverVelocidadEnemigos();
        dmgListo = true;
    }

    // Update is called once per frame
    void Update()
    {
        comprobarVida();
    }

    public void restarVida(int daño)
    {
        vidaRata -= daño;
        barraVida.SetHealth(vidaRata);
    }

    void comprobarVida()
    {
        if (vidaRata <= 0)
        {
            enemigoMuerte();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("proyectil"))
        {
            restarVida(controlador.devolverDmgProyectil());
            jugadorScript.actualizarVida(controlador.devolverRoboVida());
        }
    }

    void enemigoMuerte()
    {
        controlador.enemigoEliminado();
        Destroy(gameObject);
    }
}
