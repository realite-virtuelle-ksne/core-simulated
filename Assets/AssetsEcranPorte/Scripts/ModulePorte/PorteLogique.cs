using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

public class PorteLogique : MonoBehaviour
{
    //static public bool modulePorteLogiqueFinished = false;


    public GameObject BoutonA; // Bouton 1
    public GameObject BoutonB; // Bouton 2
    public GameObject light_Resultat; // Objet dont la couleur changera
    public GameObject TextObject; // Objet contenant le texte (par exemple, un cube)

    private bool LightAActif = false; // État du BoutonA
    private bool LightBActif = false; // État du BoutonB
    // Types de portes logiques
    public enum TypePorte { AND, OR, XOR, NAND, NOR, XNOR };
    public TypePorte typePorte = TypePorte.AND; // Par défaut, c'est une porte AND
    
    protected static bool moduleIsFInished = false;
    protected internal virtual bool UpdateOutput()
    {
        // Vérifier si les boutons sont activés
        LightAActif = BoutonA.GetComponent<ComposantLumineux>().estActif;
        LightBActif = BoutonB.GetComponent<ComposantLumineux>().estActif;

        // Vérifier la logique
        bool resultat = false;
        //
        switch (typePorte)
        {
            case TypePorte.AND:
                resultat = LightAActif && LightBActif;
                break;

            case TypePorte.OR:
                resultat = LightAActif || LightBActif;
                break;

            case TypePorte.XOR:
                resultat = LightAActif ^ LightBActif;
                break;

            case TypePorte.NAND:
                resultat = !(LightAActif && LightBActif);
                break;

            case TypePorte.NOR:
                resultat = !(LightAActif || LightBActif);
                break;

            case TypePorte.XNOR:
                resultat = !(LightAActif ^ LightBActif);
                break;
        }
        // Mettre à jour l'état de la lumière
        light_Resultat.GetComponent<ComposantLumineux>().estActif = resultat;
        return resultat;
    }
    public void NewTypePorte()
    {
        // Choisir aléatoirement le type de porte
        typePorte = (TypePorte)Random.Range(0, System.Enum.GetValues(typeof(TypePorte)).Length);
    }

    protected void InitTextMeshPro()
    {
        // Ajouter un composant TextMesh à l'objet de texte
        TextMeshPro textMesh = TextObject.GetComponent<TextMeshPro>();
        if (textMesh == null)
        {
            textMesh = TextObject.AddComponent<TextMeshPro>();
        }

        // Définir le texte en fonction du type de porte
        switch (typePorte)
        {
            case TypePorte.AND:
                textMesh.text = "AND";
                break;

            case TypePorte.OR:
                textMesh.text = "OR";
                break;

            case TypePorte.XOR:
                textMesh.text = "XOR";
                break;

            case TypePorte.NAND:
                textMesh.text = "NAND";
                break;

            case TypePorte.NOR:
                textMesh.text = "NOR";
                break;

            case TypePorte.XNOR:
                textMesh.text = "XNOR";
                break;
        }
    }


    protected internal void StartPorte()
    {
        NewTypePorte();
        UpdateOutput();

        InitTextMeshPro();
    }

    // Update est appelé une fois par frame
    void Update()
    {
        if (!moduleIsFInished)
        {
            UpdateOutput();
        }
    }
}
