using UnityEngine;
using WebSocketSharp;

public class WebSocketClientTest : MonoBehaviour
{
    private WebSocket ws;
    private string roomID = "1"; // 默认房间号为1，您可以根据需要进行修改

    private void Awake()
    {
        // 根据输入的房间号构建WebSocket连接地址
        string url = "ws://10.133.18.99:60001"; // 假设玩家标识为"player1"

        // 创建WebSocket连接
        ws = new WebSocket(url);

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

    private void OnWebSocketMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("Received message from server: " + e.Data);

        // 处理从服务器接收的消息
        // 根据消息内容执行相应的逻辑
    }

    private void OnWebSocketError(object sender, ErrorEventArgs e)
    {
        Debug.LogError("WebSocket error: " + e.Message);
    }

    private void OnWebSocketClose(object sender, CloseEventArgs e)
    {
        Debug.Log("WebSocket connection closed with code: " + e.Code);
    }
}