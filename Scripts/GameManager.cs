using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public bool gamePaused;
    private UIScript ui;
    private InputManager input;

	// Use this for initialization
	void Start ()
    {
		ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
        input = GetComponent<InputManager>();
    }

    public void Resume()
    {
        input.mouse.Hide();
        gamePaused = false;
        Time.timeScale = 1;
        ui.ClosePausePanel();
    }
    public void Pause()
    {
        input.mouse.Show();
        gamePaused = true;
        Time.timeScale = 0;
        ui.OpenPausePanel();
    }

    public void LoadScene(int numScene)
    {
        SceneManager.LoadScene(numScene);
    }
}
