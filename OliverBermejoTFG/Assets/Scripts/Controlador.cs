using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    public int enemigosEliminados;
    public int totalEnemigos;
    public int dmgProyectil;
    public int dmgEnemigo;
    public int dmgEnemigoRestado;
    public int salasCompletadas;
    public static bool juegoCompl;
    public float tiempoSpawnEnemigos;
    public int vidaEnemigos;
    public float velocidadEnemigos;
    public float intervaloDisparo;
    public int roboVida;
    public bool escudoAdquirido;

    // Start is called before the first frame update
    void Start()
    {
        valoresIniciales();
    }

    private void Update()
    {
        switch (salasCompletadas)
        {
            case 0:
                totalEnemigos = 10;
                tiempoSpawnEnemigos = 0.6f;
                vidaEnemigos = 20;
                dmgEnemigo = 15;
                break;
            case 1:
                totalEnemigos = 12;
                tiempoSpawnEnemigos = 0.5f;
                vidaEnemigos = 22;
                dmgEnemigo = 15 - dmgEnemigoRestado;
                break;
            case 2:
                totalEnemigos = 14;
                tiempoSpawnEnemigos = 0.4f;
                vidaEnemigos = 24;
                dmgEnemigo = 17 - dmgEnemigoRestado;
                break;
            case 3:
                totalEnemigos = 16;
                tiempoSpawnEnemigos = 0.3f;
                vidaEnemigos = 26;
                dmgEnemigo = 17 - dmgEnemigoRestado;
                break;
            case 4:
                totalEnemigos = 20;
                tiempoSpawnEnemigos = 0.2f;
                vidaEnemigos = 30;
                dmgEnemigo = 20 - dmgEnemigoRestado;
                break;
            case 5:
                totalEnemigos = 24;
                tiempoSpawnEnemigos = 0.1f;
                vidaEnemigos = 34;
                dmgEnemigo = 20 - dmgEnemigoRestado;
                break;
            case 6:
                totalEnemigos = 28;
                tiempoSpawnEnemigos = 0.0f;
                vidaEnemigos = 38;
                dmgEnemigo = 23 - dmgEnemigoRestado;
                break;
            case 7:
                totalEnemigos = 33;
                tiempoSpawnEnemigos = 0.0f;
                vidaEnemigos = 40;
                dmgEnemigo = 25 - dmgEnemigoRestado;
                break;
            case 8:
                StartCoroutine(juegoCompletado());
                break;
        }
    }

    public void valoresIniciales()
    {
        intervaloDisparo = 0.9f;
        dmgProyectil = 10;
        roboVida = 0;
        totalEnemigos = 10;
        tiempoSpawnEnemigos = 2.0f;
        vidaEnemigos = 20;
        velocidadEnemigos = 2.25f;
        dmgEnemigo = 15;
        dmgEnemigoRestado = 0;
        enemigosEliminados = 0;
        salasCompletadas = 0;
        escudoAdquirido = false;
        juegoCompl = false;
    }

    public void enemigoEliminado()
    {
        enemigosEliminados += 1;
    }

    public void resertearEnemigosEliminados()
    {
        enemigosEliminados = 0;
    }

    public void actualizarVelocidadEnemigos(float num)
    {
        velocidadEnemigos += num;
    }

    public int devolverEnemigosEliminados()
    {
        return enemigosEliminados;
    }

    public int devolverTotalEnemigos()
    {
        return totalEnemigos;
    }

    public int devolverDmgProyectil()
    {
        return dmgProyectil;
    }

    public void actualizarDmgProyectil(int num)
    {
        dmgProyectil += num;
    }

    public void salaCompletada(int numero)
    {
        salasCompletadas += numero;
    }

    public float devolverTiempoSpawnEnemigos()
    {
        return tiempoSpawnEnemigos;
    }

    public void actualizarVidaEnemigos(int vida)
    {
        vidaEnemigos += vida;
    }

    public int devolverVidaEnemigo()
    {
        return vidaEnemigos;
    }

    public void actualizarDmgEnemigo(int dmg)
    {
        dmgEnemigoRestado += dmg;
    }

    public int devolverDmgEnemigo()
    {
        return dmgEnemigo;
    }

    public void actualizarIntervaloDisparo(float numero)
    {
        intervaloDisparo -= numero;
    }

    public float devolverIntervaloDisparo()
    {
        return intervaloDisparo;
    }

    public float devolverVelocidadEnemigos()
    {
        return velocidadEnemigos;
    }

    public void actualizarRoboVida(int num)
    {
        roboVida += num;
    }

    public int devolverRoboVida()
    {
        return roboVida;
    }

    public void actualizarEscudoAdquirido()
    {
        escudoAdquirido = true;
    }

    public bool devolverEscudoAdquirido()
    {
        return escudoAdquirido;
    }

    public int devolverSalasCompletadas()
    {
        return salasCompletadas;
    }

    IEnumerator juegoCompletado()
    {
        yield return new WaitForSeconds(5.0f);
        juegoCompl = true;
    }

    public bool devolverJuegoCompletado()
    {
        return juegoCompl;
    }
}
