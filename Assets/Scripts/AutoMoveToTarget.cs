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
        navMeshAgent.updateUpAxis = false;//禁止xy轴翻转，避免2d物体旋转后不可见
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
