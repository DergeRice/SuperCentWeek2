using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//로티
public class Target : MonoBehaviour
{
    RaycastHit hit;

    int GroundMask;
    public GameObject player;

    private void Start() {
        GroundMask = 1  << LayerMask.NameToLayer("Ground");  
       Setinvisible();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.GetComponent<MoveToPath>().agent.isStopped=false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);
             transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity,GroundMask))
            {
                this.transform.position = new Vector3(hit.point.x,0,hit.point.z);
                player.GetComponent<MoveToPath>().makePath();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            
        }

        if((transform.position-player.transform.position).magnitude<0.2f){
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void Setinvisible(){
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
    }
}