using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> availableNodes;

    public bool isIntersection = false;
    public string intersectionType = "";

    public bool hasTrafficLight = false;
    public SemaforoController semaforoController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3 GetNodePosition(float yPosition){
        return new Vector3(
            this.gameObject.transform.position.x,
            yPosition,
            this.gameObject.transform.position.z
        );
    }

    public Node GetRandomNode(){
        return availableNodes[Random.Range(0, availableNodes.Count)];
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
