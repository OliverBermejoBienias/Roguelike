using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemigoNavMesh : MonoBehaviour
{
    public GameObject jugador;
    public GameObject controladorGeneral;
    public Controlador controladorScript;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("player");
        controladorGeneral = GameObject.FindGameObjectWithTag("controlador");
        controladorScript = controladorGeneral.GetComponent<Controlador>();
        agent.speed = controladorScript.devolverVelocidadEnemigos();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 pos = jugador.transform.position.x, transform.position.y, jugador.transform.position.z;
        agent.SetDestination(jugador.transform.position);
        //jugador.transform.position.x, transform.position.y, jugador.transform.position.z
    }
}
