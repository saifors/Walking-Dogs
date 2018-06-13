using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {

    public float timeCounter;
    protected float fadeOutTimer;

    [Header("Event Booleans")]
    public bool dogHasFled;
    public bool dogsSent;
    public bool dogsCutsceneSet;
    public bool hasShownWhistle;
    public bool sendHasBeenEntered;
    public bool encounterInitialized;

    [Header("Tutorial panels")]
    public CanvasGroup walkCtr;
    public CanvasGroup whistleCtr;
    public CanvasGroup sendCtr;
    public CanvasGroup fleeCtr;
    public float fadeTime;
    public float stayTime;
    private bool showWalk;
    private bool showWhistle;
    private bool showSend;
    private bool showFlee;
    private bool walkFadeOut;
    private bool walkFadedIn;
    private bool whistleFadeOut;
    private bool whistleFadedIn;
    private bool sendFadeOut;
    private bool sendFadedIn;
    private bool fleeFadeOut;
    private bool fleeFadedIn;


    [Header("Other Objects")]
    private PlayerControllerFinal playerController;
    public DogAI1 dog1;
    public DogAI2 dog2;
    public DogAI2 dog3;
    public MonsterBehaviour monster;

    public GameObject sendLimit;

    // Use this for initialization
    void Start ()
    {
        walkCtr.alpha = 0;
        whistleCtr.alpha = 0;
        sendCtr.alpha = 0;
        fadeOutTimer = fadeTime;
        showWalk = true;

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerFinal>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ShowWalk();
        ShowWhistle();
        if (playerController.enterFlee && !dog1.startedFleeing)
        {
            Debug.Log("Dog Flees");
            dog1.SetCutscene();
            dog1.startedFleeing = true;
            showFlee = true;
        }
        ShowFlee();
        if(playerController.enterSend && !sendHasBeenEntered)
        {
            showSend = true;
            sendHasBeenEntered = true;
        }
        ShowSend();

        

        if(playerController.sendDogs && !dogsCutsceneSet)
        {
            dog2.SetCutscene();
            dog3.SetCutscene();
            dogsCutsceneSet = true;
            Debug.Log("Cutscenes have been set");
        }

        if(playerController.monsterEncounter && !encounterInitialized)
        {
            
            dog1.gameObject.SetActive(true);
            dog1.agent.speed = 8;            
            dog1.sound.Play(1, 100);
            
            dog1.agent.SetDestination(monster.transform.position);
            encounterInitialized = true;
        }

        if(monster.hasAttackedDog)
        {
            //dog McFucking dies
            dog1.gameObject.SetActive(false);
        }

        if(playerController.escapedHome)
        {
            //When You escape to to home;
            monster.gameObject.SetActive(false);
            
            SceneManager.LoadScene(4);

        }

        if(dogsCutsceneSet)
        {
            sendLimit.SetActive(false);
        }

        if(monster.encounterFinished)
        {

        }
    }

    void ShowWalk()
    {
        if (showWalk)
        {

            timeCounter += Time.deltaTime;
            if (!walkCtr.gameObject.activeInHierarchy) walkCtr.gameObject.SetActive(true);
            if (!walkFadedIn) walkCtr.alpha = timeCounter / fadeTime;
            if (walkCtr.alpha >= 1 && !walkFadedIn)
            {
                walkFadedIn = true;
                timeCounter = 0;
            }
            if (walkFadedIn && timeCounter >= stayTime)
            {
                walkFadeOut = true;
                timeCounter = 0;
            }
            if (walkFadeOut)
            {
                fadeOutTimer -= Time.deltaTime;
                walkCtr.alpha = fadeOutTimer / fadeTime;
                if (walkCtr.alpha <= 0)
                {
                    fadeOutTimer = fadeTime;
                    timeCounter = 0;
                    showWalk = false;
                    showWhistle = true;
                }
            }

        }
        else if (walkCtr.gameObject.activeInHierarchy) walkCtr.gameObject.SetActive(false);
    }

    void ShowWhistle()
    {
    if (showWhistle)
        {

            timeCounter += Time.deltaTime;
            if (!whistleCtr.gameObject.activeInHierarchy) whistleCtr.gameObject.SetActive(true);
            if (!whistleFadedIn) whistleCtr.alpha = timeCounter / fadeTime;
            if (whistleCtr.alpha >= 1 && !whistleFadedIn)
            {
                whistleFadedIn = true;
                timeCounter = 0;
            }
            if (whistleFadedIn && timeCounter >= stayTime)
            {
                whistleFadeOut = true;
                timeCounter = 0;
            }
            if (whistleFadeOut)
            {
                fadeOutTimer -= Time.deltaTime;
                whistleCtr.alpha = fadeOutTimer / fadeTime;
                if (whistleCtr.alpha <= 0)
                {
                    fadeOutTimer = fadeTime;
                    timeCounter = 0;
                    showWhistle = false;
                    
                }
            }

        }
        else if (whistleCtr.gameObject.activeInHierarchy) whistleCtr.gameObject.SetActive(false);
    }

    void ShowSend()
    {
        if (showSend)
        {

            timeCounter += Time.deltaTime;
            if (!sendCtr.gameObject.activeInHierarchy) sendCtr.gameObject.SetActive(true);
            if (!sendFadedIn) sendCtr.alpha = timeCounter / fadeTime;
            if (sendCtr.alpha >= 1 && !sendFadedIn)
            {
                sendFadedIn = true;
                timeCounter = 0;
            }
            if (sendFadedIn && timeCounter >= stayTime)
            {
                sendFadeOut = true;
                timeCounter = 0;
            }
            if (sendFadeOut)
            {
                fadeOutTimer -= Time.deltaTime;
                sendCtr.alpha = fadeOutTimer / fadeTime;
                if (walkCtr.alpha <= 0)
                {
                    fadeOutTimer = fadeTime;
                    timeCounter = 0;
                    showSend = false;
                    
                }
            }

        }
        else if (sendCtr.gameObject.activeInHierarchy) sendCtr.gameObject.SetActive(false);
    }

    void ShowFlee()
    {
        if (showFlee)
        {

            timeCounter += Time.deltaTime;
            if (!fleeCtr.gameObject.activeInHierarchy) fleeCtr.gameObject.SetActive(true);
            if (!fleeFadedIn) fleeCtr.alpha = timeCounter / fadeTime;
            if (fleeCtr.alpha >= 1 && !fleeFadedIn)
            {
                fleeFadedIn = true;
                timeCounter = 0;
            }
            if (fleeFadedIn && timeCounter >= stayTime)
            {
                fleeFadeOut = true;
                timeCounter = 0;
            }
            if (fleeFadeOut)
            {
                fadeOutTimer -= Time.deltaTime;
                fleeCtr.alpha = fadeOutTimer / fadeTime;
                if (fleeCtr.alpha <= 0)
                {
                    fadeOutTimer = fadeTime;
                    timeCounter = 0;
                    showFlee = false;
                    
                }
            }

        }
        else if (fleeCtr.gameObject.activeInHierarchy) fleeCtr.gameObject.SetActive(false);
    }
}
