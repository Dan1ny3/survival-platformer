using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    [Header("Setari Muzica")]
    [Tooltip("Trage aici AudioManager-ul care canta muzica de pestera")]
    public AudioSource sursaMuzicaNivel;

    [Tooltip("Trage aici melodia pentru Golem Boss (fisierul .mp3 sau .wav)")]
    public AudioClip muzicaBoss;

    private bool aSchimbatMuzica = false;

    private void OnTriggerEnter(Collider other)
    {
        // Verificam daca jucatorul a intrat in arena si daca nu am schimbat deja muzica
        if (other.CompareTag("Player") && !aSchimbatMuzica)
        {
            if (sursaMuzicaNivel != null && muzicaBoss != null)
            {
                // Schimbam melodia si ii dam Play
                sursaMuzicaNivel.clip = muzicaBoss;
                sursaMuzicaNivel.Play();

                aSchimbatMuzica = true; // Ne asiguram ca nu se restarteaza daca jucătorul se plimba prin zona
                Debug.Log("A inceput muzica de Boss!");
            }
        }
    }
}