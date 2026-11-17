using UnityEngine;

public class HubMovement : MonoBehaviour
{
    [Header("Setari Miscare Hub")]
    [SerializeField] private float vitezaDeplasare = 5f;
    [SerializeField] private float vitezaRotatie = 720f;

    [Header("Setari Saritura")]
    [SerializeField] private float fortaSaritura = 7f;
    [Tooltip("Lungimea razei care verifica solul. Ajusteaza daca personajul nu sare sau sare in aer.")]
    [SerializeField] private float distantaVerificareSol = 0.3f;

    private Rigidbody rb;
    private Vector3 directieMiscare;
    private bool vreaSaSara = false;
    private bool estePeSol = false;

    private Animator animator;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;

        // Ne asiguram ca personajul nu se rastoarna cand urca pe dealuri
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        estePeSol = false;

        RaycastHit[] lovituri = Physics.RaycastAll(transform.position + Vector3.up * 0.5f, Vector3.down, distantaVerificareSol);

        foreach (RaycastHit lovitura in lovituri)
        {

            if (!lovitura.collider.CompareTag("Player"))
            {
                estePeSol = true;
                break;
            }
        }

        Debug.DrawRay(transform.position + Vector3.up * 0.5f, Vector3.down * distantaVerificareSol, estePeSol ? Color.green : Color.red);

        // Preluam input-ul de miscare
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        if (CameraSwitcher.Instance != null && CameraSwitcher.Instance.IsFirstPerson)
        {
            directieMiscare = (transform.forward * moveZ + transform.right * moveX).normalized;
        }
        else
        {
            if (mainCamera != null)
            {
                Vector3 cameraForward = mainCamera.transform.forward;
                Vector3 cameraRight = mainCamera.transform.right;

                cameraForward.y = 0f;
                cameraRight.y = 0f;

                cameraForward.Normalize();
                cameraRight.Normalize();

                directieMiscare = (cameraForward * moveZ + cameraRight * moveX).normalized;
            }
            else
            {
                directieMiscare = new Vector3(moveX, 0f, moveZ).normalized;
            }
        }

        if (Input.GetButtonDown("Jump") && estePeSol)
        {
            vreaSaSara = true;
        }

        // Transmitem viteza catre Animator
        if (animator != null)
        {
            animator.SetFloat("Speed", directieMiscare.magnitude);
        }
    }

    void FixedUpdate()
    {
        // Aplicam miscarea pe orizontala
        Vector3 vitezaTinta = directieMiscare * vitezaDeplasare;
        vitezaTinta.y = rb.linearVelocity.y; // Pastram viteza actuala de cadere/gravitatie

        // Daca jucătorul a apasat Space, ii modificam viteza pe axa Y instantaneu
        if (vreaSaSara)
        {
            vitezaTinta.y = fortaSaritura;
            vreaSaSara = false; // Resetam comanda
        }

        rb.linearVelocity = vitezaTinta;

        // Rotatia corpului (Doar in Third Person)
        if (directieMiscare != Vector3.zero)
        {
            if (CameraSwitcher.Instance != null && CameraSwitcher.Instance.IsFirstPerson)
            {
                // In First Person, mouse-ul roteste corpul
            }
            else
            {
                Quaternion rotatieTinta = Quaternion.LookRotation(directieMiscare, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotatieTinta, vitezaRotatie * Time.fixedDeltaTime);
            }
        }
    }
}