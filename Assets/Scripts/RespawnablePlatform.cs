using System.Collections;
using UnityEngine;

public class RespawnablePlatform : MonoBehaviour
{
    [Header("Setari Timp (Secunde)")]
    [SerializeField] private float timpPanaCade = 1.5f;
    [SerializeField] private float timpPanaReapare = 3f;

    private Rigidbody rb;
    private Vector3 pozitieInitiala;
    private Quaternion rotatieInitiala;
    private bool aInceputCaderea = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        pozitieInitiala = transform.position;
        rotatieInitiala = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Daca jucatorul atinge platforma si ea nu e deja in proces de cadere
        if (collision.gameObject.CompareTag("Player") && !aInceputCaderea)
        {
            StartCoroutine(SecventaCadereSiReaparitie());
        }
    }

    private IEnumerator SecventaCadereSiReaparitie()
    {
        aInceputCaderea = true;

        yield return new WaitForSeconds(timpPanaCade);

        rb.isKinematic = false;

        yield return new WaitForSeconds(timpPanaReapare);

        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = pozitieInitiala;
        transform.rotation = rotatieInitiala;

        aInceputCaderea = false;
    }
}