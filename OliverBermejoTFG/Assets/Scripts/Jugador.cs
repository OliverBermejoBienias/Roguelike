using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private float horizontalMove;
    private float verticalMove;
    private Vector3 movementDirection;
    public CharacterController player;
    public static Animator animatorJugador;
    public int moviendo = 1;
    public int ratasEliminadas;
    public int vidaMaxima;
    public int vida = 100;
    public GameObject[] chispas;
    public HealthBar barraVida;
    public bool muerto;
    public GameObject escudoUI;
    public GameObject escudo;
    public bool escudoListo;
    public bool inmune;

    public float playerSpeed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        animatorJugador = GetComponent<Animator>();
        ratasEliminadas = 0;
        vidaMaxima = 100;
        muerto = false;
        inmune = false;
        escudoListo = false;
        escudo.SetActive(false);
        escudoUI.SetActive(false);
        playerSpeed = 2.6f;
        barraVida.inicializeMaxHealth(vidaMaxima);
        for (int i = 0; i < chispas.Length; i++)
        {
            chispas[i].SetActive(false);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (!muerto)
        {
            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");

            movementDirection = new Vector3(horizontalMove, 0, verticalMove);
            movementDirection.Normalize(); //magnitud maxima para que el movimiento en diagonal no se sume y sea limitado.

            player.Move(movementDirection * playerSpeed * Time.deltaTime);

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                animatorJugador.SetBool("corriendo", true);
            }
            else
            {
                animatorJugador.SetBool("corriendo", false);
            }
        }
        if (vida <= 0)
        {
            muerteJugador();
        }
    }

    public void recibirDmg(int dmg)
    {
        if (!inmune)
        {
            vida = vida - dmg;
            barraVida.SetHealth(vida);
            if (escudoListo)
            {
                StartCoroutine(escudoActivo());
            }
        }
    }

    public void actualizarVida(int num)
    {
        if (vida < vidaMaxima)
        {
            vida += num;
            barraVida.SetHealth(vida);
        }
    }

    public void actualizarVidaMaxima(int num)
    {
        vidaMaxima += num;
        barraVida.SetMaxHealth(vidaMaxima);
    }

    public void actualizarVelocidadJugador(float num)
    {
        playerSpeed += num;
    }

    private void muerteJugador()
    {
        muerto = true;
        animatorJugador.SetBool("MuerteD", true);
        StartCoroutine(gameOver());
    }

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(1.5f);
        GameOver.fin = true;
    }

    IEnumerator escudoActivo()
    {
        escudo.SetActive(true);
        inmune = true;
        yield return new WaitForSeconds(3f);
        inmune = false;
        StartCoroutine(escudoEnfriamiento());
    }

    IEnumerator escudoEnfriamiento()
    {
        escudoListo = false;
        escudo.SetActive(false);
        yield return new WaitForSeconds(7f);
        escudoListo = true;
    }

    public void escudoConseguido()
    {
        escudoListo = true;
        escudoUI.SetActive(true);
    }
}
