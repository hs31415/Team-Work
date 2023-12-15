using UnityEngine;
using System;
using UnityEngine.AI;

public class ObjectState : MonoBehaviour
{
    public int style;//1、兵种 2、法术 3、建筑 4、基地
    public event Action<float> OnHealthChanged; // 定义生命值变化事件
    public float CurrentHealth { get; private set; } // 使用属性访问currentHealth
    public float initialHealth = 3; // 初始生命值
    public float moveSpeed; // 移动速度
    public int cost;//费用
    public float attackRange = 5f; // 攻击范围
    public float enmityRange = 5f;//仇恨范围
    public float defense = 0.8f;//防御力,0-1之间
    private float closestDistance = 100;//与最近敌方单位的距离
    private GameObject closestEnemy;//最近的地方单位
    private float currentHealth; // 当前生命值
    private NavMeshAgent agent; // 获取物体的 NavMeshAgent 组件
    private Vector3 originalMoveTarget; //移动目标
    private bool SetTarget = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // 获取 NavMeshAgent 组件
        currentHealth = initialHealth; // 初始化当前生命值
        UpdateHealth(); // 初始化时更新生命值
    }

    void Update()
    {
        if (style == 1)
        {
            if (!SetTarget)
            {
                SetTarget = true;
                originalMoveTarget = agent.destination;
            }
            bool foundEnemy = false;//是否发现敌人
            bool isAttacking = false;//是否正在攻击
            Collider2D[] attackColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);//攻击范围碰撞检测盒
            Collider2D[] enmityColliders = Physics2D.OverlapCircleAll(transform.position, enmityRange);//仇恨范围碰撞检测盒
            foreach (Collider2D collider in enmityColliders)//对于进入仇恨范围的敌方物体，向其移动但不进行攻击
            {
                if (collider.gameObject.CompareTag("Red") && gameObject.CompareTag("Blue"))
                {
                    foundEnemy = true;
                    agent.SetDestination(collider.gameObject.transform.position);//把物体移动目标设定为敌方物
                }
                else if (collider.gameObject.CompareTag("Blue") && gameObject.CompareTag("Red"))
                {
                    foundEnemy = true;
                    agent.SetDestination(collider.gameObject.transform.position);//把物体移动目标设定为敌方物体
                }
            }
            foreach (Collider2D collider in attackColliders)//对于进入攻击范围的物体，进行攻击，具体攻击逻辑归属于另一个脚本，方便进行不同的攻击逻辑
            {
                if (collider.gameObject.CompareTag("Red") && gameObject.CompareTag("Blue"))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position); // 计算与敌人的距离
                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = collider.gameObject; // 更新最近的敌人
                    }
                    isAttacking = true;
                    agent.speed = 0;
                }
                else if (collider.gameObject.CompareTag("Blue") && gameObject.CompareTag("Red"))//另一方的攻击逻辑
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position); // 计算与敌人的距离
                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = collider.gameObject; // 更新最近的敌人
                    }
                    isAttacking = true;
                    agent.speed = 0;
                }
            }
            if (!foundEnemy)//被敌方单位吸引到仇恨，向其移动
            {
                agent.SetDestination(originalMoveTarget);
            }
            if (!isAttacking)//未攻击，恢复正常速度
            {
                agent.speed = moveSpeed;
            }
        }
        else if (style == 2)
        {

        }

    }

    public void DecreaseHealth(float damage)//减少生命值
    {
        currentHealth -= damage * defense;
        UpdateHealth(); // 更新生命值
    }

    public float getHealth()
    {
        return currentHealth;
    }
    void UpdateHealth()
    {
        if (currentHealth <= 0 && style != 4)
        {
            Destroy(gameObject); // 生命值归零时销毁对象
        }

        OnHealthChanged?.Invoke(currentHealth); // 触发生命值变化事件
    }

}
