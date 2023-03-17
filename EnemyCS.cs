using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//로티
public class EnemyCS : MonoBehaviour
{
    [SerializeField] float Radi;
    [SerializeField] GameObject Player;
    //int PlayerLayerMask;
    int PlayerMask,WallMask;
    NavMeshAgent NavMeshAgent;
    NavMeshPath NavMeshPath;

    Animator _Animator;

    bool FoundPlayer;
    bool RunAwaying = true;

    bool FirstRunAway;
    float RandA = 5;
    float RandB = 8;
    
    
    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.Find("Player");
          NavMeshPath = new NavMeshPath();
         _Animator = GetComponent<Animator>();
        _Animator.SetFloat("RanPar",Random.Range(0,1f));
          PlayerMask = 1  << LayerMask.NameToLayer("Player");  
         WallMask = 1  << LayerMask.NameToLayer("Wall");  
        InvokeRepeating(nameof(SetNavMesh),0f,4f);
       NavMeshAgent = GetComponent<NavMeshAgent>();
      
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        SetAnimation();

        AmIDead();
    }
    void AmIDead(){
        if(_Animator.GetCurrentAnimatorStateInfo(0).IsName("ImDead")){
            Invoke(nameof(Killme),0.3f);
        }
    }

    void Killme(){

        GameObject lvText = GetComponent<FloatingLv>().ThisLvObj;
            if(GetComponent<FloatingLv>().EnemyMarkObj!=null)
                Destroy(GetComponent<FloatingLv>().EnemyMarkObj);
            
        Destroy(lvText); 
        Destroy(gameObject);
    }

    void FindPlayer(){
        Collider[] Col =  Physics.OverlapSphere(transform.position,Radi,PlayerMask);
       for(int i = 0;i<Col.Length;i++){
        if(Col[0].tag=="Player"){ //col이 비어있지 않고 플레이어를 찾으면
            if(!Physics.Raycast(transform.position,Player.transform.position-transform.position,Radi,WallMask)){
                CancelInvoke(nameof(SetNavMesh));
                NavMeshAgent.SetDestination(Player.transform.position);
                Radi =20;
                if(!FoundPlayer)gameObject.GetComponent<FloatingLv>().EncounterPlayer();
                FoundPlayer = true; //찾았다 이녀석
                
                }
            }
        }
        if(FoundPlayer&&Col.Length==0){ // 플레이어를 한번 찾았는데 놓쳤어 = col.length ==0?
            if(FoundPlayer)gameObject.GetComponent<FloatingLv>().MissingPlayer();
            FoundPlayer = false;
            FirstRunAway =true;
            Radi =10;
            //gameObject.GetComponent<FloatingLv>().EncounterPlayer();
            InvokeRepeating(nameof(SetNavMesh),2f,4f);
        }
        

        
        
        if(FoundPlayer&&GetComponent<FloatingLv>().PlayerLV>GetComponent<FloatingLv>().ThisObjectLV){ // 찾아논 플레이어가 갑자기 나보다 레벨이 높아져 그럼 도망가
           FirstRunAway =false;
            NavMeshAgent.SetDestination(GetRunAwayVec());
            _Animator.SetTrigger("Run Away");
            RunAwaying =true;
        }

        
    
    }

    Vector3 GetRunAwayVec(){

        //Vector3 originVec = NavMeshAgent.velocity;
        
        //NavMeshAgent.velocity = Vector3.zero;

        Vector3 TowardPlayer = transform.position - Player.transform.position;
        Vector3 RunAwayVec;
        NavMeshHit Hit;
        RunAwayVec = TowardPlayer*Random.Range(4,RandB);
        if(NavMesh.SamplePosition(RunAwayVec,out Hit,400f,NavMesh.AllAreas)){
            //NavMeshAgent.velocity = originVec;
            if((transform.position-RunAwayVec).magnitude<0.2f)
                GetRunAwayVec();
            return RunAwayVec;
        }
        else GetRunAwayVec();

        return RunAwayVec;
    }
    
    void SetAnimation(){
        if(NavMeshAgent.velocity.magnitude>0.01f){
        _Animator.SetBool("Walking",true);
       }
       else{
        _Animator.SetBool("Walking",false);
       }
    }

    void SetNavMesh(){
        NavMeshHit Hit;
        if(NavMesh.SamplePosition(new Vector3(Random.Range(-30f,50f),0,Random.Range(-30f,50f)),out Hit,200f,NavMesh.AllAreas)){
            NavMeshAgent.SetDestination(Hit.position);
        }
        else
            SetNavMesh();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,Radi);
        if(FoundPlayer)    //dont bother me at the prefeb menu;;;;;;;
             Gizmos.DrawRay(transform.position,Player.transform.position);
    }

    private void OnTriggerStay(Collider other) { 
        if(other.tag =="Player"){
            _Animator.SetTrigger("Attack");
        }
    }

    private void OnDestroy() {
        GameObject _uimanager,_uimanager2;
        UIManager _ui;

        _uimanager = GameObject.FindGameObjectWithTag("UIManager");
        _uimanager2 = GameObject.FindGameObjectWithTag("UIManager");
        _ui = _uimanager.gameObject.GetComponent<UIManager>();
        _ui._enemyCount++;
            _ui._enemyCountText.text = _ui._enemyCount.ToString() + " / " + _ui.EnemyCountMax.ToString();
    }
}
