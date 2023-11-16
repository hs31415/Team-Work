using UnityEngine;
using TMPro;

public class SquareClickHandler : MonoBehaviour
{
    public TextAsset jsonFile; // 引用JSON文件的TextAsset
    public GameObject prefab; // 预设体对象
    public Canvas canvas; // Canvas对象的引用
    private MyDataObject data;//卡牌信息
    private GameObject instance; // 声明instance为类级别变量
    private bool isInstanceCreated = false; // 增加一个布尔变量用于跟踪预设体是否已经生成
    void Start()
    {
        // 加载并解析JSON文件内容
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            data = JsonUtility.FromJson<MyDataObject>(jsonText);

            // 输出JSON数据到控制台
            Debug.Log("Name: " + data.name);
            Debug.Log("Type: " + data.type);
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
        public string type;
        public string attack;
        public string defense;
        public string attack_range;
        public string move_speed;
        public string cost;
        public string description;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(clickPos);
            if (hit != null && hit.gameObject == gameObject)//如果点击的是卡牌
            {
                Debug.Log("检测到点击");
                if (!isInstanceCreated) // 检查预设体是否已经生成
                {
                    Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);
                    Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);

                    instance = Instantiate(prefab, worldCenter, Quaternion.identity);

                    TextMeshProUGUI textMesh = instance.GetComponentInChildren<TextMeshProUGUI>();
                    if (textMesh != null)

                    {
                        if (data.type == "兵种") 
                        {
                            textMesh.text =
                                "卡牌名称: " + data.name +"(" + data.type + ")"
                                + "\n攻击力: " + data.attack 
                                + "\n防御力: " + data.defense
                                + "\n攻击范围: " + data.attack_range 
                                + "\n移动速度: " + data.move_speed 
                                + "\n费用: " + data.cost 
                                + "\n描述: " + data.description;
                        }
                        else if (data.type == "法术")
                        {
                            textMesh.text =
                                "卡牌名称: " + data.name + "(" + data.type + ")"
                                + "\n攻击力: " + data.attack
                                + "\n防御力: " + data.defense
                                + "\n攻击范围: " + data.attack_range
                                + "\n移动速度: " + data.move_speed
                                + "\n费用: " + data.cost
                                + "\n描述: " + data.description;
                        }
                            
                        else if (data.type == "建筑")
                        {
                            textMesh.text =
                                "卡牌名称: " + data.name + "(" + data.type + ")"
                                + "\n攻击力: " + data.attack
                                + "\n防御力: " + data.defense
                                + "\n攻击范围: " + data.attack_range                               
                                + "\n费用: " + data.cost
                                + "\n描述: " + data.description;
                        }

                    }
                    RectTransform rectTransform = instance.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        // 将预设体的posZ设置为-9
                        rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, -9f);
                    }

                    isInstanceCreated = true; // 设置预设体已经生成的状态为true
                }
            }
            else if (hit != null && hit.gameObject.name == "Square Variant(Clone)")
            {

            }
            else
            {
                if (instance != null)
                {
                    Destroy(instance);
                    isInstanceCreated = false; // 如果预设体被销毁，则将状态重置为false
                }
            }
        }
    }
}
