using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Player))]
public class Enemy : MonoBehaviour
{

    protected Player Player;
    protected Controller Controller;
    protected NavMeshAgent Agent;
    protected StateEnum State;
    protected Target[] PotentialTargets;
    protected Target target;
    protected float NextState;

    // Start is called before the first frame update
    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Controller = FindObjectOfType<Controller>();
        Player = GetComponent<Player>();
        PotentialTargets = FindObjectsOfType<Target>();
        target = PotentialTargets[Random.Range(0, PotentialTargets.Length)];
        Agent.SetDestination(target.transform.position);
        State = StateEnum.RUN;

        Player.Input.SwitchToAK = true;
    }

    // Update is called once per frame
    void Update()
    {
        Agent.updatePosition = true;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        NextState -= Time.deltaTime;

        switch (State)
        {
            case StateEnum.RUN:
                
                Player.Input.LookX = Mathf.Sign(Agent.desiredVelocity.x);
                Player.Input.LookZ = Mathf.Sign(Agent.desiredVelocity.z);

                if (Agent.desiredVelocity.magnitude < 0.1f)
                {
                    State = StateEnum.SHOOT;
                    NextState = Random.Range(5f, 12f);
                }

                break;
            case StateEnum.SHOOT:

                var look = Controller.transform.position - transform.position;
                Player.Input.LookX = look.x;
                Player.Input.LookZ = look.z;

                if (NextState < 0)
                {
                    State = StateEnum.RUN;
                    target = PotentialTargets[Random.Range(0, PotentialTargets.Length)];
                    Agent.SetDestination(target.transform.position);
                }
                break;
        }

        //transform.position += Agent.desiredVelocity * Time.deltaTime;
        Player.Input.RunX = Mathf.Abs(Agent.desiredVelocity.x) > 0.1f ? Mathf.Sign(Agent.desiredVelocity.x) : 0;
        Player.Input.RunZ = Mathf.Abs(Agent.desiredVelocity.z) > 0.1f ? Mathf.Sign(Agent.desiredVelocity.z) : 0;
        Player.Input.Shoot = State == StateEnum.SHOOT;


        Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + Agent.desiredVelocity * 10);
    }

    public enum StateEnum
    {
        RUN,
        SHOOT
    }
}
