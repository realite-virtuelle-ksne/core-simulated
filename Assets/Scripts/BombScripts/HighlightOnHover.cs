using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HighlightOnHover : MonoBehaviour
{
    private Outline outline;
    [SerializeField] private int width = 4;
    private XRSimpleInteractable interactable;

    void Start()
    {
        // R�cup�re ou ajoute le composant Outline sur l'objet
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }
        outline.enabled = false;

        // R�cup�re le XRSimpleInteractable et ajoute les �v�nements
        interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.hoverExited.AddListener(OnHoverExit);
        }
    }

    void OnDestroy()
    {
        // Nettoie les �v�nements pour �viter des erreurs
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnHoverEnter);
            interactable.hoverExited.RemoveListener(OnHoverExit);
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        Debug.Log("Survol d�tect� : " + gameObject.name);
        Activate();
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        Debug.Log("Fin du survol : " + gameObject.name);
        Deactivate();
    }

    public void Activate()
    {
        outline.enabled = true;
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = width;
        outline.OutlineMode = Outline.Mode.OutlineAll;
    }

    public void Deactivate()
    {
        outline.enabled = false;
    }
}
