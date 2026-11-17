using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [Header("Setari Mouse")]
    public float sensibilitateMouse = 200f;

    [Tooltip("Trage aici corpul jucatorului (MaleCharacter)")]
    public Transform corpulJucatorului;

    private float rotatieX = 0f;

    void OnEnable()
    {
        // Cand camera FPS se activeaza, ascundem si blocam cursorul pe centrul ecranului
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        // Cand camera FPS se dezactiveaza, eliberam mouse-ul
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // Citim miscarea mouse-ului
        float mouseX = Input.GetAxis("Mouse X") * sensibilitateMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilitateMouse * Time.deltaTime;

        // Calculam rotatia pe verticala si o limitam 
        rotatieX -= mouseY;
        rotatieX = Mathf.Clamp(rotatieX, -90f, 90f);

        // Aplicam rotatia verticala doar pe camera 
        transform.localRotation = Quaternion.Euler(rotatieX, 0f, 0f);

        // Aplicam rotatia stanga-dreapta pe tot corpul jucatorului
        corpulJucatorului.Rotate(Vector3.up * mouseX);
    }
}