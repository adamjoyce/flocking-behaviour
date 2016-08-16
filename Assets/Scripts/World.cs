using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    public Transform agentPrefab;
    public int numberOfAgents;
    public float bound;
    public float spawnRadius;


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

    // Returns the neighbours of agent inside radius.
    public List<Agent> getNeighbours(Agent agent, float radius)
    {
        List<Agent> neighbourAgents = new List<Agent>();
        for (int i = 0; i < agents.Count; i++)
        {
            if (agents[i] != agent && Vector3.Distance(agent.position, agents[i].position) <= radius)
                neighbourAgents.Add(agents[i]);
        }

        return neighbourAgents;
    }

    // Randomly spawns a number of agents in the scene.
    private void spawnAgents(Transform prefab, int agentNumber)
    {
        for (int i = 0; i < agentNumber; i++)
        {
            GameObject agent = Instantiate(prefab, new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)), Quaternion.identity) as GameObject;
        }
    }
}
