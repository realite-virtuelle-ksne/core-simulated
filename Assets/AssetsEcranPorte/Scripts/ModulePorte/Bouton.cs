using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouton : ComposantLumineux
{
    // Fonction appelée lorsqu'un joueur interagit avec le bouton
    public void ActiverBouton()
    {
        estActif = !estActif; // Alterner l'état du bouton
    }

    public void Activate()
    {
        ActiverBouton(); // Activer ou désactiver le bouton lorsqu'il est cliqué
    }
}
