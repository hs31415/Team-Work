using UnityEngine;
using System;

public class ObjectHealth : MonoBehaviour
{
    public event Action<float> OnHealthChanged; // 定义生命值变化事件
    public float CurrentHealth { get; private set; } // 使用属性访问currentHealth
    public float initialHealth = 3; // 初始生命值
    private float currentHealth; // 当前生命值

    void Start()
    {
        currentHealth = initialHealth; // 初始化当前生命值
        UpdateHealth(); // 初始化时更新生命值
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
}
