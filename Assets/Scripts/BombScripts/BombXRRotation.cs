using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BombXRRotation : MonoBehaviour
{
    public float RotationSpeed = 10f;
    private XRBaseInteractor interactor;
    private XRController controller; // Référence au contrôleur XR
    private Quaternion previousRotation;
    private bool isGrabbed = false;
    private bool isTriggerPressed = false; // Pour savoir si le trigger est pressé

    public void OnGrab(XRBaseInteractor interactor)
    {
        isGrabbed = true;
        this.interactor = interactor;
        previousRotation = interactor.transform.rotation;

        // Récupérer le XRController depuis l'interactor
        controller = interactor.GetComponent<XRController>();
    }

    public void OnRelease(XRBaseInteractor interactor)
    {
        isGrabbed = false;
        this.interactor = null;
        controller = null;
        isTriggerPressed = false;
    }

    private void Update()
    {
        if (isGrabbed && controller != null)
        {
            // Vérifier si le bouton trigger est pressé
            controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out isTriggerPressed);

            if (isTriggerPressed)
            {
                // Calcul de la différence de rotation entre la frame précédente et actuelle
                Quaternion deltaRotation = interactor.transform.rotation * Quaternion.Inverse(previousRotation);

                // Appliquer cette rotation à la bombe
                transform.rotation = deltaRotation * transform.rotation;

                // Mettre à jour la rotation précédente
                previousRotation = interactor.transform.rotation;
            }
        }
    }
}
