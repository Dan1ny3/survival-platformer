using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public static CameraSwitcher Instance { get; private set; }

    [Header("Camerele din Joc")]
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;

    private bool isFirstPerson = false;
    public bool IsFirstPerson => isFirstPerson;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        // Incepem jocul in Third Person
        SetFirstPerson(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            SetFirstPerson(!isFirstPerson);
        }
    }

    // Functie centrala care activeaza/dezactiveaza camerele corect
    public void SetFirstPerson(bool state)
    {
        isFirstPerson = state;
        if (thirdPersonCamera != null) thirdPersonCamera.SetActive(!isFirstPerson);
        if (firstPersonCamera != null) firstPersonCamera.SetActive(isFirstPerson);
    }
}