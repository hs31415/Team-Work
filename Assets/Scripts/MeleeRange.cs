using UnityEngine;
using System;
using UnityEngine.AI;

public class MeleeRange : MonoBehaviour
{
    public float attackRange = 5f; // 攻击范围
    public float attack = 1f;//攻击力
    public GameObject attackPrefab; // 攻击物体的预制体
    public float attackInterval = 1f; // 攻击间隔，单位为秒
    private float lastAttackTime = 0f; // 上次攻击时间
    void Start()
    {
    }

    void Update()
    {
        Collider2D[] attackColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);//攻击范围碰撞检测盒
        foreach (Collider2D collider in attackColliders)//对于进入攻击范围的物体，进行攻击
        {
            if (CanAttack()) // 检查是否可以攻击
            {
                foreach (Collider2D colliders in attackColliders)//对于进入攻击范围的物体，进行攻击
                {
                    if (colliders.gameObject.CompareTag("Red") && gameObject.CompareTag("Blue"))
                    {
                        Attack(colliders.gameObject);
                    }
                    else if (colliders.gameObject.CompareTag("Blue") && gameObject.CompareTag("Red"))//另一方的攻击逻辑
                    {
                        Attack(colliders.gameObject);
                    }
                }
                lastAttackTime = Time.time; // 更新上次攻击时间
            }
        }
    }
    void Attack(GameObject target)
    {
        ObjectState enemy = target.GetComponent<ObjectState>();
        if (enemy != null)
        {
            enemy.DecreaseHealth(attack);
        }
    }
    bool CanAttack()
    {
        return Time.time - lastAttackTime > attackInterval; // 检查是否超过攻击间隔
    }
}
