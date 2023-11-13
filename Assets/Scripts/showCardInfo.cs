using UnityEngine;
using TMPro;

public class SquareClickHandler : MonoBehaviour
{
    public TextAsset jsonFile; // 引用JSON文件的TextAsset
    public GameObject prefab; // 预设体对象
    public Canvas canvas; // Canvas对象的引用
    private MyDataObject data;//卡牌信息
    void Start()
    {
        // 加载并解析JSON文件内容
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            data = JsonUtility.FromJson<MyDataObject>(jsonText);

            // 输出JSON数据到控制台
            Debug.Log("Name: " + data.name);
            Debug.Log("attack: " + data.attack);
            Debug.Log("defense: " + data.defense);
            Debug.Log("attack_range: " + data.attack_range);
            Debug.Log("move_speed: " + data.move_speed);
            Debug.Log("cost: " + data.cost);
            Debug.Log("description: " + data.description);
        }
    }
    /*
    json文件格式：
    {
        "name": 
        "attack": 
        "defense": 
        "attack_range": 
        "move_speed": 
        "cost": 
        "description": 
    }
    */

    [System.Serializable]
    public class MyDataObject
    {
        public string name;
        public string attack;
        public string defense;
        public string attack_range;
        public string move_speed;
        public string cost;
        public string description;
    }
    void Update()
    {
        // 检测鼠标左键是否被点击
        if (Input.GetMouseButtonDown(0))
        {
            // 将鼠标点击位置转换为世界坐标
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 检测点击是否在方块的碰撞框内
            Collider2D hit = Physics2D.OverlapPoint(clickPos);
            if (hit != null && hit.gameObject == gameObject)
            {
                // 输出提示信息到控制台
                Debug.Log("检测到点击");
                // 实例化预设体
                GameObject instance = Instantiate(prefab);

                // 设置预设体中展示的信息
                TextMeshProUGUI textMesh = instance.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null)
                {
                    textMesh.text = "Name: " + data.name;
                }
            }
        }
    }
}
