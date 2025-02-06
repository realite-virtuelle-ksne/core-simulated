using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField]
    private float holdTime = 2f; // Temps pour maintenir le bouton avant de changer le mat�riau

    [SerializeField]
    private TextMeshPro digitsTimer;

    [SerializeField]
    private TextMeshPro error1;

    [SerializeField]
    private TextMeshPro error2;

    [SerializeField]
    public AudioSource audioSource;

    [SerializeField]
    public AudioClip soundButtonPressed;

    [SerializeField]
    public AudioClip soundErrorModule;

    [SerializeField] 
    public AudioClip soundSuccessModule;

    [SerializeField]
    public Material successModuleMaterial; // Mat�riau pour le succ�s

    [SerializeField]
    public Material errorModuleMaterial; // Mat�riau pour les erreurs

    [SerializeField]
    public Material lightBandMaterial; // Mat�riau pour l'�tat "en attente"

    [SerializeField]
    public Material initialGlassMaterial; // Mat�riau initial pour la LED et le lightBand

    [SerializeField]
    public GameObject lightBand;

    [SerializeField]
    public GameObject stateModuleLED;

    [SerializeField]
    private HighlightOnHover highlightOnHover;

    public XRBaseInteractor interactor;


    private bool isHolding = false; // Indique si le bouton est maintenu
    private float holdTimer = 0f; // Minuteur pour le maintien du bouton
    private Coroutine lightBandCoroutine = null; // R�f�rence � la coroutine de changement de mat�riau
    private bool moduleSuccess = false; // Indique si le module a �t� r�ussi


    void Update()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;
        }
    }

    public void Activate()
    {
        if (!this.enabled) return;


        // Jouer le son lorsque le bouton est appuy�
        if (audioSource != null && soundButtonPressed != null)
        {
            audioSource.PlayOneShot(soundButtonPressed);
        }

        // Commencer � tenir le bouton
        isHolding = true;
        holdTimer = 0f;

        // D�marrer une coroutine pour changer le mat�riau apr�s 1 seconde
        if (lightBandCoroutine != null)
        {
            StopCoroutine(lightBandCoroutine);
        }
        lightBandCoroutine = StartCoroutine(ChangeLightBandMaterialAfterDelay(lightBandMaterial, 1f));
    }

    public void Deactivate()
    {
        if (moduleSuccess)
        {
            // Ne rien r�initialiser si le module a d�j� �t� r�ussi
            return;
        }

        // R�initialiser imm�diatement le mat�riau du lightBand
        if (lightBandCoroutine != null)
        {
            StopCoroutine(lightBandCoroutine);
            lightBandCoroutine = null;
        }
        SetLightBandMaterial(initialGlassMaterial);

        MeshRenderer stateModuleLEDMesh = stateModuleLED.GetComponent<MeshRenderer>();


        //Module pass� avec succ�s
        if (holdTimer >= holdTime && digitsTimer.text.Contains("5"))
        {
            // Succ�s : changer les mat�riaux de la LED
            moduleSuccess = true; // Indiquer que le module a �t� r�ussi
            if (stateModuleLEDMesh != null)
            {
                stateModuleLEDMesh.material = successModuleMaterial;
            }

            audioSource.PlayOneShot(soundSuccessModule);

            DisableModule();

            // Laisser le mat�riau de succ�s appliqu� sans le r�initialiser
        }
        else
        {
            // Erreur : gestion des erreurs (mise � jour des couleurs et du mat�riau)
            if (error1.color != Color.red)
            {
                error1.color = Color.red;
                audioSource.PlayOneShot(soundErrorModule);
            }
            else if (error2.color != Color.red)
            {
                error2.color = Color.red;
                audioSource.PlayOneShot(soundErrorModule);
            }
            else
            {
                // Explosion ou autre action critique � d�finir
            }

            // Mettre � jour le mat�riau de la LED pour signaler une erreur
            if (stateModuleLEDMesh != null)
            {
                stateModuleLEDMesh.material = errorModuleMaterial;

                // Revenir au mat�riau initial apr�s une seconde
                StartCoroutine(ResetMaterialAfterDelay(stateModuleLED, initialGlassMaterial, 1f));
            }
        }

        // R�initialiser les �tats
        isHolding = false;
        holdTimer = 0f;
    }

    // M�thode pour changer le mat�riau du lightBand
    private void SetLightBandMaterial(Material material)
    {
        if (lightBand != null)
        {
            MeshRenderer renderer = lightBand.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
    }

    // Coroutine pour changer le mat�riau du lightBand apr�s un d�lai
    private IEnumerator ChangeLightBandMaterialAfterDelay(Material material, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Appliquer le mat�riau si le bouton est toujours appuy� et que le module n'a pas encore �t� r�ussi
        if (isHolding && !moduleSuccess)
        {
            SetLightBandMaterial(material);
        }
    }

    // Coroutine pour r�initialiser le mat�riau apr�s un d�lai
    private IEnumerator ResetMaterialAfterDelay(GameObject targetObject, Material material, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!moduleSuccess) // Ne r�initialise pas si le module a �t� r�ussi
        {
            MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
    }

    private void DisableModule()
    {
        if (highlightOnHover != null)
        {
            highlightOnHover.enabled = false;
        }

        // D�sactiver le collider pour bloquer les clics et survols
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // D�sactiver ce script
        this.enabled = false;
    }

}
