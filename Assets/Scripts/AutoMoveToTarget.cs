using UnityEngine;
using UnityEngine.AI;

public class AutoMoveToTarget : MonoBehaviour
{
    public Transform target; // 目标位置

    private NavMeshAgent navMeshAgent;

    void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        SetDestination();
    }

    void SetDestination()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }
}
