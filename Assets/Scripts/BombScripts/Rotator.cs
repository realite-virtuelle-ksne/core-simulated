using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private Transform interactorTransform;
    private bool isRotating = false;
    private Vector3 lastPosition;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Événements pour le grab et l'activation
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
        grabInteractable.activated.AddListener(OnActivate);
        grabInteractable.deactivated.AddListener(OnDeactivate);
    }

    public void OnGrab(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRBaseControllerInteractor interactor)
        {
            interactorTransform = interactor.transform;
            lastPosition = interactorTransform.position;
        }
    }

    public void OnRelease(SelectExitEventArgs args)
    {
        isRotating = false;
        interactorTransform = null;
    }

    public void OnActivate(ActivateEventArgs args)
    {
        isRotating = true;
    }

    public void OnDeactivate(DeactivateEventArgs args)
    {
        isRotating = false;
    }

    public void FixedUpdate()
    {
        if (isRotating && interactorTransform)
        {
            Vector3 deltaPosition = interactorTransform.position - lastPosition;
            Vector3 rotationAxis = Vector3.Cross(deltaPosition, Vector3.up);
            float rotationAmount = deltaPosition.magnitude * rotationSpeed;

            rb.AddTorque(rotationAxis * rotationAmount);
            lastPosition = interactorTransform.position;
        }
    }
}
