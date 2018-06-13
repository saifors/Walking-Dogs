using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonHouseAnim : MonoBehaviour
{

    public MonsterHouse monster;

	// Use this for initialization
	void Start ()
    {
        monster = GetComponentInParent<MonsterHouse>();
    }

    public void EndAttack()
    {
        monster.EndAttack();
    }
}
