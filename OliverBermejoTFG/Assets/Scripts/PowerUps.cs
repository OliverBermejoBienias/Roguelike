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
    public MenuPowerUp controladorPowerUp;
    public bool escudoAdquirido;
    public string mensaje = "";

    private void Start()
    {
        numeroRandom = generarNumeroRandom();
        jugador = GameObject.FindGameObjectWithTag("player");
        controladorGeneral = GameObject.FindGameObjectWithTag("controlador");
        jugadorScript = jugador.GetComponent<Jugador>();
        controladorScript = controladorGeneral.GetComponent<Controlador>();
        controladorPowerUp = controladorScript.GetComponent<MenuPowerUp>();
        escudoAdquirido = controladorScript.devolverEscudoAdquirido();
    }
    private void Update()
    {
        escudoAdquirido = controladorScript.devolverEscudoAdquirido();
    }

    private int generarNumeroRandom()
    {
        if (!escudoAdquirido)
        {
            // Generar un n�mero aleatorio entre 0 y 1
            float probabilidad = Random.Range(0f, 1f);

            // Verificar la probabilidad para determinar el rango del n�mero aleatorio
            if (probabilidad <= 0.95f)
            {
                // Generar un n�mero aleatorio entre 1 y 5
                return Random.Range(1, 6);
            }
            else
            {
                return 6;
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
                mensaje = "�Enhorabuena, has conseguido una mejora! Tu vida m�xima ha aumentado 10 puntos.";
                controladorPowerUp.AbrirMenu(mensaje);
                break; 
            case 2:
                //DISPAROS CADA MENOS TIEMPO
                controladorScript.actualizarIntervaloDisparo(0.2f);
                mensaje = "�Enhorabuena, has conseguido una mejora! El intervalo entre disparos ha disminuido en 0,2.";
                controladorPowerUp.AbrirMenu(mensaje);
                break;
            case 3:
                //MENOR DA�O DE ENEMIGOS
                controladorScript.actualizarDmgEnemigo(3);
                mensaje = "�Enhorabuena, has conseguido una mejora! Los enemigos ahora te har�n 3 puntos menos de da�o.";
                controladorPowerUp.AbrirMenu(mensaje);
                break;
            case 4:
                //ROBO DE VIDA
                controladorScript.actualizarRoboVida(1);
                mensaje = "�Enhorabuena, has conseguido una mejora! Te curas 1 punto de vida adicional cada vez que da�as a un enemigo.";
                controladorPowerUp.AbrirMenu(mensaje);
                break;
            case 5:
                //DA�O DE PROYECTIL
                controladorScript.actualizarDmgProyectil(1);
                mensaje = "�Enhorabuena, has conseguido una mejora! Tus proyectiles ahora inflingen 1 punto m�s de da�o.";
                controladorPowerUp.AbrirMenu(mensaje);
                break;
            case 6:
                //ESCUDO DE 2s DE INMUNIDAD
                controladorScript.actualizarEscudoAdquirido();
                jugadorScript.escudoConseguido();
                mensaje = "�Enhorabuena, has conseguido una mejora LEGENDARIA! Un escudo te volver� inmune durante 3 segundos cuando recibas da�o," +
                    " pero cuidado, porque tardar�s 7 segundos en volver a tenerlo disponible.";
                controladorPowerUp.AbrirMenu(mensaje);
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
