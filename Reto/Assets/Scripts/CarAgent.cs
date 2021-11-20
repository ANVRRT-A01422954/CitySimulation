using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAgent : MonoBehaviour
{

    private CarState currentState;

    public Node currentNode;
    private Vector3 currentNodePos;

    public float speed = 15.0f;

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
            currentNode = currentNode.GetRandomNode();

        }else{
            MoveToNode();
            print("Moving to node");
        }

    }

    private void MoveToNode(){
        
        transform.position = Vector3.MoveTowards(transform.position, currentNodePos, speed * Time.deltaTime);
    }

    private void FaceToDestination(){
        // Vector3 direction = currentNode.transform.position - transform.position;
        // Quaternion lookRotation = Quaternion.LookRotation(direction);

        // float step = speed * Time.deltaTime;
        // this.transform.LookAt(currentNode.transform);
        // transform.LookAt(Vector3(this.transform.position.x, currentNode.transform.position.y, this.transform.position.z));
        transform.LookAt(new Vector3(currentNode.transform.position.x, transform.position.y, currentNode.transform.position.z));
    }

}
