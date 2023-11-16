using UnityEngine;
using TMPro;
public class SpawnAndMove : MonoBehaviour
{
    public GameObject prefab; // 预设体
    public Transform target; // 目标位置
    public TextMeshProUGUI textObject; // 通过在编辑器中拖动来设置Text对象
    public int Cost;
    public void SetPrefab(GameObject newPrefab)
    {
        prefab = newPrefab;
    }
    void Update()
    {
        if (textObject != null)
        {
            // 获取Text对象上挂载的CostSystem脚本
            CostSystem costSystem = textObject.GetComponent<CostSystem>();
            if (costSystem != null)
            {
                Cost = costSystem.currentCost; // 获取CostSystem脚本中的cost值
                // 在这里使用费用进行其他操作
            }
        }
        if (Input.GetMouseButtonDown(0)) // 当鼠标左键点击时
        {
            if (prefab.name == "obj0")
            {
                return;
            }
            ObjectState objectState = prefab.GetComponent<ObjectState>(); // 获取预设体上的ObjectState组件
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 从鼠标点击位置发射一条射线
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject == this.gameObject && Cost >= objectState.cost) // 检查点击的对象是否是当前挂载该脚本的对象，以及当前费用是否足够
                {
                    Vector3 spawnPosition = hitInfo.point; // 获取点击位置
                    spawnPosition.z = 0; // 确保 z 轴为 0

                    GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity); // 生成预设体

                    CostSystem costSystem = textObject.GetComponent<CostSystem>();
                    costSystem.currentCost -= objectState.cost;
                    costSystem.UpdateCostText();
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
