using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyCtrl : MonoBehaviour {
    public float stopDistance = 0.5f;
    public float kagoHeight = 3.0f;
    public float g=1.6f;
    public float jumpPower = 10.0f;

    [SerializeField] Player player;
    [SerializeField] GameObject kago;
    [SerializeField] BallSpawner spawner;

    enum State
    {
        walk, 
        Throw, 
        attack, 
        jump
    }

    GameObject moveTarget;
    NavMeshAgent agent;
    Animator animator;
    State state;
    State nextState;

    float time;
    int score = 0;
    int throwCount;
    float jumpStartYpos;
    bool isJump = false;
    bool isOnGround = false;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -g, 0);
        moveTarget = new GameObject();
        moveTarget.transform.position = transform.position + transform.forward * 10;

        state = State.walk;
        nextState = State.walk;
        agent.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
       
        if (isOnGround) agent.enabled = true;
        if (state == State.walk) { Walk(); }
        else if (state == State.Throw) { Throw(); }
        else if (state == State.attack) { Attak(); }
        else if (state == State.jump) { Jump(); }

        if(nextState != state){
            state = nextState;
        }
	}

    void Walk()
    {
        if (isOnGround) { 
            Debug.Log("State == walk isOnGroundisOnGround");
        time += Time.deltaTime;
        Vector3 moveTargetPosition = new Vector3(moveTarget.transform.position.x, transform.position.y, moveTarget.transform.position.z);
        Vector3 enemyPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Vector3.Distance(enemyPosition, moveTargetPosition) < stopDistance || time>5.0f)
        {
            moveTarget.transform.position = new Vector3(Random.Range(transform.position.x - 5.0f, transform.position.x + 5.0f), transform.position.y, Random.Range(transform.position.z - 5.0f, transform.position.z + 5.0f));
            time = 0;
            agent.isStopped = true;
            //agent.speed = 0;
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            animator.SetBool("walk", false);
            DecideNextState();
            
        }
        else
        {
            Debug.Log("walk");
            agent.isStopped = false;
            //agent.speed = 3.5f;
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            agent.SetDestination(moveTarget.transform.position);
            animator.SetBool("walk", true);
            transform.LookAt(new Vector3(moveTarget.transform.position.x, transform.position.y, moveTarget.transform.position.z));
        }
        }
    }

    void Throw(){
        agent.isStopped = true;
        if(throwCount<100){
            Debug.Log("throw");
            throwCount++;
            Debug.Log("throw throwCount =" +throwCount);
            animator.SetBool("throw", true);
            //lookAt kago
            transform.LookAt(kago.transform);
            //発射
            spawner.EnemyShot(transform);
        }
        else{
            animator.SetBool("throw", false);
            throwCount = 0;
            DecideNextState();
        }
    }

    void Attak(){
        agent.isStopped = true;
        Debug.Log("attack");
        animator.SetBool("attack", true);
        //lookAt player
        transform.LookAt(player.transform);
        //発射
        spawner.EnemyShot(transform);
       
    }

    public void EndAttack(){
        animator.SetBool("attack", false);
        DecideNextState();
    }

    void Jump()
    {
        Debug.Log("jump");

        if (isOnGround && !isJump && state==State.jump)
            {
                jumpStartYpos = transform.position.y;
                agent.enabled = false;
                animator.SetBool("jump", true);
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                isJump = true;
                isOnGround = false;
                //lookat kago
                transform.LookAt(kago.transform);

            }
            else if (transform.position.y - jumpStartYpos > kagoHeight)
            {
                animator.SetBool("jumpShoot", true);
                //lookat kago
                transform.LookAt(kago.transform);
                //発射
                spawner.EnemyShot(transform);
            }
            else
            {
                animator.SetBool("jumpShoot", false);
                //lookat kago
                transform.LookAt(kago.transform);
            }
    }
    void JumpEnd(){
        isJump = false;
        agent.enabled = true;
        animator.SetBool("jump", false);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        nextState = State.walk;
        state = State.walk;
    }

    void DecideNextState()
    {
        //移動して新しいポジションについたら
        if (state == State.walk) { 
            //敵のscoreがプレーヤーに負けてる、もしくはプレーヤがjump中なら
            if (score<player.GetPlayerScore() /*|| player.isJump*/)
            {
                //2/3の確率で攻撃
                int r = Random.Range(0, 3);
                if (r >= 0 && r <= 1)
                {
                    nextState = State.attack;
                }
                 else
                {
                    //残り1/3の確率はthrow or jump
                    r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        nextState = State.Throw;
                    }
                    else if (r == 1)
                    {
                        nextState = State.jump;
                    }
                }
            }
            //敵のscoreがプレーヤーに勝ってるかつ、プレーヤがjump中でないなら
            else{
                //throw or jump
                int r = Random.Range(0, 5);
                if(r>=0 && r<=2){
                    nextState = State.Throw;
                }
                else if(r>=3 && r<=4){
                    nextState = State.jump;
                }
            }

        }
        //throw or attack or jumpが終わったら
        else{
            //新しい場所に移動
            nextState = State.walk;
        }

    }

    public void EnemyScoreUp(){
        score += 10;
    }

    public void attackAnimEnd(){
        animator.SetBool("attack", false);
        DecideNextState();
    }


	private void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.tag=="moon"){
            
            isOnGround = true;
            agent.enabled = true;
            if(isJump){
                JumpEnd();
                Debug.Log("jumpend");
            }
        }
	}
}
