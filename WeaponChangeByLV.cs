using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//로티
public class WeaponChangeByLV : MonoBehaviour
{
    float Lv;
    public GameObject[] WeaPon = new GameObject[4];

    public bool _check = true;
    void Start()
    {
        Lv =gameObject.GetComponent<FloatingLv>().ThisObjectLV;
        SetAllDisable();
    }

    // Update is called once per frame
    void Update()
    {
        Lv =gameObject.GetComponent<FloatingLv>().ThisObjectLV;

       
        if(Lv>=270)
        {
             if (_check == true)
            {

                SetAllDisable(); WeaPon[3].SetActive(true);
                GameObject pre = Resources.Load("Prefabs/Particles/Light") as GameObject;
                pre.transform.position = Vector3.zero;
                Instantiate(pre,WeaPon[3].transform.parent);

                _check = false;
            }
        }
        else if(Lv>=170)
        {
             if (_check == true)
            {
            
                SetAllDisable(); WeaPon[2].SetActive(true);
                GameObject pre = Resources.Load("Prefabs/Particles/Light") as GameObject;
                pre.transform.position = Vector3.zero;
                Instantiate(pre,WeaPon[2].transform.parent);

                _check = false;
            }
        }
        else if(Lv>=70)
        {
             if (_check == true)
            {
                SetAllDisable(); WeaPon[1].SetActive(true);
                GameObject pre = Resources.Load("Prefabs/Particles/Light") as GameObject;
                pre.transform.position = Vector3.zero;
                Instantiate(pre,WeaPon[1].transform.parent);

                _check = false;
            }
        }
        else if(Lv<70)
        {
            SetAllDisable(); WeaPon[0].SetActive(true);
        }
    
        
    }

    void SetAllDisable(){
        for(int i= 0; i<WeaPon.Length;i++){
            WeaPon[i].SetActive(false);
        }
    }
}
