using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//로티
public class RotateAsPlayrt : MonoBehaviour
{
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(-Player.transform.position);
    }
}
