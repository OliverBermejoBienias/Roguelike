using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public bool enPausa;

    // Start is called before the first frame update
    void Start()
    {
        menuPausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (enPausa)
            {
                continuarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        enPausa = true;
    }

    public void continuarJuego()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        enPausa = false;
    }

    public void salirJuego()
    {
        Application.Quit();
    }
}
