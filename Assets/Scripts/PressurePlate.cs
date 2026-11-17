using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Conexiunea cu Usa")]
    [Tooltip("Trage aici obiectul BossGate din ierarhie")]
    [SerializeField] private Animator animatorUsa;

    private void OnTriggerEnter(Collider other)
    {
        // Cand cufarul intra pe placa
        if (other.CompareTag("Chest"))
        {
            if (animatorUsa != null)
            {
                // Spunem usii sa se deschida si oprim starea de inchidere
                animatorUsa.SetBool("DoorOpen", true);
                animatorUsa.SetBool("DoorClose", false);
                Debug.Log("Placa a fost apasata, BossGate-ul este deschis!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cand cufarul iese de pe placa 
        if (other.CompareTag("Chest"))
        {
            if (animatorUsa != null)
            {
                // Oprim starea de deschidere si activam inchiderea
                animatorUsa.SetBool("DoorOpen", false);
                animatorUsa.SetBool("DoorClose", true);
                Debug.Log("BossGate-ul a fost inchis");
            }
        }
    }
}