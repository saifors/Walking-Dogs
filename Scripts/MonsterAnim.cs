using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnim : MonoBehaviour {

    public MonsterBehaviour monster;

    private void Start()
    {
        monster = GetComponentInParent<MonsterBehaviour>();
    }

    public void EndAttack()
    {
        monster.attackAnimFinished = true;
    }

    public void Hit()
    {
        monster.hasHit = true ;
    }

    public void FinishedTransform()
    {
        monster.finishedTransform = true;
    }
}
