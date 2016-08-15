﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    public Transform agentPrefab;
    public int numberOfAgents;
    private List<Agent> agents;

    // Use this for initialization.
    void Start()
    {
        agents = new List<Agent>();
        spawnAgents(agentPrefab, numberOfAgents);

        agents.AddRange(FindObjectsOfType<Agent>());
    }

    // Update is called once per frame.
    void Update()
    {

    }

    // Randomly spawns a number of agents in the scene.
    private void spawnAgents(Transform prefab, int agentNumber)
    {
        for (int i = 0; i < agentNumber; i++)
        {
            var obj = Instantiate(prefab, new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f)), Quaternion.identity);
        }
    }

    // Returns the neighbours of agent inside radius.
    private List<Agent> getNeighbours(Agent agent, float radius)
    {
        return null;
    }
}
