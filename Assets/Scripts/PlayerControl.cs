using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private float thrustInput;
    private float rotateDirection;
    public float acceleration;
    public float deceleration;
    public float friction;
    public float turnSpeed;
    public float maxSpeed;
    private float speedTolerance = 1e-2f;
    public ParticleSystem jetComponent;
    private ParticleSystem.MainModule jetMain;

    void Start()
    {
        jetMain = jetComponent.main;
        rb = GetComponent<Rigidbody2D>();
        jetMain.startColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        float normalizedVelocity = rb.velocity.magnitude / maxSpeed;
        jetMain.startLifetime = (normalizedVelocity * 0.5f);
    }

    void FixedUpdate()
    {
        Vector2 forwardDirection = (Vector2)transform.up;

        thrustInput = Input.GetAxis("Vertical");
        rotateDirection = Input.GetAxis("Horizontal");
        if(thrustInput >= 0 && rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(forwardDirection * thrustInput * acceleration);
        }
        else
        {
            rb.velocity -= deceleration * rb.velocity;
        }

        transform.Rotate(new Vector3(0,0,-1) * turnSpeed * rotateDirection);
        rb.velocity -= friction * rb.velocity;

        if(rb.velocity.magnitude <= speedTolerance)
            rb.velocity = new Vector2(0.0f, 0.0f);
    }
}
