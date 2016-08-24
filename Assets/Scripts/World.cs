using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    public Transform agentPrefab;
    public int numberOfAgents;
    public float bound;
    public int numberOfSubdivision;
    public float spawnRadius;

    public bool threeDimensions = false;

    public List<Agent>[,] bins;
    private List<Agent> agents;
    private List<Predator> predators;

    // Use this for initialization.
    void Start()
    {
        // Build bins.
        buildWorldBins();

        agents = new List<Agent>();
        predators = new List<Predator>();

        spawnAgents(agentPrefab, numberOfAgents);
        agents.AddRange(FindObjectsOfType<Agent>());
        predators.AddRange(FindObjectsOfType<Predator>());
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

    // Returns the preadtors inside the radius.
    public List<Predator> getPredators(Agent agent, float radius)
    {
        List<Predator> predatorAgents = new List<Predator>();
        for (int i = 0; i < predators.Count; i++)
        {
            if (Vector3.Distance(agent.position, predators[i].position) <= radius)
                predatorAgents.Add(predators[i]);
        }

        return predatorAgents;
    }

    // Returns the bin location for the agent given.
    public Vector3 determineAgentBin(Agent agent)
    {
        float binSize = (bound * 2) / numberOfSubdivision;
        int binX = (int)Mathf.Floor(agent.position.x / binSize);
        int binZ = (int)Mathf.Floor(agent.position.z / binSize);

        int binY;
        if (threeDimensions)
            binY = (int)Mathf.Floor(agent.position.y / binSize);
        else
            binY = 0;

        return new Vector3(binX, binY, binZ);
    }

    // Sets up the world bins.
    private void buildWorldBins()
    {
        bins = new List<Agent>[numberOfSubdivision, numberOfSubdivision];
        for (int i = 0; i < numberOfSubdivision; i++)
        {
            for (int j = 0; j < numberOfSubdivision; j++)
            {
                bins[i, j] = new List<Agent>();
            }
        }
    }

    // Randomly spawns a number of agents in the scene.
    private void spawnAgents(Transform prefab, int agentNumber)
    {
        for (int i = 0; i < agentNumber; i++)
        {
            if (threeDimensions)
            {
                GameObject agent = Instantiate(prefab, new Vector3(Random.Range(-spawnRadius + bound, spawnRadius + bound), Random.Range(-spawnRadius + bound, spawnRadius + bound), Random.Range(-spawnRadius + bound, spawnRadius + bound)), Quaternion.identity) as GameObject;
            } 
            else
            {
                GameObject agent = Instantiate(prefab, new Vector3(Random.Range(-spawnRadius + bound, spawnRadius + bound), 0, Random.Range(-spawnRadius + bound, spawnRadius + bound)), Quaternion.identity) as GameObject;
            }
        }
    }
}
