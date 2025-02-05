using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComposantLumineux : MonoBehaviour
{
    // Start is called before the first frame update
    public bool estActif = false; // État du bouton

    // Fonction appelée lorsqu'un joueur interagit avec le bouton

    void Start()
    {
        estActif = Random.value > 0.5f; // 50% de chance que estActif soit true
        GetComponent<Renderer>().material.color = estActif ? Color.green : Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = estActif ? Color.green : Color.red;
    }

    public void updateEstActif(bool b) { estActif = b; }
}
