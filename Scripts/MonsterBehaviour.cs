using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    public enum State {Idle, Chase, Attack, Cutscene};
    public State state;

    private NavMeshAgent agent;

    [Header("Target Properties")]
    public float radius;
    public LayerMask targetMask;
    public bool targetDetected;
    public Transform targetTransform;
    public Transform dogTransform;

    public PlayerControllerFinal playerController;

    public float attackDistance;
    public float attackMargin;

    public bool attackAnimFinished;
    public bool hitConfirmed;
    public bool attackDog;
    
    public bool hasAttackedDog;
    public bool encounterFinished;
    public bool hasTransformed;
    public bool transformInit;
    public bool finishedTransform;

    private float timeCounter;
    public float attackCooldown;
    public float idleTime;

    public int damage;

    public SoundPlayer sound;
    public GameObject dogModel;
    public GameObject monsterModel;

    public Animator monsterAnim;
    public Animator dogAnim;

    public bool hasHit;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        sound = GetComponent<SoundPlayer>();
        idleTime = 1;
        SetCutscene();
    }
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Chase:
                ChaseUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;
            case State.Cutscene:
                CutsceneUpdate();
                break;
            default:
                break;
        }
        
    }
    void FixedUpdate()
    {
        targetDetected = false;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (hitColliders.Length != 0)
        {
            //Target has been detected
            targetDetected = true;
            targetTransform = hitColliders[0].transform; //0 is basically guranteed to be the target in this case.
        }
    }

    void IdleUpdate()
    {
        if (timeCounter >= idleTime)
        {
            SetChase();
        }

        timeCounter += Time.deltaTime;
    }
    void ChaseUpdate()
    {
        if (Vector3.Distance(transform.position, targetTransform.position) <= attackDistance)
        {
            SetAttack();
            return;
        }
        if(timeCounter >= 1)
        {
            agent.speed += 0.1f;
            timeCounter = 0;
        }

        timeCounter += Time.deltaTime;
        agent.SetDestination(targetTransform.position);
    }
    void AttackUpdate()
    {

        if (hasHit && !hitConfirmed)
        {
            if ((Vector3.Distance(transform.position, targetTransform.position) <= attackDistance + attackMargin))
            {

                hitConfirmed = true;
                playerController.Damage(damage);
            }

        }
        else if(attackAnimFinished) hitConfirmed = true;
        
        if(attackAnimFinished && hitConfirmed)
        {   
            if(timeCounter >= attackCooldown)
            {
                SetChase();
                attackAnimFinished = false;
                hitConfirmed = false;
                hasHit = false;
                timeCounter = 0;
                
            }
            
        }
        if(timeCounter >= 8) //fail safe because sometimes the evnt for attack anim finished fucks up.
        {
            SetChase();
            attackAnimFinished = false;
            hitConfirmed = false;
            hasHit = false;
            timeCounter = 0;
        }
        timeCounter += Time.deltaTime;

    }
    void CutsceneUpdate()
    {
        

        if (hasTransformed && !transformInit) //tranform from dog to monster
        {
            
                dogModel.SetActive(false);
                monsterModel.SetActive(true);
                transformInit = true;
        }

        if (!attackDog && hasTransformed && finishedTransform)
        {
            monsterAnim.SetBool("walking", true);
            agent.SetDestination(dogTransform.position);
                
            
            if (Vector3.Distance(transform.position, dogTransform.position) <= attackDistance)
            {
                attackDog = true;
                
            }

            
        }
        
        if(attackDog && !hasAttackedDog)
        {
            //play attack animation
            monsterAnim.SetBool("walking", false);
            monsterAnim.SetBool("attack", true);
            timeCounter = 0;
            
            hasAttackedDog = true;
        }
        
        if(hasAttackedDog) timeCounter += Time.deltaTime;

        if (hasAttackedDog && timeCounter >= attackCooldown)
        {
            encounterFinished = true;
            SetIdle();
        }
    }

    void SetIdle()
    {
        monsterAnim.SetBool("walking", false);
        monsterAnim.SetBool("attack", false);
        agent.isStopped = true;
        agent.stoppingDistance = 0;
        radius = 5.0f;
        timeCounter = 0;
        attackAnimFinished = false;
        state = State.Idle;
    }

    void SetChase()
    {
        monsterAnim.SetBool("walking", true);
        monsterAnim.SetBool("attack", false);
        agent.isStopped = false;
        agent.stoppingDistance = 3;
        agent.speed = 8.5f;
        timeCounter = 0;
        radius = 50.0f;
        state = State.Chase;
    }

    void SetAttack()
    {
        monsterAnim.SetBool("walking", false);
        monsterAnim.SetBool("attack", true);
        agent.isStopped = true;
        attackAnimFinished = false;
        timeCounter = 0;
        hitConfirmed = false;
        state = State.Attack;
    }

    void SetCutscene()
    {
        agent.isStopped = false;
        timeCounter = 0;
        state = State.Cutscene;
    }

    

    public void ActivateTransform()
    {
        dogAnim.SetBool("startTransform", true);
    }
}
