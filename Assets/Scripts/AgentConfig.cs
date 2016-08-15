using UnityEngine;
using System.Collections;

public class AgentConfig : MonoBehaviour
{
    public float Rc;
    public float Rs;
    public float Ra;

    public float Kc;
    public float Ks;
    public float Ka;

    public float maxAcceleration = 10.0f;
    public float maxVelocity = 10.0f;
}