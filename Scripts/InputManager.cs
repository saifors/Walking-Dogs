using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControllerFinal playerController;
    private LookRotation lookRotation;
    

    private float sensitivity = 3.0f;

    public MouseCursor mouse;

    public CutsceneManager cutscene;
    public GameManager gameManager;

    

    // Use this for initialization
    void Start ()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerFinal>();
        lookRotation = playerController.GetComponent<LookRotation>(); //It remembers that player controller is that object so it gets a diferent component from the same object.

        mouse = new MouseCursor();
        mouse.Hide();
        gameManager = GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameManager.gamePaused) gameManager.Resume();
            else gameManager.Pause();
            
        }

        if (gameManager.gamePaused) return;

        //Player Movement
        Vector2 inputAxis = Vector2.zero;
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
        playerController.SetAxis(inputAxis);
        //Player Jump
        if(Input.GetButton("Jump")) playerController.StartJump();

        //Read Mouse axis
        Vector2 mouseAxis = Vector2.zero;
        mouseAxis.x = Input.GetAxis("Mouse X") * sensitivity;
        mouseAxis.y = Input.GetAxis("Mouse Y") * sensitivity;
        lookRotation.SetMouseAxis(mouseAxis);

        

        //Shooting
        //if(Input.GetButton("Fire1")) playerController.TryShot();
        //if(Input.GetKeyDown(KeyCode.R)) playerController.TryReload();

        if(Input.GetMouseButtonDown(0)) mouse.Hide();
        if(Input.GetKeyDown(KeyCode.Escape)) mouse.Show(); //Without this you're legit fucked.
        //if(Input.GetKeyDown(KeyCode.B)) playerController.Damage(10);

        if(Input.GetKeyDown(KeyCode.X)) playerController.Whistle();
        if (cutscene.sendHasBeenEntered && !cutscene.dogsSent)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                playerController.SendDogs();
                cutscene.dogsSent = true;
                Debug.Log("Sent");
            }
        }

    }
}
