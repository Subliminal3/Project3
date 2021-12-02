using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public Material AlwaysVisible, enemy;
    public float maxSpeed = 7f;
    public float slowedSpeed = 3f;



    public float speed;
    private GameObject thisObj;
    private NavMeshAgent agent = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = maxSpeed;

    }
    void Update()
    {
        agent.destination = player.transform.position;
    }

    public void ReduceSpeed()
    {
        agent.GetComponent<NavMeshAgent>().speed = slowedSpeed;
        Debug.Log("I have been slowed!");

        
    }

    public void IncreaseSpeed()
    {
        agent.GetComponent<NavMeshAgent>().speed = maxSpeed;
        Debug.Log("Speed returned to normal");
    }

    public void AlwaysVisibleTexture()
    {
        this.GetComponent<MeshRenderer>().material = AlwaysVisible;
    }

    public void EnemyTexure()
    {
        this.GetComponent<MeshRenderer>().material = enemy;
    }




}
