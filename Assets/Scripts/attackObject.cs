using UnityEngine;

public class AttackObject : MonoBehaviour
{
    private GameObject target;
    private float moveSpeed = 5f; // 移动速度

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target != null)
        {
            // 计算朝向目标的方向
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // 移动攻击物体朝向目标
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject); // 如果目标为空，销毁攻击物体
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            // 在这里处理攻击命中目标后的逻辑
            ObjectState healthScript = target.GetComponent<ObjectState>();
            if (healthScript != null)
            {
                healthScript.DecreaseHealth();
            }

            Destroy(gameObject); // 销毁攻击物体
        }
    }
}
