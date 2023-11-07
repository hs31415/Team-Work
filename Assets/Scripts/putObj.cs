using UnityEngine;

public class SpawnAndMove : MonoBehaviour
{
    public GameObject prefab; // 预设体
    public Transform target; // 目标位置

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 当鼠标左键点击时
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 将鼠标点击位置转换为世界坐标
            spawnPosition.z = 0; // 确保 z 轴为 0

            GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity); // 生成预设体

            AutoMoveToTarget moveScript = newObject.AddComponent<AutoMoveToTarget>(); // 添加自动移动脚本
            moveScript.target = target; // 设置移动目标
        }
    }
}
