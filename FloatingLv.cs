using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//로티
public class FloatingLv : MonoBehaviour
{

    Vector3 LVPosition;
    Vector3 MarkPosition;

    public GameObject ThisLvObj,LVCanvas;

    public float ThisObjectLV;
    [SerializeField] float FloatingY;
    [SerializeField] GameObject LVText;
    [SerializeField] GameObject MarkText;
    float amount=0;

    public GameObject EnemyMarkObj,EnemyMissingObj;
    public float PlayerLV;
    [SerializeField] float TempLV=0;
    float index=1;

    Vector3 temp;
    void Awake()
    {
        LVCanvas =GameObject.Find("LVCanvas");
        LVPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position+new Vector3(0,0,FloatingY));
        ThisLvObj = Instantiate(LVText,LVPosition,Quaternion.identity,LVCanvas.transform);
    }
    // Start is called before the first frame update
    void Start()
    {

         temp=Camera.main.gameObject.GetComponent<CameraFollow>()._minValues;
    }

 float d=1;
     // Update is called once per frame
    void Update()
    {
        PlayerLV= GameObject.Find("Player").GetComponent<FloatingLv>().ThisObjectLV;

        LvFloating();
         LvColoring();
          LvBigger();
        BigCam();

        LvChangeCheck();
       
        if(PlayerLV>200){
            amount = (PlayerLV-200)*1.4f;
        }
       
    }
    void LvChangeCheck(){
        if(TempLV>270) return;

        if(gameObject.tag =="Player")
            if(TempLV <= PlayerLV)
            { 
                if(PlayerLV>70){
                    GetComponent<WeaponChangeByLV>()._check =true;
                    TempLV = 70+(index*100);
                    index++; 
                }
            }
    }
    [SerializeField] float speed;
    void BigCam(){
        
        float CamMax=temp.y+amount;
        if(PlayerLV>200){
            if(Camera.main!=null){
             Camera.main.gameObject.GetComponent<CameraFollow>()._maxVaiue.y =  CamMax; 
             Camera.main.gameObject.GetComponent<CameraFollow>()._minValues.y =Mathf.MoveTowards(temp.y,CamMax,Time.deltaTime);
            }
        }
    }

    void LvBigger(){
        float BigSize;
        if(ThisObjectLV/130>4){
            BigSize = 4;
        }else
            BigSize = ThisObjectLV/130;
        gameObject.transform.localScale = new Vector3(1+BigSize,1+BigSize,1+BigSize);
    }

    void LvFloating(){
        if(Camera.main!=null){
        LVPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position+new Vector3(0,0,FloatingY));

        MarkPosition =  Camera.main.WorldToScreenPoint(gameObject.transform.position+new Vector3(0,0,2f));
        }
        if(ThisLvObj !=null){
            ThisLvObj.transform.position = LVPosition;
            ThisLvObj.GetComponent<Text>().text = "LV."+ThisObjectLV.ToString();
            
        }

        if(EnemyMarkObj !=null){
            EnemyMarkObj.transform.position = MarkPosition;
        }
        if(EnemyMissingObj !=null){
            EnemyMissingObj.transform.position = MarkPosition;
        }


    }

    void LvColoring(){
        if(gameObject.tag =="Player")
            ThisLvObj.GetComponent<Text>().color = Color.white;

        else if(PlayerLV<ThisObjectLV){
            ThisLvObj.GetComponent<Text>().color = Color.red;
        }

        else if(PlayerLV>ThisObjectLV){
            ThisLvObj.GetComponent<Text>().color = Color.blue;
        }
    }

    public void EncounterPlayer(){
        EnemyMarkObj = Instantiate(MarkText,LVPosition,Quaternion.identity,LVCanvas.transform);
        // EnemyMarkObj.GetComponent<Text>().color = new Color(255,255,255,255);


        EnemyMarkObj.GetComponent<Text>().text = "!";
        
        Invoke(nameof(DelMark),2f);
    }

    public void MissingPlayer(){
        EnemyMissingObj = Instantiate(MarkText,MarkPosition,Quaternion.identity,LVCanvas.transform);
        //EnemyMarkObj.GetComponent<Text>().color = Color.red;
        EnemyMissingObj.GetComponent<Text>().text = "?";
        Invoke(nameof(DelMark),2f);
    }

    void DelMark(){
        if(EnemyMarkObj!=null) Destroy(EnemyMarkObj);
         if(EnemyMissingObj!=null) Destroy(EnemyMissingObj);
    }
}
