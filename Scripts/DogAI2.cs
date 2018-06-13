using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAI2 : DogsBehaviour
{
    public GameObject sNodesParent;
    public bool hasReached;

    protected override void Start()
    {
        base.Start();
        sceneNode = sNodesParent.GetComponentsInChildren<Transform>();
    }

    protected override void CutsceneUpdate()
    {
        base.CutsceneUpdate();
        agent.speed = 25;
        agent.acceleration = 100;
        GoToSceneNode(1);
        if((Vector3.Distance(transform.position, sceneNode[currentSNode].position) < minDistance) && !hasReached)
        {
            
            hasReached = true;
            gameObject.SetActive(false);
        }
    }
}
