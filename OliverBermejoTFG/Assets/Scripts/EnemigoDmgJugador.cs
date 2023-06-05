using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoDmgJugador : MonoBehaviour
{
    public bool dmgListo = false;
    public GameObject controladorGeneral;
    public Controlador controlador;
    public GameObject jugador;
    public Jugador jugadorScript;

    // Start is called before the first frame update
    void Start()
    {
        dmgListo = true;
        controladorGeneral = GameObject.FindGameObjectWithTag("controlador");
        jugador = GameObject.FindGameObjectWithTag("player");
        jugadorScript = jugador.GetComponent<Jugador>();
        controlador = controladorGeneral.GetComponent<Controlador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dmgListo)
        {
            StartCoroutine(recargarEnfriamientoDmg());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") && dmgListo)
        {
            jugadorScript.recibirDmg(controlador.devolverDmgEnemigo());
            dmgListo = false;
        }
    }

    IEnumerator recargarEnfriamientoDmg()
    {
        yield return new WaitForSeconds(1.0f);
        dmgListo = true;
    }
}
