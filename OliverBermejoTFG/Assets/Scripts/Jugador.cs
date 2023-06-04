using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private float horizontalMove;
    private float verticalMove;
    private Vector3 movementDirection;
    public CharacterController player;
    public Animator animatorJugador;
    public int moviendo = 1;
    public int ratasEliminadas;
    public int vida = 100;
    public GameObject[] chispas;

    public float playerSpeed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        animatorJugador = GetComponent<Animator>();
        ratasEliminadas = 0;
        for (int i = 0; i < chispas.Length; i++)
        {
            chispas[i].SetActive(false);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");   
        verticalMove = Input.GetAxis("Vertical"); 

        movementDirection = new Vector3 (horizontalMove, 0, verticalMove);
        movementDirection.Normalize(); //magnitud maxima para que el movimiento en diagonal no se sume y sea limitado.

        player.Move(movementDirection * playerSpeed * Time.deltaTime);

        if(movementDirection != Vector3.zero){
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            animatorJugador.SetBool("corriendo",true);
        }
        else
        {
            animatorJugador.SetBool("corriendo", false);
        }

        if (vida <= 0){
            Debug.Log("Has perdido.");
        }
    }

    public void recibirDmg(int dmg)
    {
        vida = vida - dmg;
    }

    public void actualizarVida(int num)
    {
        if (vida < 100)
        {
            vida += num;
        }
    }
}
