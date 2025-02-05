using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EcranOrdinateur : MonoBehaviour
{
    public GameObject TextObject; // Objet contenant le texte (par exemple, un cube ou autre)
    private TextMeshPro textMesh;
    private Queue<string> lines = new Queue<string>(); // File pour stocker les lignes de texte
    public int maxLines = 15; // Nombre maximum de lignes � afficher

    // Start is called before the first frame update
    void Start()
    {
        // R�cup�rer ou ajouter le composant TextMeshPro
        textMesh = TextObject.GetComponent<TextMeshPro>();
        if (textMesh == null)
        {
            textMesh = TextObject.AddComponent<TextMeshPro>();
        }

        // Initialisation du texte
        textMesh.text = "";
        Application.logMessageReceived += HandleLog; // Abonnement aux messages de la console
    }

    // Ajoute une ligne de texte dans la file et met � jour l'affichage
    public void AddText(string t)
    {
        lines.Enqueue(t); // Ajouter la nouvelle ligne
        if (lines.Count > maxLines)
        {
            lines.Dequeue(); // Supprimer la ligne la plus ancienne si la limite est d�pass�e
        }

        // Mettre � jour le texte affich� en concat�nant les lignes
        textMesh.text = string.Join("\n", lines);
    }

    // R�initialise l'�cran
    public void Reset()
    {
        lines.Clear(); // Vide la file des lignes
        textMesh.text = ""; // Vide l'affichage
    }

    // Capture des logs de la console et ajout � l'�cran
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        AddText(logString); // Ajoute le message de log � l'�cran
    }

    // Nettoyage lors de la destruction du GameObject
    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog; // D�sabonnement des logs
    }
}
