using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public int initialHealth = 3; // 初始生命值
    private int currentHealth; // 当前生命值

    void Start()
    {
        currentHealth = initialHealth; // 初始化当前生命值
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Blue") && gameObject.CompareTag("Red"))
        {
            Debug.Log("开始碰撞");
            DecreaseHealth();
        }
        else if (collision.gameObject.CompareTag("Red") && gameObject.CompareTag("Blue"))
        {
            Debug.Log("开始碰撞");
            DecreaseHealth();
        }
    }

    void DecreaseHealth()
    {
        currentHealth--; // 生命值减一

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // 生命值归零时销毁对象
        }
    }
}
