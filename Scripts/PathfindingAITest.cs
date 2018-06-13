using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //To Make NavMeshAgent usable

public class PathfindingAITest : MonoBehaviour
{
    public LayerMask mask;
    public float distance = Mathf.Infinity;
	private NavMeshAgent agent;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	
	void Update ()
    {
        // This with a player is fine but for enemies it´s not because its a lot of calculations with multiple enemies
        if(Input.GetMouseButton(0)) // Left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Make sure your camera in scene has the tag Main Camera.
            RaycastHit hit = new RaycastHit();

            if(Physics.Raycast(ray, out hit, distance, mask))
            {
                //Debug.Log(hit.point); // Test if it receives the data.
                //Tell the agent to go to hit.point
                agent.SetDestination(hit.point);
            }
        }
	}
}
