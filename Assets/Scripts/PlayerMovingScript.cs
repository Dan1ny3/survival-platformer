using UnityEngine;
using System.Collections;

public class PlayerMovingScript : MonoBehaviour
{
    private Rigidbody _Rigidbody;
    private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.3f;

    [Header("Wall Jump Settings")]
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float wallJumpSideForce = 5f; // Puterea pe orizontala cand sari de pe perete
    [SerializeField] private float wallJumpUpForce = 7f;   // Puterea pe verticala
    [SerializeField] private float wallCheckDistance = 0.6f;
    [SerializeField] private LayerMask wallLayer;

    [Header("Combat Combo")]
    private int comboStep = 0;
    private float lastAttackTime = 0f;
    [SerializeField] private float comboResetTime = 2.0f; // Daca trece un nr de secunde fara click, resetam

    private uint canDoubleJump = 0;
    private bool canDash = true;
    private bool isDashing = false;
    private int orientation = 1; // 1 = Right, -1 = Left

    private bool isTouchingWall;
    private bool isWallSliding;

    [Header("References")]
    [SerializeField] private ParticleSystem _DashParticleSystem;
    [SerializeField] private Transform swordTransform;

    private PlayerManagement player;
    private int playerLayer;
    private int enemyLayer;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        player = GetComponent<PlayerManagement>();

        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");

        if (_DashParticleSystem != null) _DashParticleSystem.Stop();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        if (Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
            if (animator != null) animator.SetInteger("Combo", 0);
        }

        if (isDashing) return;

        CheckSurroundings();
        HandleMovement();
        HandleWallSlide();
        ProcessInput();
    }

    private void CheckSurroundings()
    {
        isTouchingWall = Physics.Raycast(transform.position, Vector3.right * orientation, wallCheckDistance, wallLayer);
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
        }

        if (moveInput > 0)
        {
            orientation = 1;

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            orientation = -1;

            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (!isWallSliding)
        {
            _Rigidbody.linearVelocity = new Vector3(moveInput * speed, _Rigidbody.linearVelocity.y, 0f);
        }
    }

    private void HandleWallSlide()
    {

        if (isTouchingWall && _Rigidbody.linearVelocity.y < 0)
        {
            isWallSliding = true;
            _Rigidbody.linearVelocity = new Vector3(0, Mathf.Clamp(_Rigidbody.linearVelocity.y, -wallSlideSpeed, float.MaxValue), 0);
            canDoubleJump = 1; // Permitem o saritura suplimentara dupa ce plecam de pe perete
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isWallSliding)
            {
                WallJump();
            }
            else
            {
                Jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            // Salvam momentul in care am apasat tasta
            lastAttackTime = Time.time;

            // Crestem numarul combo-ului (trecem la urmatorul atac)
            comboStep++;

            // Daca e apasat de 5 ori, il dam inapoi la atacul 1
            if (comboStep > 4)
            {
                comboStep = 1;
            }

            // Trimitem numarul catre Animator
            if (animator != null) animator.SetInteger("Combo", comboStep);

            player?.Attack();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void WallJump()
    {
        // Saritura de pe perete 
        int jumpDirection = -orientation;

        // Resetam viteza inainte de a aplica forta
        _Rigidbody.linearVelocity = Vector3.zero;

        Vector3 forceToAdd = new Vector3(jumpDirection * wallJumpSideForce, wallJumpUpForce, 0);
        _Rigidbody.AddForce(forceToAdd, ForceMode.Impulse);

        // Intoarcem caracterul vizual
        orientation = jumpDirection;
    }

    private void Jump()
    {
        if (canDoubleJump >= 2) return;

        if (animator != null) animator.SetTrigger("Jump");

        Vector3 velocity = _Rigidbody.linearVelocity;
        velocity.y = 0;
        _Rigidbody.linearVelocity = velocity;

        _Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        canDoubleJump++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Cand atingem podeaua, resetam double jump-ul
        if (collision.gameObject.CompareTag("Platform"))
        {
            canDoubleJump = 0;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        if (animator != null) animator.SetTrigger("Dash");

        if (_DashParticleSystem != null) _DashParticleSystem.Play();

        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        _Rigidbody.linearVelocity = new Vector3(orientation * dashSpeed, 0, 0);

        yield return new WaitForSeconds(dashDuration);

        _Rigidbody.linearVelocity = new Vector3(0, _Rigidbody.linearVelocity.y, 0);
        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        if (_DashParticleSystem != null) _DashParticleSystem.Stop();

        isDashing = false;
        yield return new WaitForSeconds(1f);
        canDash = true;
    }
}