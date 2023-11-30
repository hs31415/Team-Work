using UnityEngine;
using System;
using UnityEngine.AI;

public class MeleeRange : MonoBehaviour
{
    public float attackRange = 5f; // 攻击范围
    public float attack = 1f;//攻击力
    public float defense = 0.8f;//防御力,0-1之间
    public GameObject attackPrefab; // 攻击物体的预制体
    public float attackInterval = 1f; // 攻击间隔，单位为秒
    private float lastAttackTime = 0f; // 上次攻击时间
    void Start()
    {
    }

    void Update()
    {
        bool isAttacking = false;//是否正在攻击
        Collider2D[] attackColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);//攻击范围碰撞检测盒
        foreach (Collider2D collider in attackColliders)//对于进入攻击范围的物体，进行攻击
        {
            if (collider.gameObject.CompareTag("Red") && gameObject.CompareTag("Blue"))
            {
                isAttacking = true;
                if (CanAttack()) // 检查是否可以攻击
                {
                    Attack(collider.gameObject);//有时正在攻击的地方单位死亡，重新获取需要时间，避免发呆，随机攻击
                    lastAttackTime = Time.time; // 更新上次攻击时间
                }
            }
            else if (collider.gameObject.CompareTag("Blue") && gameObject.CompareTag("Red"))//另一方的攻击逻辑
            {
                isAttacking = true;
                if (CanAttack()) // 检查是否可以攻击
                {
                    Attack(collider.gameObject);
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
    }
    bool CanAttack()
    {
        return Time.time - lastAttackTime > attackInterval; // 检查是否超过攻击间隔
    }
}
