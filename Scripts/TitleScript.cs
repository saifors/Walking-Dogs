using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{

    public GameObject titlePanel;
    public GameObject creditsPanel;
    public SoundPlayer sound;
    public CanvasGroup blackFade;

    private bool musicPlaying;
    public float timeCounter;
    private MouseCursor mouse;

    private void Start()
    {
        blackFade.alpha = 1;
        sound.is2D = true;
        mouse = new MouseCursor();
        mouse.Show();
    }

    private void Update()
    {
        if (blackFade.alpha > 0) blackFade.alpha -= Time.deltaTime;
        else blackFade.gameObject.SetActive(false);
        if(!musicPlaying)
        {
            sound.Play(0, 100);
            timeCounter = 0;
            musicPlaying = true;
        }
        if (musicPlaying) timeCounter += Time.deltaTime;
        if (timeCounter >= sound.clips[0].length) musicPlaying = false;
        
    }

    public void StartGame()
    {
        Debug.Log("Start game");
        SceneManager.LoadScene(2); 
    }
    public void LoadScene(int numScene)
    {
        SceneManager.LoadScene(numScene);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void ShowCredits()
    {
        titlePanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void RemoveCredits()
    {
        creditsPanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    public void PlaySoundSelect()
    {
        sound.Play(1, 80);
    }
}
