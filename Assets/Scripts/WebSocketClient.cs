using System.Collections;
using UnityEngine;
using NativeWebSocket;
using System.Threading.Tasks;
using System;

public class Connection : MonoBehaviour
{
    WebSocket websocket;

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        ConnectToWebSocket();
    }

    async void ConnectToWebSocket()
    {
        websocket = new WebSocket("ws://10.133.28.55:60001/game/1");

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
            Debug.Log("OnMessage!");
            SendWebSocketMessage();
        };

        try
        {
            await websocket.Connect();
            // After connecting, start sending messages
            InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);
        }
        catch (Exception e)
        {
            Debug.LogError("Exception when connecting: " + e.Message);
        }
    }

    




    void Update()
    {
        
    }

    void SendWebSocketMessage()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            // Sending messages
            websocket.SendText("Hello from the unity client!");
        }
    }

    private void OnApplicationQuit()
    {
        if (websocket != null)
        {
            websocket.Close().Wait(); // 等待连接关闭
        }
    }

}
