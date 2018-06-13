using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCutscene : MonoBehaviour
{
    public float timeCounter;
    public float monsterTime;

    public MonsterHouse monster;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeCounter += Time.deltaTime;
        if(timeCounter >= monsterTime)
        {
            monster.gameObject.SetActive(true);
        }
	}
}
