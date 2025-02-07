/***
 * Script permettant de gérer les sons d'erreur et de succès du module
 * Date de création : janvier 2025
 * Auteur : Uzeir JOOMUN
 */

using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip soundButtonPressed, soundErrorModule, soundSuccessModule;

    public void PlayButtonSound()
    {
        if (audioSource != null && soundButtonPressed != null)
            audioSource.PlayOneShot(soundButtonPressed);
    }

    public void PlaySuccessSound()
    {
        if (audioSource != null && soundSuccessModule != null)
            audioSource.PlayOneShot(soundSuccessModule);
    }

    public void PlayErrorSound()
    {
        if (audioSource != null && soundErrorModule != null)
            audioSource.PlayOneShot(soundErrorModule);
    }
}
