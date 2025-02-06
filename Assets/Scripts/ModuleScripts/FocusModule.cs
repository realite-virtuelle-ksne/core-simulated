using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusModule : MonoBehaviour
{
    public Transform targetObject;  // L'objet entier � d�placer
    public Camera vrCamera;         // La cam�ra VR (si non assign�e, prend la cam�ra principale)
    public float distanceFromCamera = 2.0f; // Distance entre l'objet et la cam�ra

    void Start()
    {
        if (vrCamera == null)
        {
            vrCamera = Camera.main;
        }

        if (vrCamera == null)
        {
            Debug.LogError("Aucune cam�ra trouv�e. Assignez une cam�ra manuellement.");
            return;
        }

        if (targetObject == null)
        {
            Debug.LogError("Aucun targetObject assign�. Assignez l'objet � d�placer dans l'inspecteur.");
            return;
        }
    }

    void Update()
    {
        // D�tecte un clic gauche ou un appui sur le bouton principal du contr�leur
        if (Input.GetMouseButtonDown(0)) // Pour la souris ; pour le VR, utilisez un autre syst�me d'entr�e
        {
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        // �met un rayon depuis la cam�ra en direction du point cliqu�
        Ray ray = vrCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // V�rifie si le rayon touche cet objet (module)
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform) // V�rifie que l'objet cliqu� est bien le module
            {
                MoveObjectToCameraCenter();
            }
        }
    }

    private void MoveObjectToCameraCenter()
    {
        // Calcule la nouvelle position de l'objet en fonction de la cam�ra
        Vector3 cameraForward = vrCamera.transform.forward;
        Vector3 cameraPosition = vrCamera.transform.position;

        // Positionne l'objet � une distance d�finie devant la cam�ra
        Vector3 targetPosition = cameraPosition + cameraForward * distanceFromCamera;

        // D�place l'objet entier
        targetObject.position = targetPosition;

        // Oriente l'objet entier pour qu'il fasse face � la cam�ra
        targetObject.LookAt(vrCamera.transform, Vector3.up);
    }
}
