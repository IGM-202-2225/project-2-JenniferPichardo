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

    public float visionRange = 2f;

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
        Vector3 desiredVelocity = targetPos - physicsObject.Position;

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
        //update the angle of current wander
        float maxWanderChange = maxWanderChangePerSecond * Time.deltaTime;
        wanderAngle += Random.Range(-maxWanderChange, maxWanderChange);

        wanderAngle = Mathf.Clamp(wanderAngle, -maxWanderAngle, maxWanderAngle);

        //get a position defined by wander angle
        Vector3 wanderTarget = Quaternion.Euler(0, 0, wanderAngle) * physicsObject.Direction.normalized + physicsObject.Position;

        //seek towards wander position
        Seek(wanderTarget, weight);
    }

    protected void StayInBounds(float weight = 1f)
    {
        Vector3 futurePos = GetFuturePosition();

        if (futurePos.x > AgentManager.Instance.maxPosition.x ||
            futurePos.x < AgentManager.Instance.minPosition.x ||
            futurePos.y > AgentManager.Instance.maxPosition.y ||
            futurePos.y < AgentManager.Instance.minPosition.y)
        {
            Seek(Vector3.zero, weight);
        }

    }

    public Vector3 GetFuturePosition(float timeToLookAhead = 1f)
    {
        return physicsObject.Position + physicsObject.Velocity * timeToLookAhead;
    }

    protected void Pursue(Agent other, float timeToLookAhead = 1f, float weight = 1f)
    {
        //get future position of agent that is being pursued
        Vector3 futurePos = other.GetFuturePosition(timeToLookAhead);

        //seek towards future position
        Seek(futurePos, weight);
    }

    protected void Evade(Agent other, float timeToLookAhead = 1f, float weight = 1f)
    {
        //get future position of agent that is being evaded
        Vector3 futurePos = other.GetFuturePosition(timeToLookAhead);

        //flee from future position
        Flee(futurePos, weight);
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

    private void AvoidObstacle(Obstacle obstacle)
    {
        //get a vector from agent to obstacle
        Vector3 toObstacle = obstacle.Position - physicsObject.Position;

        //check if obstacle is behind agent
        float fwdToObstacleDot = Vector3.Dot(physicsObject.Direction, toObstacle);

        if(fwdToObstacleDot < 0)
        {
            return;
        }

        //check if obstacle to too far from left or right
        float rightToObstacleDot = Vector3.Dot(physicsObject.Right, toObstacle);

        if(Mathf.Abs(rightToObstacleDot) > physicsObject.radius + obstacle.radius)
        {
            return;
        }

        if(fwdToObstacleDot > visionRange)
        {
            return;
        }

        Vector3 desiredVelocity;

        if(rightToObstacleDot > 0)
        {
            //steer left
            desiredVelocity = physicsObject.Right * -maxSpeed;
        }
        else
        {
            //steer right
            desiredVelocity = physicsObject.Right * maxSpeed;
        }

        float weight = visionRange / (fwdToObstacleDot + 0.1f);

        Vector3 steeringForce = (desiredVelocity - physicsObject.Velocity) * weight;

        totalForce += steeringForce;
    }

    protected void AvoidAllObstacles()
    {
        foreach(Obstacle obstacle in ObstacleManager.Instance.obstacles)
        {
            AvoidObstacle(obstacle);
        }
    }
}