using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;
    public World world;
    public AgentConfig config;

    // Use this for initialization.
    void Start()
    {
        world = FindObjectOfType<World>();
        config = FindObjectOfType<AgentConfig>();
        position = transform.position;
    }

    // Update is called once per frame.
    void Update()
    {
        float time = Time.deltaTime;

        acceleration = combine();
        acceleration = Vector3.ClampMagnitude(acceleration, config.maxAcceleration);

        velocity += acceleration * time;
        velocity = Vector3.ClampMagnitude(velocity, config.maxVelocity);

        position += velocity * time;
        transform.position = position;
    }

    // Cohesion behaviour.
    private Vector3 cohesion()
    {
        return Vector3.zero;
    }

    // Seperation behaviour.
    private Vector3 seperation()
    {
        return Vector3.zero;
    }

    // Alignment behaviour.
    private Vector3 alignment()
    {
        return Vector3.zero;
    }

    // Combines all behaviours.
    private Vector3 combine()
    {
        return Vector3.zero;
    }
}
