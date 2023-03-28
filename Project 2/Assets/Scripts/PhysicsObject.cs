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

    public bool useGravity = false;

    public bool useFriction = false;

    public float frictionCoeff = 0.2f;

    private Vector3 cameraSize;

    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        cameraSize.y = Camera.main.orthographicSize;
        cameraSize.x = cameraSize.y * Camera.main.aspect;
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

        //calculate new velocity based on current acceleraion of object
        velocity += acceleration * Time.deltaTime;

        //calculate the new position based on the velocity for this frame
        transform.position += velocity * Time.deltaTime;

        //store the direction that the object is moving in
        direction = velocity.normalized;

        //zero out the acceleration for next frame
        acceleration = Vector3.zero;

        //get the mouse position for each frame
        mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = transform.position.z;

        //calculate force and apply to object
        Vector3 force = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, 0);
        force.Normalize();
        ApplyForce(force);

        Bounce();
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
        if(transform.position.x > cameraSize.x && velocity.x > 0)
        {
            velocity.x *= -1f;
        }
        if (transform.position.x < -cameraSize.x && velocity.x < 0)
        {
            velocity.x *= -1f;
        }
        if (transform.position.y > cameraSize.y && velocity.y > 0)
        {
            velocity.y *= -1f;
        }
        if (transform.position.y < -cameraSize.y && velocity.y < 0)
        {
            velocity.y *= -1f;
        }
    }
}
