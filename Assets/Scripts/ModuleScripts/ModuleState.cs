using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleState : MonoBehaviour
{
    [SerializeField]
    protected Material successMaterial;

    [SerializeField]
    protected Material errorMaterial;

    [SerializeField]
    protected Material initialMaterial;

    [SerializeField]
    protected GameObject stateLED;

    [SerializeField]
    protected AudioSource audioSource;

    [SerializeField]
    protected AudioClip successSound;

    [SerializeField]
    protected AudioClip errorSound;

    [SerializeField]
    protected HighlightOnHover highlightOnHover;

    protected bool isSuccess = false;
    protected bool isError = false;

    /// <summary>
    /// Indique si le module est termin� (succ�s ou �chec).
    /// </summary>
    public bool IsModuleCompleted => isSuccess || isError;

    /// <summary>
    /// V�rifie si la condition de succ�s est remplie. � impl�menter dans les modules d�riv�s.
    /// </summary>
    public abstract bool CheckSuccessCondition();

    /// <summary>
    /// D�finit l'�tat de succ�s du module.
    /// </summary>
    public void SetSuccessState()
    {
        if (IsModuleCompleted) return;

        isSuccess = true;
        if (stateLED != null)
        {
            SetMaterial(stateLED, successMaterial);
        }
        PlaySound(successSound);
        DisableInteractions();
    }

    /// <summary>
    /// D�finit l'�tat d'erreur du module.
    /// </summary>
    public void SetErrorState()
    {
        if (IsModuleCompleted) return;

        isError = true;
        if (stateLED != null)
        {
            SetMaterial(stateLED, errorMaterial);
            StartCoroutine(ResetMaterialAfterDelay(stateLED, initialMaterial, 1f));
        }
        PlaySound(errorSound);
    }

    /// <summary>
    /// Applique un mat�riau � un GameObject.
    /// </summary>
    protected void SetMaterial(GameObject target, Material material)
    {
        MeshRenderer renderer = target.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }

    /// <summary>
    /// Joue un son � l'aide de l'AudioSource.
    /// </summary>
    protected void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// R�initialise le mat�riau apr�s un d�lai.
    /// </summary>
    protected IEnumerator ResetMaterialAfterDelay(GameObject target, Material material, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!isSuccess)
        {
            SetMaterial(target, material);
        }
    }

    /// <summary>
    /// D�sactive les interactions pour ce module.
    /// </summary>
    protected void DisableInteractions()
    {
        if (highlightOnHover != null)
        {
            highlightOnHover.enabled = false;
        }
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        this.enabled = false;
    }
}
