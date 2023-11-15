using System.IO;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using WebSocketSharp;

public class WebSocketClient : MonoBehaviour
{
    private WebSocketSharp.WebSocket ws;

    private void Start()
    {
        // 创建WebSocket连接
        ws = new WebSocketSharp.WebSocket("ws://your-server-address");

        // 注册事件处理程序
        ws.OnOpen += OnWebSocketOpen;
        ws.OnMessage += OnWebSocketMessage;
        ws.OnError += OnWebSocketError;
        ws.OnClose += OnWebSocketClose;

        // 启动连接
        ws.Connect();
    }

    private void OnDestroy()
    {
        // 关闭WebSocket连接
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
        }
    }

    private void OnWebSocketOpen(object sender, System.EventArgs e)
    {
        Debug.Log("WebSocket connection opened.");

        // 在连接成功后可以发送消息到服务器
        ws.Send("Hello server!");
    }

    private void OnWebSocketMessage(object sender, WebSocketSharp.MessageEventArgs e)
    {
        Debug.Log("Received message from server: " + e.Data);

        // 处理从服务器接收的消息
        // 根据消息内容执行相应的逻辑

        // 解析接收到的JSON数据
        MyDataObject cardData = JsonUtility.FromJson<MyDataObject>(e.Data);

        // 使用解析后的卡牌数据执行相关操作
        string name = cardData.name;
        string type = cardData.type;
        float attack = float.Parse(cardData.attack);
        float defense = float.Parse(cardData.defense);
        int attackRange = int.Parse(cardData.attack_range);
        int moveSpeed = int.Parse(cardData.move_speed);
        int cost = int.Parse(cardData.cost);
        string description = cardData.description;

        // 执行根据卡牌数据执行的逻辑
        // ...

    }

    private void OnWebSocketError(object sender, WebSocketSharp.ErrorEventArgs e)
    {
        Debug.LogError("WebSocket error: " + e.Message);
    }

    private void OnWebSocketClose(object sender, CloseEventArgs e)
    {
        Debug.Log("WebSocket connection closed with code: " + e.Code);
    }

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
}