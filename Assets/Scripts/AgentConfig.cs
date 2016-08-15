using UnityEngine;
using System.Collections;

public class AgentConfig : MonoBehaviour
{
    public float cohesionRadius;
    public float seperationRadius;
    public float alignmentRadius;

    public float cohesionCoeff;
    public float seperationCoeff;
    public float alignmentCoeff;

    public float maxAcceleration = 10.0f;
    public float maxVelocity = 10.0f;
}