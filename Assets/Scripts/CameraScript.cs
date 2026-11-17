using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float ZPosition = -20;
    [SerializeField] private GameObject player;

    [Header("Limite Camera")]
    [Tooltip("Cat de jos are voie sa coboare camera pe axa Y")]
    [SerializeField] private float limitaJos = -5f;

    [Tooltip("Acum limita de sus e fail-safe, camera se opreste singura la coliziune")]
    [SerializeField] private float limitaSus = 100f;

    // Variabile noi pentru a gestiona coliziunea cu tavanul
    private bool eBlocataSus = false;
    private float yMaxCurent;

    void Update()
    {
        if (player == null) return;

        float targetY = player.transform.position.y;

        // Daca suntem in coliziune cu tavanul, nu o lasam sa urce mai sus de punctul de impact
        if (eBlocataSus && targetY > yMaxCurent)
        {
            targetY = yMaxCurent;
        }

        // Aplicam limitele 
        float inaltimeLimitata = Mathf.Clamp(targetY, limitaJos, limitaSus);

        // Aplicam noua pozitie a camerei
        transform.position = new Vector3(player.transform.position.x, inaltimeLimitata, ZPosition);

    }

    public void setZPosition(float Position)
    {
        ZPosition = Position;
    }

    private void OnTriggerEnter(Collider other)

    {

        if (other.gameObject.CompareTag("LimitCamera"))

        {

            eBlocataSus = true;


            // Salvam inaltimea exacta la care s-a lovit camera de tavan

            yMaxCurent = transform.position.y;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LimitCamera"))
        {
            // Cand playerul cade/coboara, eliberam camera
            eBlocataSus = false;
            Debug.Log("Camera hit the ceiling");
        }
    }
}