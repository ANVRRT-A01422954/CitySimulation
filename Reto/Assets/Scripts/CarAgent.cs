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

    private void MoveAgent(){
        if (currentState == CarState.stopped) return;

        CalculateMovement();
        FaceToDestination();
    }

    private void CalculateMovement(){
        currentNodePos = currentNode.GetNodePosition(this.transform.position.y);

        if(Vector3.Distance(this.transform.position, currentNodePos) < 0.1f){
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


}
