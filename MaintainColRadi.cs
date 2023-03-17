using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MaintainColRadi : MonoBehaviour
{

    NavMeshAgent Agent;
    CapsuleCollider Collider;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<NavMeshAgent>()!=null){
            Agent = GetComponent<NavMeshAgent>();
        }
        if(GetComponent<CapsuleCollider>()!=null){
            Collider = GetComponent<CapsuleCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
       Collider.radius = Agent.radius;
    }
}
