using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonBouton : MonoBehaviour
{
    [SerializeField]
    public AudioSource audioSource;

    [SerializeField]
    public AudioClip soundButtonPressed;

    private bool isHolding = false;

    private void Start()
    {
        isHolding = false;
        audioSource = GetComponent<AudioSource>();
    }
    
    public void Appuyer()
    {
        if (audioSource != null && soundButtonPressed != null)
        {
            audioSource.PlayOneShot(soundButtonPressed);
        }


    }

    public void Lacher()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
