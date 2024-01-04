using UnityEngine;

public class Mortedade : MonoBehaviour
{
    public AudioClip audioClip; // Áudio a ser reproduzido
    private AudioSource audioSource; // Referência ao componente AudioSource

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtém a referência ao componente AudioSource

        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip); // Reproduz o áudio uma vez
        }
    }
}