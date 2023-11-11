using UnityEngine;
using System;

public class ObjectState : MonoBehaviour
{
    public event Action<float> OnHealthChanged; // 定义生命值变化事件
    public float CurrentHealth { get; private set; } // 使用属性访问currentHealth
    public float initialHealth = 3; // 初始生命值
    public float moveSpeed; // 移动速度
    public int cost;
    public float attackRange = 5f; // 攻击范围
    public GameObject attackPrefab; // 攻击物体的预制体
    public float attackInterval = 1f; // 攻击间隔，单位为秒
    private float lastAttackTime = 0f; // 上次攻击时间
    private bool canAttack = true; // 标记是否可以进行攻击

    private float currentHealth; // 当前生命值

    void Start()
    {
        currentHealth = initialHealth; // 初始化当前生命值
        UpdateHealth(); // 初始化时更新生命值
    }

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Red") && gameObject.CompareTag("Blue"))
            {
                if (CanAttack()) // 检查是否可以攻击
                {
                    Attack(collider.gameObject);
                    lastAttackTime = Time.time; // 更新上次攻击时间
                }
            }
            if (collider.gameObject.CompareTag("Blue") && gameObject.CompareTag("Red"))
            {
                if (CanAttack()) // 检查是否可以攻击
                {
                    Attack(collider.gameObject);
                    lastAttackTime = Time.time; // 更新上次攻击时间
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Blue") && gameObject.CompareTag("Red")) ||
            (collision.gameObject.CompareTag("Red") && gameObject.CompareTag("Blue")))
        {
            Debug.Log("开始碰撞");
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
