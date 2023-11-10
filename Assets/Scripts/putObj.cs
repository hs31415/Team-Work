using UnityEngine;

public class SpawnAndMove : MonoBehaviour
{
    public GameObject prefab; // 预设体
    public Transform target; // 目标位置
    public void SetPrefab(GameObject newPrefab)
    {
        prefab = newPrefab;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 当鼠标左键点击时
        {
            if (prefab.name == "obj0")
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 从鼠标点击位置发射一条射线
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject == this.gameObject) // 检查点击的对象是否是当前挂载该脚本的对象
                {
                    Vector3 spawnPosition = hitInfo.point; // 获取点击位置
                    spawnPosition.z = 0; // 确保 z 轴为 0

                    GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity); // 生成预设体

                    // 根据当前脚本所挂载的对象的标签，为生成的预设体添加对应的标签
                    if (this.gameObject.CompareTag("blueArea"))
                    {
                        newObject.tag = "Blue";
                    }
                    else if (this.gameObject.CompareTag("redArea"))
                    {
                        newObject.tag = "Red";
                    }

                    AutoMoveToTarget moveScript = newObject.AddComponent<AutoMoveToTarget>(); // 添加自动移动脚本
                    moveScript.target = target; // 设置移动目标
                }
            }
        }
    }
}
