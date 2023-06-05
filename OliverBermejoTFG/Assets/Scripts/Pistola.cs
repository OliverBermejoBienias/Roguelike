using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    public float rangoAtaque = 10f; // Rango de ataque de la torreta
    public float velocidadRotacion = 10f; // Velocidad de rotación de la torreta
    public float intervaloDisparo; // Intervalo de tiempo entre disparos
    private float ultimoDisparo; // Último tiempo en que se disparó
    public Transform firePoint; // Punto de disparo de la torreta
    public GameObject projectilePrefab; // Prefab del proyectil a disparar
    public GameObject controladorGeneral;
    public Controlador controladorScript;

    public Transform objetivo; // Referencia al enemigo más cercano

    private void Start()
    {
        controladorScript = controladorGeneral.GetComponent<Controlador>();
        InvokeRepeating("encontrarEnemigoMasCercano", 0f, 0.5f);
        InvokeRepeating("comprobarIntervaloDisparo", 0f, 0.5f);
    }
    private void Update()
    {
        if (objetivo == null)
        {
            return;
        }

        // Si hay un enemigo dentro del rango, rotar hacia él y disparar
        if (objetivo != null && enemigoEnRango())
        {
            RotateTurret();
            if (Time.time - ultimoDisparo >= intervaloDisparo)
            {
                Fire();
                ultimoDisparo = Time.time;
            }
        }
    }

    // Buscar el enemigo más cercano dentro del rango de ataque
    private void encontrarEnemigoMasCercano()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("rata");
        float distanciaMasCorta = Mathf.Infinity;
        GameObject enemigoMasCercano = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distanciaEnemigo = Vector3.Distance(transform.position, enemigo.transform.position);

            if (distanciaEnemigo < distanciaMasCorta)
            {
                distanciaMasCorta = distanciaEnemigo;
                enemigoMasCercano = enemigo;
            }
        }

        if (enemigoMasCercano != null && distanciaMasCorta <= rangoAtaque)
        {
            objetivo = enemigoMasCercano.transform;
        }
        else
        {
            objetivo = null;
        }
    }

    // Verificar si hay un enemigo dentro del rango de ataque
    private bool enemigoEnRango()
    {
        float distanciaObjetivo = Vector3.Distance(transform.position, objetivo.position);

        if (distanciaObjetivo <= rangoAtaque)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Rota la torreta hacia el enemigo más cercano
    private void RotateTurret()
    {
        Vector3 direction = objetivo.position - transform.position;

        // Calcula la rotación en el eje "Y" para apuntar al enemigo
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float yRotation = Quaternion.Lerp(transform.rotation, targetRotation, velocidadRotacion * Time.deltaTime).eulerAngles.y;

        // Calcula la rotación en el eje "X" para apuntar al enemigo
        Vector3 relativeDirection = transform.InverseTransformDirection(direction);
        float xRotation = Mathf.Atan2(relativeDirection.y, relativeDirection.z) * Mathf.Rad2Deg;

        // Aplica la rotación en los ejes "Y" y "X"
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        firePoint.localRotation = Quaternion.Euler(-xRotation, 0f, 0f);
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Destroy(projectile, 10.0f);
    }

    public void comprobarIntervaloDisparo()
    {
        intervaloDisparo = controladorScript.devolverIntervaloDisparo();
    }
}
