﻿using UnityEngine;
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
        velocity = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
    }

    // Update is called once per frame.
    void Update()
    {
        float time = Time.deltaTime;

        acceleration = alignmentBehaviour();//combineBehaviours();
        acceleration = Vector3.ClampMagnitude(acceleration, config.maxAcceleration);

        velocity += acceleration * time;
        velocity = Vector3.ClampMagnitude(velocity, config.maxVelocity);

        position += velocity * time;
        transform.position = position;

        if (velocity.magnitude > 0)
            transform.LookAt(position + velocity);
    }

    // Steers the agent's current velocity towards the centre of mass of all nearby neighbours.
    private Vector3 cohesionBehaviour()
    {
        Vector3 resultantVector = new Vector3();

        // Get all nearby neighbours.
        List<Agent> neighbours = world.getNeighbours(this, config.cohesionRadius);

        // Zero neighbours results in no cohesion desire.
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

        return resultantVector.normalized;
    }

    // Steers the agent in the opposite direction from each of its neighbours.
    private Vector3 seperationBehaviour()
    {
        Vector3 resultantVector = new Vector3();

        // Get all nearby neighbours.
        List<Agent> neighbours = world.getNeighbours(this, config.seperationRadius);

        // Zero neighbours results in no seperation desire.
        if (neighbours.Count == 0)
            return resultantVector;

        // Add the contribution from each neighbour towards this agent.
        for (int i = 0; i < neighbours.Count; i++)
        {
            Vector3 towardsAgent = position - neighbours[i].position;

            // The force contribution will vary inversely to the distance.
            if (towardsAgent.magnitude > 0)
                resultantVector += towardsAgent.normalized / towardsAgent.magnitude;
        }

        return resultantVector;
    }

    // Steers the agent to match the velocity of its neighbours.
    private Vector3 alignmentBehaviour()
    {
        Vector3 resultantVector = new Vector3();

        // Get all nearby neighbours.
        List<Agent> neighbours = world.getNeighbours(this, config.alignmentRadius);

        // Zero neighbours results in no alignement desire.
        if (neighbours.Count == 0)
            return resultantVector;

        // Match veloicty of all nearby neighbours.
        for (int i = 0; i < neighbours.Count; i++)
            resultantVector += neighbours[i].velocity;

        return resultantVector.normalized;
    }

    // Combines all behaviours.
    private Vector3 combineBehaviours()
    {
        return Vector3.zero;
    }
}
