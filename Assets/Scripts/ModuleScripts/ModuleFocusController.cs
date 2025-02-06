using System.Collections;
using UnityEngine;

public class ModuleFocusController : MonoBehaviour
{
    public Transform focusPoint;      // Point de focus pour les modules
    public float focusSpeed = 5f;     // Vitesse de focus sur le module
    public ObjectLiftController liftController; // R�f�rence au script de levage de la bombe

    private Transform currentModule;  // Le module actuellement s�lectionn�
    private bool isCameraFocusedOnModule = false; // Suivi de l'�tat de la cam�ra
    private Vector3 initialCameraPosition;  // Position initiale de la cam�ra
    private Quaternion initialCameraRotation; // Rotation initiale de la cam�ra

    void Start()
    {
        // Sauvegarder la position et la rotation initiale de la cam�ra
        initialCameraPosition = Camera.main.transform.position;
        initialCameraRotation = Camera.main.transform.rotation;
    }

    void Update()
    {
        // Si la bombe est lev�e, permet la s�lection des modules
        if (liftController != null && liftController.IsLifted())
        {
            // Si l'utilisateur clique sur un module
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // V�rifier si l'objet touch� est un module
                    if (hit.collider.CompareTag("Module"))
                    {
                        SelectModule(hit.collider.transform);
                    }
                }
            }

            // Si l'utilisateur clique droit
            if (Input.GetMouseButtonDown(1))
            {
                if (isCameraFocusedOnModule)
                {
                    // Si la cam�ra est focalis�e sur le module, on revient � la vue de la bombe lev�e
                    ReturnToLiftedBombView();
                }
                else
                {
                    // Si la cam�ra est sur la bombe, on repose la bombe
                    LowerObjectToInitialPosition();
                }
            }
        }
    }

    // S�lectionner un module et recentrer la cam�ra dessus
    void SelectModule(Transform module)
    {
        if (module == currentModule) return; // Si c'est d�j� le module s�lectionn�, ne rien faire

        currentModule = module;

        // Recentrer la cam�ra sur le module
        StartCoroutine(SmoothFocusMovement(module));
        isCameraFocusedOnModule = true; // La cam�ra est maintenant focalis�e sur le module
    }

    // Retour � la vue de la bombe lev�e
    void ReturnToLiftedBombView()
    {
        StartCoroutine(SmoothReturnToLiftedBombView());
        isCameraFocusedOnModule = false; // La cam�ra n'est plus focalis�e sur le module
    }

    // Repose la bombe � sa position initiale
    void LowerObjectToInitialPosition()
    {
        liftController.LowerObject();
        isCameraFocusedOnModule = false; // La cam�ra n'est plus focalis�e sur le module
    }

    // D�placement fluide de la cam�ra vers le module s�lectionn�
    IEnumerator SmoothFocusMovement(Transform module)
    {
        Vector3 targetPos = module.position + new Vector3(0, 0, -3f); // D�calage pour mieux zoomer sur le module
        Quaternion targetRot = Quaternion.LookRotation(module.position - Camera.main.transform.position);

        float elapsedTime = 0f;
        float duration = 1f / focusSpeed;

        Vector3 startPos = Camera.main.transform.position;
        Quaternion startRot = Camera.main.transform.rotation;

        while (elapsedTime < duration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);

            Camera.main.transform.position = Vector3.Lerp(startPos, targetPos, t);
            Camera.main.transform.rotation = Quaternion.Lerp(startRot, targetRot, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = targetPos;
        Camera.main.transform.rotation = targetRot;
    }

    // Retourne la cam�ra � la vue o� la bombe est lev�e
    IEnumerator SmoothReturnToLiftedBombView()
    {
        float elapsedTime = 0f;
        float duration = 1f / focusSpeed;

        Vector3 startPos = Camera.main.transform.position;
        Quaternion startRot = Camera.main.transform.rotation;

        while (elapsedTime < duration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);

            Camera.main.transform.position = Vector3.Lerp(startPos, liftController.liftedCameraPosition, t);
            Camera.main.transform.rotation = Quaternion.Lerp(startRot, liftController.liftedCameraRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = liftController.liftedCameraPosition;
        Camera.main.transform.rotation = liftController.liftedCameraRotation;
    }
}
