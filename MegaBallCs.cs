using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//로티
public class MegaBallCs : MonoBehaviour
{

    public float BallLv = 100;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "+"+BallLv.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
