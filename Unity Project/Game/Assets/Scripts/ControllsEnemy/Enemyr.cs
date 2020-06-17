using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemyr : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent myNavMesh;

    public Transform playerTransform;
    public float checkRate = 0.01f;
    
    float nextCheck;
    void Start()
    {
        
            

        myNavMesh = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            FollowPlayer();
        }
            
    }
    void FollowPlayer()
    {
        myNavMesh.transform.LookAt(playerTransform);
        myNavMesh.destination = playerTransform.position;
    }
}
