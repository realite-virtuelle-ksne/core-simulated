using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PorteLogiqueComplexe : PorteLogique, IModule
{
    public GameObject PorteLogiqueA;
    public GameObject PorteLogiqueB;
    private bool moduleIsFInished = false;

    override
    protected internal bool UpdateOutput()
    {
        // Vérifier si les boutons sont activés
        bool LightAActif = PorteLogiqueA.GetComponent<PorteLogique>().light_Resultat.GetComponent<ComposantLumineux>().estActif;
        bool LightBActif = PorteLogiqueB.GetComponent<PorteLogique>().light_Resultat.GetComponent<ComposantLumineux>().estActif;

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


    // Start is called before the first frame update
    void Start()
    {
        NewTypePorte();

        PorteLogiqueA.GetComponent<PorteLogique>().StartPorte();
        PorteLogiqueB.GetComponent<PorteLogique>().StartPorte();

        UpdateOutput();

        while (UpdateOutput())
        {
            NewTypePorte();
            UpdateOutput();
        }

        InitTextMeshPro();
    }

    void Update()
    {
        if (!moduleIsFInished)
        {
            if (UpdateOutput())
            {
                moduleIsFInished = true;
            }
        }
    }

    // Implémentation de l'interface IModule
    public bool IsFinished()
    {
        return moduleIsFInished;
    }
}
