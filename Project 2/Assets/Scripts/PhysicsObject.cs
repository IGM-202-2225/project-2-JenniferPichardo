using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsObject : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 acceleration;
    private Vector3 direction;

    public float mass = 1f;
    public float gravityStrength = 1f;

    public bool bounceOffWalls = false;

    public bool useGravity = false;

    public bool useFriction = false;

    public float frictionCoeff = 0.2f;

    public float radius = 1f;

    public Vector3 Velocity => velocity;
    public Vector3 Direction => direction;

    public Vector3 Position => transform.position;

    public Vector3 Right => transform.right;


    // Start is called before the first frame update
    void Start()
    {
        direction = (Vector3)Random.insideUnitCircle.normalized;

    }

    // Update is called once per frame
    void Update()
    {
        if (useGravity)
        {
            ApplyGravity(Vector3.down * gravityStrength);
        }
        if (useFriction)
        {
            ApplyFriction(frictionCoeff);
        }
        if (bounceOffWalls)
        {
            Bounce();
        }

        //calculate new velocity based on current acceleraion of object
        velocity += acceleration * Time.deltaTime;

        //calculate the new position based on the velocity for this frame
        transform.position += velocity * Time.deltaTime;

        if(velocity.sqrMagnitude > Mathf.Epsilon)
        {
            //store the direction that the object is moving in
            direction = velocity.normalized;
        }

        //zero out the acceleration for next frame
        acceleration = Vector3.zero;

        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);

    }

    /// <summary>
    /// Applies a force to object using Newton's second law of motion
    /// </summary>
    /// <param name="force">The force vector that will act on this object</param>
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    /// <summary>
    /// Apply friction force to this object
    /// </summary>
    /// <param name="coefficient">Coefficient of friction</param>
    private void ApplyFriction(float coefficient)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coefficient;
        
        ApplyForce(friction);
    }

    private void ApplyGravity(Vector3 gravityForce)
    {
        acceleration += gravityForce;
    }

    private void Bounce()
    {
        if(transform.position.x > AgentManager.Instance.maxPosition.x && velocity.x > 0)
        {
            velocity.x *= -1f;
        }
        if (transform.position.x < AgentManager.Instance.minPosition.x && velocity.x < 0)
        {
            velocity.x *= -1f;
        }
        if (transform.position.y > AgentManager.Instance.maxPosition.y && velocity.y > 0)
        {
            velocity.y *= -1f;
        }
        if (transform.position.y < AgentManager.Instance.minPosition.y && velocity.y < 0)
        {
            velocity.y *= -1f;
        }
    }
}
