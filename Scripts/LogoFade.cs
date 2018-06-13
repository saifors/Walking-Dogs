using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoFade : MonoBehaviour {

    public CanvasGroup logoImage;
    public CanvasGroup blackFade;
    public bool logoVisible;
    public bool logoEnded;
    public float fadeTime;
    public float timeCounter;

    // Use this for initialization
    void Start()
    {
        logoImage.alpha = 0;
        blackFade.alpha = 0;
        fadeTime = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!logoVisible && !logoEnded)
        {
            logoImage.alpha += fadeTime * Time.deltaTime;
            if(logoImage.alpha >= 1)
            {
                logoImage.alpha = 1;
                logoVisible = true;
            }
        }
        if(logoVisible)
        {
            timeCounter += Time.deltaTime;
        }
        if(timeCounter >= 2)
        {           
            if(logoImage.alpha <= 0)
            {
                logoImage.alpha = 0;
                logoVisible = false;
                logoEnded = true;
                timeCounter = 0;
                
            }
            else logoImage.alpha -= fadeTime * Time.deltaTime;
        }
        if(logoEnded)
        {
            if(blackFade.alpha >= 1) SceneManager.LoadScene(1);
            else blackFade.alpha += fadeTime * Time.deltaTime;
        }
    }
}
