using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BombXRRotation : MonoBehaviour
{
    public float RotationSpeed = 10f;
    private XRBaseInteractor interactor;
    private XRController controller; // R�f�rence au contr�leur XR
    private Quaternion previousRotation;
    private bool isGrabbed = false;
    private bool isTriggerPressed = false; // Pour savoir si le trigger est press�

    public void OnGrab(XRBaseInteractor interactor)
    {
        isGrabbed = true;
        this.interactor = interactor;
        previousRotation = interactor.transform.rotation;

        // R�cup�rer le XRController depuis l'interactor
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
            // V�rifier si le bouton trigger est press�
            controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out isTriggerPressed);

            if (isTriggerPressed)
            {
                // Calcul de la diff�rence de rotation entre la frame pr�c�dente et actuelle
                Quaternion deltaRotation = interactor.transform.rotation * Quaternion.Inverse(previousRotation);

                // Appliquer cette rotation � la bombe
                transform.rotation = deltaRotation * transform.rotation;

                // Mettre � jour la rotation pr�c�dente
                previousRotation = interactor.transform.rotation;
            }
        }
    }
}
