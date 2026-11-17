using UnityEngine;

public class RespawnOnFall : MonoBehaviour
{
    private Vector3 pozitieInitiala;
    private Quaternion rotatieInitiala;
    private Rigidbody rb;

    void Start()
    {
        // Salveaza pozitia si rotatia exacta a cufarului de la inceputul nivelului
        pozitieInitiala = transform.position;
        rotatieInitiala = transform.rotation;

        // Luam referinta catre Rigidbody pentru a-i controla fizica
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificam daca obiectul care a lovit cufarul are scriptul FallDeath (Abisul)
        if (other.GetComponent<FallDeath>() != null)
        {
            RespawnObiect();
        }
    }

    private void RespawnObiect()
    {
        // Teleportam cufarul inapoi la coordonatele lui initiale de pe platforma
        transform.position = pozitieInitiala;
        transform.rotation = rotatieInitiala;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log(gameObject.name + " a cazut in DeathAbyss si a fost trimis inapoi pe platforma!");
    }
}