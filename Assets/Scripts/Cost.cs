using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostSystem : MonoBehaviour
{
    public TextMeshProUGUI costText; // 如果使用的是TextMeshPro组件，使用TextMeshProUGUI类型

    public int currentCost = 0;
    private float costIncreaseRate = 1.0f;
    private const int maxCost = 10;
    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f)
        {
            timer = 0.0f;
            IncreaseCost();
            UpdateCostText();
        }
    }

    void IncreaseCost()
    {
        if (currentCost < maxCost)
        {
            currentCost++;
        }
    }

    public void UpdateCostText()
    {
        costText.text = "费用: \n" + currentCost.ToString(); // 更新UI Text显示的文本内容
    }
}
