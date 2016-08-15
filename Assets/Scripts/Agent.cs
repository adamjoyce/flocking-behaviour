using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        acceleration = cohesionBehaviour();//combineBehaviours();
        acceleration = Vector3.ClampMagnitude(acceleration, config.maxAcceleration);

        velocity += acceleration * time;
        velocity = Vector3.ClampMagnitude(velocity, config.maxVelocity);

        position += velocity * time;
        transform.position = position;
    }

    // Steers the agent's current velocity towards the centre of mass of all nearby neighbours.
    private Vector3 cohesionBehaviour()
    {
        Vector3 resultantVector = new Vector3();

        // Get all nearby neighbours.
        List<Agent> neighbours = world.getNeighbours(this, config.cohesionRadius);

        // Zero neighbours means no cohesion desire.
        if (neighbours.Count == 0)
            return resultantVector;

        // Find the centre of mass of all neighbours.
        for (int i = 0; i < neighbours.Count; i++)
        {
            resultantVector += neighbours[i].position;
        }
        resultantVector /= neighbours.Count;

        // Vector towards centre of mass and normalise.
        resultantVector = resultantVector - position;
        Vector3.Normalize(resultantVector);

        return resultantVector;
    }

    // Seperation behaviour.
    private Vector3 seperationBehaviour()
    {
        return Vector3.zero;
    }

    // Alignment behaviour.
    private Vector3 alignmentBehaviour()
    {
        return Vector3.zero;
    }

    // Combines all behaviours.
    private Vector3 combineBehaviours()
    {
        return Vector3.zero;
    }
}
