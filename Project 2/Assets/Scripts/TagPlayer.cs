using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagPlayer : Agent
{
    public enum TagState
    {
        It,
        NotIt,
        Counting
    }

    private TagState currentState = TagState.NotIt;

    public TagState CurrentState => currentState;


    protected override void CalculateSteeringForces()
    {
        switch (currentState)
        {
            case TagState.It:
                {
                    break;
                }
            case TagState.Counting:
                {
                    break;
                }
            case TagState.NotIt:
                {
                    break;
                }
        }
    }

    private void StateTransition(TagState newTagState)
    {
        currentState = newTagState;


        switch (currentState)
        {
            case TagState.It:
                {
                    break;
                }
            case TagState.Counting:
                {
                    break;
                }
            case TagState.NotIt:
                {
                    break;
                }
        }
    }
}
