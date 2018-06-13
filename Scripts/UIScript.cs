using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public RectTransform lifeBarTrans;
    public PlayerControllerFinal playerController;
    public int maxLifebarSize;
    public float lifebarSize;

    public GameObject SendDogsPanel;
    public GameObject WalkControlsPanel;
    public GameObject WhistleControlsPanel;
    public GameObject pausePanel;

    public Animator anim;

    // Use this for initialization
    void Start ()
    {
        maxLifebarSize = 175;
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifebarSize = (maxLifebarSize * playerController.life) / 100;
        lifeBarTrans.sizeDelta = new Vector2(lifebarSize, 16.5f);

        if(playerController.isDamaged)
        {
            //anim.SetBool("playDmg", true);
            anim.Play("efectdam");
            playerController.isDamaged = false;
        }
	}

    public void ClosePausePanel()
    {
        pausePanel.SetActive(false);
    }
    public void OpenPausePanel()
    {
        pausePanel.SetActive(true);
    }
}
