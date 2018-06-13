using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterHouse : MonoBehaviour {

    private NavMeshAgent agent;
    private Transform playerTransform;
    private PlayerControllerFinal playerController;
    public float attackDistance;
    public float timeCounter;
    public float impendingDoom;

    public Animator anim;

    public bool attackAnimFinished;


    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerController = playerTransform.GetComponent<PlayerControllerFinal>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        agent.SetDestination(playerTransform.position);
        if(Vector3.Distance(transform.position, playerTransform.position) <= attackDistance)
        {
            //Play monster Animation and when finished
            anim.SetBool("attack", true);
            
            if(attackAnimFinished)
            {
                playerController.Die();
            }
            
        }
	}

    public void EndAttack()
    {
        attackAnimFinished = true;
    }
}
