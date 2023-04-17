using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    public PhysicsObject physicsObject;

    public float maxSpeed = 5f;
    public float maxForce = 5f;

    private Vector3 totalForce = Vector3.zero;

    private float wanderAngle = 0f;

    public float maxWanderAngle = 45f;

    public float maxWanderChangePerSecond = 10f;

    public float personalSpace = 1f;

    private void Awake()
    {
        if(physicsObject == null)
        {
            physicsObject = GetComponent<PhysicsObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSteeringForces();

        totalForce = Vector3.ClampMagnitude(totalForce, maxForce);
        physicsObject.ApplyForce(totalForce);

        totalForce = Vector3.zero;
    }

    protected abstract void CalculateSteeringForces();

    protected void Seek(Vector3 targetPos, float weight = 1f)
    {
        //calculate desired velocity
        Vector3 desiredVelocity = targetPos = physicsObject.Position;

        //set desired velocity magnitude to max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        //calculate the seek steering force
        Vector3 seekingForce = desiredVelocity - physicsObject.Velocity;

        //apply the seek steering force
        totalForce += seekingForce * weight;
    }

    protected void Flee(Vector3 targetPos, float weight = 1f)
    {
        Vector3 desiredVelocity = physicsObject.Position - targetPos;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 fleeingForce = desiredVelocity - physicsObject.Velocity;

        totalForce += fleeingForce * weight;
    }

    protected void Wander(float weight = 1f)
    {

    }

    protected void StayInBounds()
    {
        Vector3 futurePos = GetFuturePosition();

        if(futurePos.x > AgentManager.Instance.maxPosition.x ||
            futurePos.x < AgentManager.Instance.minPosition.x ||
            futurePos.y > AgentManager.Instance.maxPosition.y ||
            futurePos.y < AgentManager.Instance.minPosition.y)
        {
            Seek(Vector3.zero);
        } 
        
    }

    public Vector3 GetFuturePosition(float timeToLookAhead = 1f)
    {
        return new Vector3();
    }

    protected void Pursue(Agent other, float timeToLookAhead = 1f)
    {

    }

    protected void Evade(Agent other, float timeToLookAhead = 1f)
    {

    }

    protected void Separate<T>(List<T> agents) where T : Agent
    {
        float sqrPersonalSpace = Mathf.Pow(personalSpace, 2);

        foreach(T other in agents)
        {
            float sqrDist = Vector3.SqrMagnitude(other.physicsObject.Position - physicsObject.Position);
            
            if(sqrDist < float.Epsilon)
            {
                continue;
            }
        

            if(sqrDist < sqrPersonalSpace)
            {
                float weight = sqrPersonalSpace / (sqrDist + 0.1f);
                Flee(other.physicsObject.Position, weight);
            }
        }
    }
}
