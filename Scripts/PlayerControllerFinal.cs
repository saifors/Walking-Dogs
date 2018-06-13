using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControllerFinal : MonoBehaviour
{

    private Vector2 axis;
    private CharacterController controller;

    private Vector3 moveDirection;
    public float speed;
    public float standardSpeed;
    public float slowSpeed;
    public float runSpeed;
    private float forceToGround = Physics.gravity.y;

    private bool jump;
    public float jumpSpeed; //Not force like the platformer because thatr one was with Rigidbody unlike this one.

    public float gravityMagnitude = 1;

    public int life;
    public int maxLife;

    public float timeCounter;
    public float healTime;

    public float radius;
    public LayerMask targetMask;

    public SoundPlayer sound;
    
    //Cutscene triggers.
    public bool enterFlee;
    public bool enterSend;
    public bool canEscape;
    public bool escapedHome;
    public bool sendDogs;
    public bool monsterEncounter;

    public float dogRadius;

    public CutsceneManager cutscene;
    public MonsterBehaviour monster;

    private Vector3 currentFramePosition;
    private Vector3 lastFramePosition;

    public bool isMoving;
    public float stepCounter;

    public bool isDamaged;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        maxLife = 100;
        life = maxLife;
        standardSpeed = speed;
        currentFramePosition = transform.position;
        lastFramePosition = currentFramePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded && !jump)
        {
            moveDirection.y = forceToGround;
        }
        else
        {
            jump = false;
            moveDirection.y += Physics.gravity.y * gravityMagnitude * Time.deltaTime;
        }

        Vector3 transformDirection = axis.x * transform.right + axis.y * transform.forward; //Tranforms movement in the world axis to local axis.

        //moveDirection = new Vector3(axis.x, 0, axis.y);
        moveDirection.x = transformDirection.x * speed;
        moveDirection.z = transformDirection.z * speed;


        controller.Move(moveDirection * Time.deltaTime);

        if(timeCounter >= healTime)
        {
            RecoverLife(1);
            timeCounter = 0;
        }

        timeCounter += Time.deltaTime;

        if (cutscene.sendHasBeenEntered && !cutscene.dogsSent)
        {
            speed = slowSpeed;
        }
        if(monsterEncounter && !cutscene.encounterInitialized)
        {
            speed = runSpeed;
        }

        lastFramePosition = currentFramePosition;
        currentFramePosition = transform.position;

        if (currentFramePosition != lastFramePosition)
        {
            isMoving = true;       
        }
        else
        {
            isMoving = false;
        }

        if(isMoving)
        {
            if(stepCounter >= sound.clips[0].length)
            {
                sound.Play(3 ,100);
                stepCounter = 0;
            }
            
        }
        stepCounter += Time.deltaTime;
    }
    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
    }
    public void StartJump()
    {
        if(!controller.isGrounded) return;

        jump = true;
        moveDirection.y = jumpSpeed;
    }
    
    public void Damage(int dmg)
    {
        life -= dmg;
        isDamaged = true;
        if(life <= 0)
        {
            Die();
        }
    }

    public void RecoverLife(int heal)
    {        
        life += heal;
        if(life >= 100)
        {
            life = 100;
        }
    }

    public void Die()
    {
        //go to gulag
        SceneManager.LoadScene(3);
    }

    public void Whistle()
    {
        //play whistle audio
        sound.Play(Random.Range(0,2), 100);

        //Make dogs change to following State if patrolling?
        Collider[] dogColliders = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (dogColliders.Length != 0)
        {
            for(int i = 0; i <= dogColliders.Length - 1; i++)
            {
                DogsBehaviour doggo = dogColliders[i].gameObject.GetComponentInParent<DogsBehaviour>();
                
                if (!doggo.isInCutscene) doggo.SetFollowing(); // If dog is in cutscene it won´t listen to whistle.
                
            }
            
        }
        
    }

    public void SendDogs()
    {
        //Order sound
        sound.Play(2, 100);
        speed = standardSpeed;
        sendDogs = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "FleeTrigger")
        {
            enterFlee = true;
        }
        else if(col.gameObject.name == "SendTrigger")
        {
            Debug.Log("entered Send");
            enterSend = true;
        }
        else if(col.gameObject.name == "EncounterTrigger" && !monsterEncounter)
        {
            canEscape = true;
            monsterEncounter = true;
            monster.ActivateTransform();
            monster.sound.Play(0, 100);
        }
        else if(col.gameObject.name == "EscapeTrigger" && canEscape)
        {
            escapedHome = true;
        }
    }

    private void OnDrawGizmos()
    {
        Color c = Color.red;
        c.a = 0.3f;
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, dogRadius);
    }
}
