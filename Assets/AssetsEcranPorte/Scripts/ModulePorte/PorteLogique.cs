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

    private bool boutonAActif = false; // �tat du BoutonA
    private bool boutonBActif = false; // �tat du BoutonB

    public bool lastGate = false;

    // Types de portes logiques
    public enum TypePorte { AND, OR, XOR, NAND, NOR, XNOR };
    public TypePorte typePorte = TypePorte.AND; // Par d�faut, c'est une porte AND
    bool update_output()
    {
        // V�rifier si les boutons sont activ�s
        boutonAActif = BoutonA.GetComponent<ComposantLumineux>().estActif;
        boutonBActif = BoutonB.GetComponent<ComposantLumineux>().estActif;

        // V�rifier la logique
        bool resultat = false;
        //
        switch (typePorte)
        {
            case TypePorte.AND:
                resultat = boutonAActif && boutonBActif;
                break;

            case TypePorte.OR:
                resultat = boutonAActif || boutonBActif;
                break;

            case TypePorte.XOR:
                resultat = boutonAActif ^ boutonBActif;
                break;

            case TypePorte.NAND:
                resultat = !(boutonAActif && boutonBActif);
                break;

            case TypePorte.NOR:
                resultat = !(boutonAActif || boutonBActif);
                break;

            case TypePorte.XNOR:
                resultat = !(boutonAActif ^ boutonBActif);
                break;
        }
        // Mettre � jour l'�tat de la lumi�re
        light_Resultat.GetComponent<ComposantLumineux>().estActif = resultat;
        return resultat;
    }
    void newTypePorte()
    {
        // Choisir al�atoirement le type de porte
        typePorte = (TypePorte)Random.Range(0, System.Enum.GetValues(typeof(TypePorte)).Length);
    }



    // Start est appel� au d�but du jeu
    void Start()
    {
        //modulePorteLogiqueFinished = false;

        newTypePorte();
        update_output();

        boutonAActif = false; // Initialisation des �tats des boutons
        boutonBActif = false;

        while (update_output() && lastGate)
        {
            newTypePorte(); 
        }


        // Ajouter un composant TextMesh � l'objet de texte
        TextMeshPro textMesh = TextObject.GetComponent<TextMeshPro>();
        if (textMesh == null)
        {
            textMesh = TextObject.AddComponent<TextMeshPro>();
        }



        // D�finir le texte en fonction du type de porte
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

    // Update est appel� une fois par frame
    void Update()
    {
        /*
        if (!modulePorteLogiqueFinished)
        {
            bool b = update_output();
            
            if (lastGate && b)
            {
                print(name + "    FINISHED ==============");
                modulePorteLogiqueFinished = true;
            }
        }*/
        update_output();
        
    }
}
