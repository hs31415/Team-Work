using UnityEngine;
using System;
using UnityEngine.AI;

public class remoteAttack : MonoBehaviour
{
    public float attackRange = 5f; // 攻击范围
    public float attack = 1f;//攻击力
    public GameObject attackPrefab; // 攻击物体的预制体
    public float attackInterval = 1f; // 攻击间隔，单位为秒
    public int style = 0;//0为单体;1为范围
    public float splash = 0f;//攻击溅射范围
    private float lastAttackTime = 0f; // 上次攻击时间
    private GameObject closestEnemy;//最近的地方单位
    private float closestDistance = 100;//与最近敌方单位的距离


    void Start()
    {
    }

    void Update()
    {
        Collider2D[] attackColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);//攻击范围碰撞检测盒
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
                if (CanAttack())
                {
                    Attack(closestEnemy);
                    lastAttackTime = Time.time; // 更新上次攻击时间
                }
            }
            else if (collider.gameObject.CompareTag("Blue") && gameObject.CompareTag("Red"))//另一方的攻击逻辑
            {
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position); // 计算与敌人的距离
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = collider.gameObject; // 更新最近的敌人
                }
                if (CanAttack())
                {
                    Attack(closestEnemy);
                    lastAttackTime = Time.time; // 更新上次攻击时间
                }
            }
        }
    }
    void Attack(GameObject target)
    {
        GameObject attackInstance = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        attackInstance.GetComponent<AttackObject>().SetTarget(target);
        attackInstance.GetComponent<AttackObject>().damage = attack;
        attackInstance.GetComponent<AttackObject>().style = style;
        attackInstance.GetComponent<AttackObject>().splash = splash;
    }
    bool CanAttack()
    {
        return Time.time - lastAttackTime > attackInterval; // 检查是否超过攻击间隔
    }
}
