using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public EnemyState currentState;
    public Transform player;
    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = EnemyState.Patrol;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                if (distance < chaseDistance)
                {
                    currentState = EnemyState.Chase;
                }
                break;
            case EnemyState.Chase:
                Chase();
                if (distance < attackDistance)
                {
                    currentState = EnemyState.Attack;
                }
                else if (distance > chaseDistance) 
                {
                    currentState = EnemyState.Patrol;
                }
                break;
            case EnemyState.Attack:
                Attack();
                if (distance > attackDistance)
                {
                    currentState = EnemyState.Chase;
                }
                break;
        }
    }

    void Patrol()
    {
        agent.isStopped = false;
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.isStopped = true;
        Debug.Log("Enemy Attack");
    }
}