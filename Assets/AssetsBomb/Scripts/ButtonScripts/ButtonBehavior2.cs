/***
 * Script permettant de gérer la logique du module "Bouton"
 * Date de création : janvier 2025
 * Auteur : Uzeir JOOMUN
 */

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonBehavior2 : MonoBehaviour
{
    [SerializeField]
    private float holdTime = 2f;

    [SerializeField]
    private TextMeshPro digitsTimer;

    [SerializeField]
    private GameObject lightBand;

    [SerializeField]
    private Material lightBandMaterial, initialGlassMaterial;

    [SerializeField]
    private HighlightOnHover highlightOnHover;

    public XRBaseInteractor interactor;

    private bool isHolding = false;
    private float holdTimer = 0f;
    private Coroutine lightBandCoroutine = null;
    private bool moduleSuccess = false;

    private SoundManager soundManager;
    private LEDManager ledManager;
    private ErrorManager errorManager;


    //Chargement du script
    private void Awake()
    {
        soundManager = GetComponent<SoundManager>();
        ledManager = GetComponent<LEDManager>();
        errorManager = GetComponent<ErrorManager>();
    }

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

        soundManager.PlayButtonSound();
        isHolding = true;
        holdTimer = 0f;

        if (lightBandCoroutine != null)
            StopCoroutine(lightBandCoroutine);

        lightBandCoroutine = StartCoroutine(ChangeLightBandMaterialAfterDelay(1f));
    }

    public void Deactivate()
    {
        if (moduleSuccess) return;

        if (lightBandCoroutine != null)
            StopCoroutine(lightBandCoroutine);

        SetLightBandMaterial(initialGlassMaterial);

        //Le module se désactive et est résolu lorsque qu'il est relaché au moment ou il y a un 5 à n'importe quel position du timer de la bombe
        if (holdTimer >= holdTime && digitsTimer.text.Contains("5"))
        {
            moduleSuccess = true;
            ledManager.SetSuccessMaterial();
            soundManager.PlaySuccessSound();
            DisableModule();
        }
        else
        {
            if (!errorManager.HandleError())
                ledManager.SetErrorMaterial();
        }

        isHolding = false;
        holdTimer = 0f;
    }

    /**
     * Changement du material de la bande lumineuse à droite de la bombe lorsque le bouton est maintenu plus d'une seconde
     */
    private void SetLightBandMaterial(Material material)
    {
        if (lightBand != null)
        {
            MeshRenderer renderer = lightBand.GetComponent<MeshRenderer>();
            if (renderer != null)
                renderer.material = material;
        }
    }

    /**
     * Active la lumière après avoir passé un certain temps à maintenir le bouton 
     */
    private IEnumerator ChangeLightBandMaterialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isHolding && !moduleSuccess)
            SetLightBandMaterial(lightBandMaterial);
    }


    /**
     * Permet de désactiver le module lorsque celui-ci est résolu
     */
    private void DisableModule()
    {
        if (highlightOnHover != null)
            highlightOnHover.enabled = false;

        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;

        this.enabled = false;
    }
}
