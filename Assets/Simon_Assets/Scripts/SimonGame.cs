using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonGame : MonoBehaviour
{
    public GameObject padR;
    public GameObject padV;
    public GameObject padB;
    public GameObject padJ;
    public bool gameNotStarted = true;
    public int nb_rounds = 5;
    public int actual_round=1;
    public List<int> sequence;
    public List<int> player_sequence;
    public int nb_pad_pressed=0;
    public bool player_failed;
    public bool player_succed = false;
    public AudioSource Success_Sequence_audio;
    public AudioSource Fail_Sequence_audio;
    public AudioSource Pad_Pressed;
    public void padRPressed(){
        if(gameNotStarted){
            InitSimonGame();
        }else{
            Pad_Pressed.Play();
            player_sequence.Add(0);
            checkSuccessPress();
        }
    }
    public void padVPressed(){
        if(gameNotStarted){
            InitSimonGame();
        }else{
            Pad_Pressed.Play();
            player_sequence.Add(1);
            checkSuccessPress();
        }
    }
    public void padBPressed(){
        if(gameNotStarted){
            InitSimonGame();
        }else{
            Pad_Pressed.Play();
            player_sequence.Add(2);
            checkSuccessPress();
        }
    }
    public void padJPressed(){
        if(gameNotStarted){
            InitSimonGame();
        }else{
            Pad_Pressed.Play();
            player_sequence.Add(3);
            checkSuccessPress();
        }
    }

    private void InitSimonGame(){
        gameNotStarted = !gameNotStarted;
        for(;actual_round<nb_rounds;actual_round++){
            Simon(actual_round);
        }
        player_succed = true;
    }
    private void Simon(int actual_round_length){
        showSequence(actual_round_length);
        StartCoroutine(WaitPlayerInterraction);
        if(!player_failed){
            Success_Sequence_audio.Play();
        }else{
            player_failed = false;
            actual_round -= 1; // restart sequence
        }

    }
    private bool checkSuccessPress(){
        index = player_sequence.Count;
        if(player_sequence[index] != sequence[index]){
            player_failed = true;
        }
    }
    private void showSequence(int actual_round_length){}
    IEnumerator WaitStartingTheGame(){
        yield return new WaitWhile(() => gameNotStarted);
        initSimonGame();
    }
    IEnumerator WaitPlayerInterraction(){
        yield return new WaitWhile(() => nb_pad_pressed<actual_round && !player_failed);
    }
    // Start is called before the first frame update
    void Start()
    {
        sequence = new List<int>();
        player_sequence = new List<int>();
        for(int i= 0; i < nb_rounds; i++){
            sequence.Add(Random.Range(0,4)); // Nombre entier entre 0 à 3 inclut # 0 : R, 1 : V, 2 : B, 3 : J 
        }
        StartCoroutine(WaitStartingTheGame());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
