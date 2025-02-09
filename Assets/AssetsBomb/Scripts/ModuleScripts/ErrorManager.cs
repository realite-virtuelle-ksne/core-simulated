/***
 * Script permettant de gérer les erreurs faites sur un module de la bombe
 * Date de création : janvier 2025
 * Auteur : Uzeir JOOMUN
 */

using TMPro;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro error1;

    [SerializeField]
    private TextMeshPro error2;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip soundErrorModule;

    public bool HandleError()
    {
        if (error1.color != Color.red)
        {
            error1.color = Color.red;
            audioSource.PlayOneShot(soundErrorModule);
            return false;
        }
        else if (error2.color != Color.red)
        {
            error2.color = Color.red;
            audioSource.PlayOneShot(soundErrorModule);
            return false;
        }
        else
        {
            // Explosion (bruit d'explosion, musique de partie perdue, mettre l'écran en noir, afficher le menu)
            // 
            return true;
        }
    }
}
