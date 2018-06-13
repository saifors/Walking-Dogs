using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAI1 : DogsBehaviour
{
    public bool startedFleeing;
    public bool reachedFirst;
    public bool hasFled;

    public GameObject sNodesParent;
    

    protected override void Start()
    {
        base.Start();
        sceneNode = sNodesParent.GetComponentsInChildren<Transform>();
        
    }

    protected override void CutsceneUpdate()
    {
        if(startedFleeing && !reachedFirst)
        {
            agent.speed = 25;
            agent.acceleration = 100;
            GoToSceneNode(1);
        }
        if ((Vector3.Distance(transform.position, sceneNode[currentSNode].position) < minDistance) && !hasFled)
        {
            
            if (!reachedFirst) reachedFirst = true;
            if(currentSNode >= sceneNode.Length)
            {
                hasFled = true;
                gameObject.SetActive(false);
            }
            else GoToNextNode();
        }
    }

    

}
