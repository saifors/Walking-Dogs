using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogsBehaviour : MonoBehaviour {

    public enum State { Idle, Following, Patrol, Cutscene };
    public State state;

    public NavMeshAgent agent;

    private float timeCounter;
    public float idleTime = 1.0f;
    public float followTime;

    [Header("Target Properties")]
    public float radius;
    public LayerMask targetMask;
    public bool targetDetected;
    public Transform targetTransform;

    [Header("Path Properties")]
    public Transform[] nodes;
    public int currentNode;
    public float minDistance = 0.5f;
    public bool stopAtEachNode = false;
    public float maxDist = 5;
    public Transform[] sceneNode;
    public GameObject nodesParent;

    public bool patrolling;
    private bool targetReached;

    protected int currentSNode = 1;

    public SoundPlayer sound;
    protected float barkCounter;
    protected float barkInterval;

    public bool isInCutscene;

    private PlayerControllerFinal playerController;
    private Transform playerTransform;

    public Animator anim;

    // Use this for initialization
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetIdle();

        nodes = nodesParent.GetComponentsInChildren<Transform>();
        sound = GetComponent<SoundPlayer>();
        barkInterval = Random.Range(2.5f, 10.75f);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerFinal>();
        playerTransform = playerController.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Following:
                FollowingUpdate();
                break;
            case State.Patrol:
                PatrolUpdate();
                break;
            case State.Cutscene:
                CutsceneUpdate();
                break;
            default:
                break;
        }
        barkCounter += Time.deltaTime;
        if(barkCounter >= barkInterval)
        {
            barkCounter = 0;
            barkInterval = Random.Range(3.5f, 10.75f);
            sound.Play(0, Random.Range(70, 90));
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
            SetPatrol();
        }
        else timeCounter += Time.deltaTime;
    }

    void FollowingUpdate()
    {
        if(timeCounter >= followTime)
        {
            SetIdle();
        }
        else
        {  
            agent.SetDestination(targetTransform.position);
        }
        timeCounter += Time.deltaTime;
    }

    void PatrolUpdate()
    {
        
        if(!patrolling) GoToANearNode();

        //if (stopAtEachNode) SetIdle();
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 2) SetIdle();
        //if(agent.isStopped) SetIdle();
        //Debug.Log(Vector3.Distance(transform.position, nodes[currentNode].position));
        
    }

    protected virtual void CutsceneUpdate()
    {

    }

    void SetIdle()
    {
        timeCounter = 0;
        agent.isStopped = true;
        agent.stoppingDistance = 0;
        radius = 5.0f;
        anim.SetBool("walking", false);
        patrolling = false;

        state = State.Idle;
    }

    public void SetFollowing()
    {
        timeCounter = 0;
        agent.isStopped = false;
        agent.stoppingDistance = 4;
        radius = 10.0f;
        followTime = Random.Range(5.0f,7.0f);
        anim.SetBool("walking", true);
        patrolling = false;

        state = State.Following;
    }

    void SetPatrol()
    {
        timeCounter = 0;
        agent.isStopped = false;
        agent.stoppingDistance = 4;
        anim.SetBool("walking", true);
        state = State.Patrol;
    }

    public void SetCutscene()
    {
        timeCounter = 0;
        agent.isStopped = false;
        agent.stoppingDistance = 1;

        patrolling = false;

        isInCutscene = true;
        anim.SetBool("walking", false);
        state = State.Cutscene;
    }

    void GoToANearNode()
    {
        
        int newNode = Random.Range(1, nodes.Length);
        if ((newNode != currentNode) && (Vector3.Distance(nodes[newNode].position, playerTransform.position) <= playerController.dogRadius))
        {
            currentNode = newNode;
            patrolling = true;
        }
        
        else return;

        GoToNode(nodes[currentNode]);
        
    }

    protected virtual void GoToNode(Transform n)
    {
        agent.SetDestination(n.position);
    }

    public void GoToSceneNode(int sN)
    {
        agent.SetDestination(sceneNode[sN].position);
        
    }

    protected virtual void GoToNextNode()
    {
        
        currentSNode++;
        if (currentSNode >= nodes.Length) targetReached = true;
        

        agent.SetDestination(sceneNode[currentSNode].position);
    }
}
