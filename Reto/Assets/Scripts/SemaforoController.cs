using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemaforoController : MonoBehaviour
{
    public List<Transform> Lights=new List<Transform>();

    public Material yellowMaterial;
    public Material redMaterial;
    public Material greenMaterial;
    public Material grayMaterial;

    float timer=0.0f;
    
    public bool type;

    public SemaforoState state;

    public enum SemaforoState
    {
        yellow,
        red,
        green
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Type1(float time){

    }

    void LightsType(){

        timer+=Time.deltaTime;

        if(type){
            if(timer<=10){
                TurnRed();
                state = SemaforoState.red;
            }else if(timer > 10 && timer <= 15){
                TurnGreen(); 
                state = SemaforoState.green;
            }else if(timer>=15 && timer <= 20){
                TurnYellow();
                state = SemaforoState.yellow;
            }
            if(timer>20){
                timer=0;
            }
        }else{
            if(timer<=5){
                TurnGreen(); 
                state = SemaforoState.green;
            }else if(timer > 5 && timer <= 10){
                TurnYellow(); 
                state = SemaforoState.yellow;
            }else if(timer>=10 && timer <= 20){
                TurnRed();
                state = SemaforoState.red;
            }
            if(timer>20){
                timer=0;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

        LightsType();

    }

    void TurnYellow(){
        Lights[0].GetComponent<Renderer>().material.color = grayMaterial.color;
        Lights[1].GetComponent<Renderer>().material.color = yellowMaterial.color;
        Lights[2].GetComponent<Renderer>().material.color = grayMaterial.color;
    }

    void TurnGreen(){
        Lights[0].GetComponent<Renderer>().material.color = grayMaterial.color;
        Lights[1].GetComponent<Renderer>().material.color = grayMaterial.color;
        Lights[2].GetComponent<Renderer>().material.color = greenMaterial.color;
    }

    void TurnRed(){
        Lights[0].GetComponent<Renderer>().material.color = redMaterial.color;
        Lights[1].GetComponent<Renderer>().material.color = grayMaterial.color;
        Lights[2].GetComponent<Renderer>().material.color = grayMaterial.color;
    }
}
