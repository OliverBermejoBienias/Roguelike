using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public int numeroRandom;
    public GameObject jugador;
    public Jugador jugadorScript;
    public GameObject controladorGeneral;
    public Controlador controladorScript;

    private void Start()
    {
        numeroRandom = generarNumeroRandom();
        jugador = GameObject.FindGameObjectWithTag("player");
        controladorGeneral = GameObject.FindGameObjectWithTag("controlador");
        jugadorScript = jugador.GetComponent<Jugador>();
        controladorScript = controladorGeneral.GetComponent<Controlador>();
    }

    private int generarNumeroRandom()
    {
        // Generar un número aleatorio entre 0 y 1
        float probabilidad = Random.Range(0f, 1f);

        // Verificar la probabilidad para determinar el rango del número aleatorio
        if (probabilidad <= 0.95f)
        {
            // Generar un número aleatorio entre 1 y 5
            return Random.Range(1, 6);
        }
        else
        {
            return 5;
        }
    }

    public void powerUp(int numeroPowerUp)
    {
        switch (numeroPowerUp)
        {
            case 1:
                //VELOCIDAD MOVIMIENTO
                jugadorScript.actualizarVelocidadJugador(0.3f);
                break; 
            case 2:
                //DISPAROS CADA MENOS TIEMPO
                controladorScript.actualizarIntervaloDisparo(0.15f);
                break;
            case 3:
                //ENEMIGOS MAS LENTOS
                controladorScript.actualizarVelocidadEnemigos(-0.2f);
                break;
            case 4:
                //ROBO DE VIDA
                controladorScript.actualizarRoboVida(1);
                break;
            case 5:
                //DAÑO DE PROYECTIL
                controladorScript.actualizarDmgProyectil(1);
                break;
            case 6:
                //ESCUDO DE 2s DE INMUNIDAD

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            powerUp(numeroRandom);
            Destroy(gameObject);
        }
    }
}
