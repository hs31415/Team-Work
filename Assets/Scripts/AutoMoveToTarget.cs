using UnityEngine;
using UnityEngine.AI;

public class AutoMoveToTarget : MonoBehaviour
{
    public Transform target; // 目标位置
    public float moveSpeed; // 移动速度

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        ObjectState objectState = GetComponent<ObjectState>(); // 获取ObjectState组件
        if (objectState != null)
        {
            moveSpeed = objectState.moveSpeed; // 获取移动速度值
        }
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>(); // 正确的使用方式
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false; // 禁止xy轴翻转，避免2d物体旋转后不可见
        navMeshAgent.speed = moveSpeed; // 设置移动速度
        navMeshAgent.SetDestination(target.position);
    }
}
