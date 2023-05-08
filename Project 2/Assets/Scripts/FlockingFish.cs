using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingFish : Agent
{
    //Agent States
    public enum FishState
    {
        Flock,
        Eat,
        Flee
    }

    private FishState currentState = FishState.Flock;

    public FishState CurrentState => currentState;

    protected override void CalculateSteeringForces()
    {
        switch (currentState)
        {
            case FishState.Flock:
                //wander around the tank
                Wander();
                Flock(AgentManager.Instance.flockingAgents);
                StayInBounds(3f);
                AvoidAllObstacles();
                if (FoodManager.Instance.Foods.Count > 0)
                {
                    currentState = FishState.Eat;
                }
                break;
            case FishState.Eat:
                //seek the nearest food if it spawns
                StayInBounds();
                AvoidAllObstacles();
                if (FoodManager.Instance.Foods.Count == 0)
                {
                    currentState = FishState.Flock;
                }
                foreach (GameObject food in FoodManager.Instance.Foods)
                {
                    //Seek(food.transform.position);
                }
                break;
            case FishState.Flee:
                //flee from big fish
                foreach(Fish fish in AgentManager.Instance.bigAgents)
                {
                    Evade(fish);
                }
                break;
        }
    }

}
