using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//로티
public class MoveToPath : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    LineRenderer lr;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        
        lr = this.GetComponent<LineRenderer>();
        lr.startWidth = lr.endWidth = 0.27f;
       
        lr.enabled = false;
    }

    public void makePath()
    {
        lr.enabled = true;
        StartCoroutine(makePathCoroutine());
    }

    void drawPath()
    {
        int length = agent.path.corners.Length;

        lr.positionCount = length;
        for (int i = 1; i < length; i++)
            lr.SetPosition(i, agent.path.corners[i]);

        
    }

    IEnumerator makePathCoroutine()
    {
        agent.SetDestination(target.transform.position);
        lr.SetPosition(0, this.transform.position);

        
        while (Vector3.Distance(this.transform.position, target.transform.position) > 0.1f)
        {
            if(lr.enabled)
                lr.SetPosition(0, this.transform.position);
            
            drawPath();

            yield return null;
        }

        lr.enabled = false;
    }

    public void DelPath(){
        target.GetComponent<Target>().Setinvisible();
        agent.path.ClearCorners();
        agent.isStopped=true;
        lr.enabled = false;
    }
}