using UnityEngine;
using System;
using UnityEngine.AI;

public class ObjectState : MonoBehaviour
{
    public event Action<float> OnHealthChanged; // 定义生命值变化事件
    public float CurrentHealth { get; private set; } // 使用属性访问currentHealth
    public float initialHealth = 3; // 初始生命值
    public float moveSpeed; // 移动速度
    public int cost;
    public float attackRange = 5f; // 攻击范围
    public float enmityRange = 5f;
    public GameObject attackPrefab; // 攻击物体的预制体
    public float attackInterval = 1f; // 攻击间隔，单位为秒
    private float lastAttackTime = 0f; // 上次攻击时间
    private bool canAttack = true; // 标记是否可以进行攻击
    private float closestDistance = 100;
    private GameObject closestEnemy;
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
        foreach (Collider2D collider in attackColliders)//对于进入攻击范围的物体，进行攻击
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
                if (CanAttack()) // 检查是否可以攻击
                {
                    if (closestEnemy != null)
                    {
                        Attack(closestEnemy);
                    }
                    else
                    {
                        Attack(collider.gameObject);
                    }

                    lastAttackTime = Time.time; // 更新上次攻击时间
                }
            }
            else if (collider.gameObject.CompareTag("Blue") && gameObject.CompareTag("Red"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position); // 计算与敌人的距离
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = collider.gameObject; // 更新最近的敌人
                }
                isAttacking = true;
                agent.speed = 0;
                if (CanAttack()) // 检查是否可以攻击
                {
                    if (closestEnemy != null)
                    {
                        Attack(closestEnemy);
                    }
                    else
                    {
                        Attack(collider.gameObject);
                    }
                    lastAttackTime = Time.time; // 更新上次攻击时间
                }
            }
        }
        if (!foundEnemy)
        {
            agent.SetDestination(originalMoveTarget);
        }
        if (!isAttacking)
        {
            agent.speed = moveSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Blue") && gameObject.CompareTag("Red")) ||
            (collision.gameObject.CompareTag("Red") && gameObject.CompareTag("Blue")))
        {
            DecreaseHealth();
        }
    }

    void Attack(GameObject target)
    {
        GameObject attackInstance = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        attackInstance.GetComponent<AttackObject>().SetTarget(target);
    }

    public void DecreaseHealth()
    {
        currentHealth--; // 生命值减一
        UpdateHealth(); // 更新生命值
    }

    void UpdateHealth()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // 生命值归零时销毁对象
        }

        OnHealthChanged?.Invoke(currentHealth); // 触发生命值变化事件
    }

    bool CanAttack()
    {
        return Time.time - lastAttackTime > attackInterval; // 检查是否超过攻击间隔
    }
}
