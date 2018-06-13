using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDogAnim : MonoBehaviour {

    public MonsterBehaviour monster;

    private void Start()
    {
        monster = GetComponentInParent<MonsterBehaviour>();
    }

    public void TransDog()
    {
        monster.hasTransformed = true;
    }
}
