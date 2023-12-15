using UnityEngine;
using TMPro;

public class ShowWinner : MonoBehaviour
{
    public GameObject background;
    public GameObject blueBase;
    public GameObject redBase;
    public GameObject victoryPrefab;
    public Transform canvasTransform;
    private bool gameEnded = false;

    private void Update()
    {
        if (gameEnded)
        {
            return;
        }
        float blueHealth = 0;
        float redHealth = 0;

        ObjectState blueObjectState = blueBase.GetComponent<ObjectState>();
        ObjectState redObjectState = redBase.GetComponent<ObjectState>();

        if (blueObjectState != null)
        {
            blueHealth = blueObjectState.getHealth();
        }

        if (redObjectState != null)
        {
            redHealth = redObjectState.getHealth();
        }

        if (blueHealth <= 0)
        {
            EndGame("红方");
        }
        else if (redHealth <= 0)
        {
            EndGame("蓝方");
        }
    }

    private void EndGame(string winner)
    {
        gameEnded = true;
        // 销毁所有标签与失败一方基地相同的物体
        if (winner == "红方")
        {
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(blueBase.tag);
            foreach (GameObject obj in objectsToDestroy)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
        }
        else if (winner == "蓝方")
        {
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(redBase.tag);
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }
        }
        // 销毁所有标签与失败一方基地相同的物体

        // 弹出胜利窗口
        Debug.Log(winner + "胜利");
        // 实例化胜利窗口预制体
        GameObject victoryObj = Instantiate(victoryPrefab, canvasTransform);
        TextMeshProUGUI victoryText = victoryObj.GetComponentInChildren<TextMeshProUGUI>();

        // 设置胜利信息
        if (victoryText != null)
        {
            victoryText.text = winner + "获胜";
        }

        // 设置预制体位置居中
        RectTransform victoryTransform = victoryObj.GetComponent<RectTransform>();
        if (victoryTransform != null)
        {
            victoryTransform.position = background.transform.position;
        }

    }

}
