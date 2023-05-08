using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Agent
{
    //Agent States
    public enum FishState
    {
        Swim,
        Eat,
    }

    private FishState currentState = FishState.Swim;

    public FishState CurrentState => currentState;

    protected override void CalculateSteeringForces()
    {
        switch (currentState)
        {
            case FishState.Swim:
                //wander around the tank
                Wander();
                Separate(AgentManager.Instance.agents);
                StayInBounds(3f);
                AvoidAllObstacles();

                if(FoodManager.Instance.Foods.Count > 0)
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
                    currentState = FishState.Swim;
                }
                foreach (GameObject food in FoodManager.Instance.Foods)
                {
                    //Seek(food.transform.position);
                }
                break;
        }
    }
}
