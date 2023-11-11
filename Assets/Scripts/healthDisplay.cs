using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public ObjectState objectHealth; // 对应的ObjectHealth组件
    public GameObject HealthBarBackground; // 血条预设体
    private GameObject healthBarInstance; // 实例化的血条对象

    void Start()
    {

        objectHealth.OnHealthChanged += UpdateHealthDisplay; // 在生命值变化事件中订阅更新显示方法

        // 实例化血条预制件，并设置其父对象为当前物体
        healthBarInstance = Instantiate(HealthBarBackground, Vector3.zero, Quaternion.identity, transform);
        healthBarInstance.transform.localPosition = new Vector3(0, -1f, 0);

    }

    void OnDestroy()
    {

        objectHealth.OnHealthChanged -= UpdateHealthDisplay; // 取消订阅生命值变化事件

    }

    void UpdateHealthDisplay(float health)
    {
        if (health == objectHealth.initialHealth)
        {
            return;
        }
        Debug.Log("healthBarInstance: " + healthBarInstance);
        Debug.Log("生命值改变");
        float healthPercentage = (float)health / objectHealth.initialHealth; // 计算当前生命值的百分比
        Transform fillTransform = healthBarInstance.transform.Find("healthFill"); // 获取血条实例中的填充长方形对象
        Debug.Log("fillTransform: " + fillTransform);
        float initialWidth = 1; // 获取填充长方形对象的初始宽度
        float newWidth = initialWidth * healthPercentage; // 根据生命值百分比计算新的宽度
        Vector3 scale = fillTransform.localScale;
        scale.x = 1 * healthPercentage; // 根据生命值百分比调整宽度
        fillTransform.localScale = scale;

        // 调整位置以实现向左边缩小
        float deltaX = (initialWidth - initialWidth * healthPercentage) / 2; // 计算需要移动的距离
        fillTransform.localPosition = new Vector3(-deltaX, 0, 0); // 根据计算出的距离向左移动
    }
}
