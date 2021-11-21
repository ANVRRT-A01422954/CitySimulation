using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAgent : MonoBehaviour
{

    private CarState currentState;
    public Node currentNode;
    private Vector3 currentNodePos;

    public float speed = 15.0f;
    private float time = 0.0f;

    public string agentName = "";

    public enum CarState
    {
        driving,
        stopped
    }

    // Start is called before the first frame update
    void Start()
    {
        StartAgent();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAgent();
    }

    private void StartAgent(){
        currentState = CarState.driving;
        MoveToNode();
    }

    private void StopAgent(){
        currentState = CarState.stopped;
    }

    private void ReactivateAgent(){
        CheckTrafficLight();
        
    }

    private void MoveAgent(){
        if (currentState == CarState.driving){
            CalculateMovement();
            FaceToDestination();
        }else if (currentState == CarState.stopped){
            ReactivateAgent();
        }


    }

    private void CalculateMovement(){
        currentNodePos = currentNode.GetNodePosition(this.transform.position.y);

        if(Vector3.Distance(this.transform.position, currentNodePos) < 0.1f){
            if(CheckTrafficLight()){
                return;
            }

            // Verifica si es intersección
            VerifyIntersection();
            //Si si es intersección 
            currentNode = currentNode.GetRandomNode();
        }else{
            MoveToNode();
        }

    }

    private void MoveToNode(){
        
        transform.position = Vector3.MoveTowards(transform.position, currentNodePos, speed * Time.deltaTime);
    }

    private void FaceToDestination(){
        transform.LookAt(new Vector3(currentNode.transform.position.x, this.transform.position.y, currentNode.transform.position.z));
    }

    private void VerifyIntersection(){
        if(currentNode.isIntersection){
            if(currentNode.intersectionType == "Start"){
                time = Time.time;
            }else if(currentNode.intersectionType == "End"){
                float endTime = Time.time - time;
                print("End time of " + agentName + " : " + endTime);

            }

        }
    }

    private bool CheckTrafficLight(){
        if(currentNode.hasTrafficLight){
            if(currentNode.semaforoController.state == SemaforoController.SemaforoState.red){
                print(agentName + " is waiting for the traffic light");
                StopAgent();
                return true;

            }else if(currentNode.semaforoController.state == SemaforoController.SemaforoState.green){
                currentState = CarState.driving;
            }
        }
        return false;
    }


    //On trigger enter
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarAgent")
        {
            print("CarAgent " + agentName + " collided with " + other.gameObject.GetComponent<CarAgent>().agentName);
            StopAgent();
        }
    }


    // void OnColi(Collider other)
    // {
    //     print("Car: "+ agentName +" Collision with " + other.GetComponent<CarAgent>().agentName);
    // }



}
