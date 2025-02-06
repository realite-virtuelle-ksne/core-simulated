using UnityEngine;

public class BombRotation : MonoBehaviour
{
    public float PCRotationSpeed = 10f; // Vitesse de rotation pour PC
    public Camera Cam;                 // Cam�ra utilis�e pour d�finir l'axe de rotation

    // Variable statique pour v�rifier si un module est focus
    public static bool IsModuleFocused = false;

    public void Activate()
    {
        // Emp�che la rotation si un module est focus
        if (IsModuleFocused) return;

        float RotX = Input.GetAxis("Mouse X") * PCRotationSpeed;
        float RotY = Input.GetAxis("Mouse Y") * PCRotationSpeed;

        Vector3 right = Vector3.Cross(Cam.transform.up, transform.position - Cam.transform.position);
        Vector3 up = Vector3.Cross(transform.position - Cam.transform.position, right);

        transform.rotation = Quaternion.AngleAxis(-RotX, up) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(RotY, right) * transform.rotation;
    }
}
