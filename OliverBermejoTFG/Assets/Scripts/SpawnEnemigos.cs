using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject enemigo;
    public GameObject luz;
    private Light pointLight;
    private float xPos;
    private float zPos;
    private int contadorEnemigos = 0;
    public int totalEnemigos;
    public int enemigosEliminados;
    public bool salaIniciada = false;
    public bool salaCompletada = false;
    public GameObject[] puertas;
    public Animator animatorPuertas;
    private BoxCollider colliderPuertas;
    public GameObject particulaSalaCompletada;
    public GameObject particulaSpawnEnemigo;
    public Transform powerUpSpawn;
    public GameObject powerUp;

    public GameObject controladorGeneral;
    private Controlador controlador;


    public void Start()
    {
        enemigosEliminados = 0;
        pointLight = luz.GetComponent<Light>();
        pointLight.intensity = 0.0f;
        controladorGeneral = GameObject.FindGameObjectWithTag("controlador");
        controlador = controladorGeneral.GetComponent<Controlador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (salaCompletada && salaIniciada)
        {
            for (int i = 0; i < puertas.Length; i++)
            {
                animatorPuertas = puertas[i].GetComponent<Animator>();
                colliderPuertas = puertas[i].GetComponent<BoxCollider>();

                colliderPuertas.enabled = false;
                animatorPuertas.SetBool("abrir", true);
                controlador.resertearEnemigosEliminados();
                Instantiate(particulaSalaCompletada, transform.position, Quaternion.identity);
            }
            Instantiate(powerUp, powerUpSpawn.position, powerUpSpawn.rotation);
            controlador.salaCompletada(1);
            Destroy(this.gameObject);
        }

        enemigosEliminados = controlador.devolverEnemigosEliminados();
        if (enemigosEliminados >= totalEnemigos)
        {
            salaCompletada = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player" && !salaIniciada)
        {
            salaCompletada = false;
            this.GetComponent<SphereCollider>().enabled = false;
            totalEnemigos = controlador.devolverTotalEnemigos();
            StartCoroutine(IniciarSala());
        }
    }

    IEnumerator IniciarSala()
    {
        salaIniciada = true;

        for (int i = 0; i < puertas.Length; i++)
        {
            animatorPuertas = puertas[i].GetComponent<Animator>();
            colliderPuertas = puertas[i].GetComponent<BoxCollider>();

            colliderPuertas.enabled = true;
            animatorPuertas.SetBool("abrir", false);
            yield return new WaitForSeconds(0.5f);
        }
        pointLight.intensity = 2.5f;
        yield return new WaitForSeconds(1.5f);
        pointLight.intensity = 0.0f;
        yield return new WaitForSeconds(3.0f);

        while (contadorEnemigos < totalEnemigos)
        {
            xPos = UnityEngine.Random.Range(this.transform.position.x - 3.7f, this.transform.position.x + 3.7f);
            zPos = UnityEngine.Random.Range(this.transform.position.z - 3.7f, this.transform.position.z + 3.7f);
            Instantiate(particulaSpawnEnemigo, new Vector3(xPos, (float)0.23, zPos), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            Instantiate(enemigo, new Vector3(xPos, (float)0.03, zPos), Quaternion.identity);
            yield return new WaitForSeconds(controlador.devolverTiempoSpawnEnemigos());
            contadorEnemigos += 1;
        }
    }
}
