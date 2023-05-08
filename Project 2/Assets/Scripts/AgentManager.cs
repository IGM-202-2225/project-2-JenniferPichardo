using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager Instance;

    [HideInInspector]
    //list for all agents
    public List<Agent> agents = new List<Agent>();
    [HideInInspector]
    //list for flocking fish agents
    public List<FlockingFish> flockingAgents = new List<FlockingFish>();
    [HideInInspector]
    //list for big fish agents
    public List<Fish> bigAgents = new List<Fish>();

    [HideInInspector]
    public Vector2 maxPosition = Vector2.one;
    [HideInInspector]
    public Vector2 minPosition = -Vector2.one;

    public float edgePadding = 1f;

    public Fish bigFish;
    public FlockingFish flockingFish;

    //number of standard fish
    public int numStandardFish = 5;

    //number of flocking fish
    public int numFlockingFish = 10;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        //set up camera dimensions
        Camera cam = Camera.main;

        if(cam != null)
        {
            Vector3 camPosition = cam.transform.position;
            float halfHeight = cam.orthographicSize;
            float halfWidth = halfHeight * cam.aspect;

            maxPosition.x = camPosition.x + halfWidth - edgePadding;
            maxPosition.y = camPosition.y + halfHeight - edgePadding;


            minPosition.x = camPosition.x - halfWidth + edgePadding;
            minPosition.y = camPosition.y - halfHeight + edgePadding;

        }

        //spawn standard big fish
        for(int i = 0; i < numStandardFish; i++)
        {
            Fish temp = Spawn(bigFish);
            bigAgents.Add(temp);
            agents.Add(temp);
        }
        //spawn flocking fish
        for(int i = 0; i < numFlockingFish; i++)
        {
            FlockingFish temp = Spawn(flockingFish);
            flockingAgents.Add(temp);
            agents.Add(temp);
        }
    }

    //spawn agents at a random location in camera
    private T Spawn<T>(T prefabToSpawn) where T : Agent
    {
        float xPos = Random.Range(minPosition.x, maxPosition.x);
        float yPos = Random.Range(minPosition.y, maxPosition.y);

        return Instantiate(prefabToSpawn, new Vector3(xPos, yPos), Quaternion.identity);
    }
}
