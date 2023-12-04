using System.Collections;
using UnityEngine;
using NativeWebSocket;
using System.Threading.Tasks;
using System;

public class Connection : MonoBehaviour
{
    WebSocket websocket;
    string roomID;
    string userID;

    // Start is called before the first frame update
    async void Start()
    {
        DontDestroyOnLoad(gameObject);
        await ConnectToWebSocket();
    }


    async Task ConnectToWebSocket()
    {
        websocket = new WebSocket("ws://10.194.4.153:60001/game");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            // 解析从服务器接收的消息
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received Message: " + message);
            HandleMessage(message);
        };

        try
        {
            await websocket.Connect();
        }
        catch (Exception e)
        {
            Debug.LogError("Exception when connecting: " + e.Message);
        }
    }

    void HandleMessage(string message)
    {
        // 解析 JSON 消息
        var jsonMessage = JsonUtility.FromJson<ServerMessage>(message);

        // 根据消息类型执行操作
        switch (jsonMessage.type)
        {
            case "UserInfo":
                // 保存分配的房间 ID 和用户 ID
                roomID = jsonMessage.room_id;
                userID = jsonMessage.user_id;
                Debug.Log("Assigned Room ID: " + roomID + " User ID: " + userID);
                break;
            case "Error":
                // 处理错误信息
                Debug.LogError("Error: " + jsonMessage.message);
                break;
                // 添加其他消息类型的处理逻辑...
        }
    }

    // 用于解析 JSON 消息的辅助类
    [Serializable]
    class ServerMessage
    {
        public string type;
        public string room_id;
        public string user_id;
        public string message;
        // 可添加更多字段以匹配服务器发送的消息结构
    }

    // Update is not needed for WebGL since the library handles it internally

    async Task SendWebSocketMessage(string message)
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            try
            {
                // 异步发送消息并等待其完成
                await websocket.SendText(message);
            }
            catch (Exception e)
            {
                Debug.LogError("Error when sending message: " + e.Message);
                // 可以在这里添加重连逻辑
            }
        }
    }


    async Task CloseWebSocket()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            try
            {
                // 异步关闭连接并等待其完成
                await websocket.Close();
            }
            catch (Exception e)
            {
                // 在这里处理关闭连接时的任何异常
                Debug.LogError("Error when closing WebSocket: " + e.Message);
            }
        }
    }

    void OnDestroy()
    {
        // 当对象被销毁时关闭 WebSocket
        CloseWebSocket().Wait(); // 确保异步操作完成
    }
}
