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
    public bool escudoAdquirido;

    private void Start()
    {
        numeroRandom = generarNumeroRandom();
        jugador = GameObject.FindGameObjectWithTag("player");
        controladorGeneral = GameObject.FindGameObjectWithTag("controlador");
        jugadorScript = jugador.GetComponent<Jugador>();
        controladorScript = controladorGeneral.GetComponent<Controlador>();
        escudoAdquirido = controladorScript.devolverEscudoAdquirido();
    }

    private int generarNumeroRandom()
    {
        if (!escudoAdquirido)
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
        else
        {
            return Random.Range(1, 6);
        }
    }

    public void powerUp(int numeroPowerUp)
    {
        switch (numeroPowerUp)
        {
            case 1:
                //AUMENTAR VIDA MAXIMA DEL JUGADOR
                jugadorScript.actualizarVidaMaxima(10);
                break; 
            case 2:
                //DISPAROS CADA MENOS TIEMPO
                controladorScript.actualizarIntervaloDisparo(0.2f);
                break;
            case 3:
                //MENOR DAÑO DE ENEMIGOS
                controladorScript.actualizarDmgEnemigo(3);
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
                controladorScript.actualizarEscudoAdquirido();
                jugadorScript.escudoConseguido();
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
