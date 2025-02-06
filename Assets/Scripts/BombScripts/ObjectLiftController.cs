using System.Collections;
using UnityEngine;

public class ObjectLiftController : MonoBehaviour
{
    public Transform targetPosition; // Position o� placer l'objet soulev�
    public float moveSpeed = 5f;     // Vitesse du d�placement
    public BombRotation bombRotation; // R�f�rence au script de rotation
    public float rotationSpeed = 5f;  // Vitesse de rotation vers la cam�ra

    private bool isLifted = false;    // �tat de l'objet (soulev� ou non)
    private Vector3 originalPosition; // Position initiale de l'objet
    private Quaternion originalRotation; // Rotation initiale de l'objet

    public Vector3 liftedCameraPosition; // Position de la cam�ra lorsque la bombe est lev�e
    public Quaternion liftedCameraRotation; // Rotation de la cam�ra lorsque la bombe est lev�e

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        if (bombRotation != null)
        {
            bombRotation.enabled = false; // D�sactiver la rotation libre d�s le d�part
        }
    }

    void Update()
    {
        // Interaction pour soulever l'objet
        if (Input.GetMouseButtonDown(0) && IsMouseOverObject() && !isLifted)
        {
            LiftObject();
        }

        // Interaction pour reposer l'objet au sol
        if (Input.GetMouseButtonDown(1) && isLifted)
        {
            LowerObject(); // Reposer la bombe
        }
    }

    public void LiftObject()
    {
        isLifted = true;

        // Sauvegarder la position de la cam�ra lorsque la bombe est lev�e
        liftedCameraPosition = Camera.main.transform.position;
        liftedCameraRotation = Camera.main.transform.rotation;

        // D�placement fluide vers la position cible
        StartCoroutine(SmoothLiftMovement(targetPosition.position, Quaternion.LookRotation(Camera.main.transform.forward)));

        // Activer la rotation contr�l�e par la souris
        if (bombRotation != null)
        {
            bombRotation.enabled = true;
        }
    }

    public void LowerObject()
    {
        isLifted = false;

        // D�placement fluide vers la position initiale
        StartCoroutine(SmoothLiftMovement(originalPosition, originalRotation));

        // D�sactiver la rotation contr�l�e par la souris
        if (bombRotation != null)
        {
            bombRotation.enabled = false;
        }
    }

    IEnumerator SmoothLiftMovement(Vector3 targetPos, Quaternion targetRot)
    {
        float elapsedTime = 0f;
        float duration = 1f / moveSpeed; // Ajuster la dur�e selon la vitesse

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (elapsedTime < duration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);

            // Mouvement et rotation fluide
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Lerp(startRot, targetRot, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assure la position et la rotation exactes
        transform.position = targetPos;
        transform.rotation = targetRot;
    }

    // V�rifie si la souris est sur l'objet
    bool IsMouseOverObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject == gameObject;
        }
        return false;
    }

    // Retourne vrai si l'objet est soulev�
    public bool IsLifted()
    {
        return isLifted;
    }
}
