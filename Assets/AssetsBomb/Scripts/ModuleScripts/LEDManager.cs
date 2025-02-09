/***
 * Script permettant de gérer la logique de la LED placée dans les différents modules de la bombe
 * Date de création : janvier 2025
 * Auteur : Uzeir JOOMUN
 */

using UnityEngine;
using System.Collections;

public class LEDManager : MonoBehaviour
{
    [SerializeField]
    private GameObject stateModuleLED;

    [SerializeField]
    private Material successModuleMaterial, errorModuleMaterial, initialGlassMaterial;


    /**
     * Permet de changer de material lorsque le module est réussi. La LED passe donc en vert.
     */
    public void SetSuccessMaterial()
    {
        SetMaterial(successModuleMaterial);
    }

    /**
     * Permet de changer de material lorsque le module est non réussi. La LED passe donc en rouge pendant un court temps.
     */
    public void SetErrorMaterial()
    {
        SetMaterial(errorModuleMaterial);
        StartCoroutine(ResetMaterialAfterDelay(1f));
    }

    private void SetMaterial(Material material)
    {
        MeshRenderer renderer = stateModuleLED.GetComponent<MeshRenderer>();
        if (renderer != null)
            renderer.material = material;
    }

    /**
     * Reset le material quand le module a été raté.
     */
    private IEnumerator ResetMaterialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetMaterial(initialGlassMaterial);
    }
}
