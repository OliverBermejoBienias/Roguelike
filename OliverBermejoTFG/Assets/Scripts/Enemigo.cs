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
    //public GameObject particulaEnemigoMuerte;

    // Start is called before the first frame update
    void Start()
    {
        controladorGeneral = GameObject.FindGameObjectWithTag("controlador");
        jugador = GameObject.FindGameObjectWithTag("player");
        jugadorScript = jugador.GetComponent<Jugador>();
        controlador = controladorGeneral.GetComponent<Controlador>();
        vidaRata = controlador.devolverVidaEnemigo();
        velocidadRata = controlador.devolverVelocidadEnemigos();
        dmgListo = true;
    }

    // Update is called once per frame
    void Update()
    {
        comprobarVida();

        if (!dmgListo)
        {
            StartCoroutine(recargarEnfriamientoDmg());
        }
    }

    public void restarVida(int daño)
    {
        vidaRata -= daño;
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
        if (other.CompareTag("player") && dmgListo)
        {
            Jugador jugadorScript = other.GetComponent<Jugador>();
            jugadorScript.recibirDmg(controlador.devolverDmgEnemigo());
            dmgListo = false;
        }
    }

    IEnumerator recargarEnfriamientoDmg()
    {
        yield return new WaitForSeconds(1.0f);
        dmgListo = true;
    }

    void enemigoMuerte()
    {
        //Instantiate(particulaEnemigoMuerte, transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(0.5f);
        controlador.enemigoEliminado(1);
        Destroy(gameObject);
    }
}
