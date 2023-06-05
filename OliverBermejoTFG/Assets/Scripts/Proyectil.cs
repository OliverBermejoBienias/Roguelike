using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private float velocidad = 20f;
    public GameObject particulasAgua;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destruir el proyectil al colisionar con cualquier otro objeto
        Instantiate(particulasAgua, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
