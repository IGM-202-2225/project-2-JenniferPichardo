using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Agent
{
    protected override void CalculateSteeringForces()
    {
        Wander();

        Flock(AgentManager.Instance.agents);
        //Separate(AgentManager.Instance.agents);

        StayInBounds(3f);

        AvoidAllObstacles();
    }
}
