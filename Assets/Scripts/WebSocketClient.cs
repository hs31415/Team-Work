using UnityEngine
using WebSocketSharp

public class WebSocketClientTest : MonoBehaviour
{
    private WebSocket ws
    private string roomID = "1" // &#x9ED8;&#x8BA4;&#x623F;&#x95F4;&#x53F7;&#x4E3A;1&#xFF0C;&#x60A8;&#x53EF;&#x4EE5;&#x6839;&#x636E;&#x9700;&#x8981;&#x8FDB;&#x884C;&#x4FEE;&#x6539;

    private void Awake()
    {
        // &#x6839;&#x636E;&#x8F93;&#x5165;&#x7684;&#x623F;&#x95F4;&#x53F7;&#x6784;&#x5EFA;WebSocket&#x8FDE;&#x63A5;&#x5730;&#x5740;
        string url = "ws://10.133.18.99:60001" // &#x5047;&#x8BBE;&#x73A9;&#x5BB6;&#x6807;&#x8BC6;&#x4E3A;"player1"

        // &#x521B;&#x5EFA;WebSocket&#x8FDE;&#x63A5;
        ws = new WebSocket(url)

        // &#x6CE8;&#x518C;&#x4E8B;&#x4EF6;&#x5904;&#x7406;&#x7A0B;&#x5E8F;
        ws.OnOpen += OnWebSocketOpen
        ws.OnMessage += OnWebSocketMessage
        ws.OnError += OnWebSocketError
        ws.OnClose += OnWebSocketClose

        // &#x542F;&#x52A8;&#x8FDE;&#x63A5;
        ws.Connect()
    }
    private void OnDestroy()
    {
        // &#x5173;&#x95ED;WebSocket&#x8FDE;&#x63A5;
        if (ws != null && ws.IsAlive)
        {
            ws.Close()
        }
    }

    private void OnWebSocketOpen(object sender, System.EventArgs e)
    {
        Debug.Log("WebSocket connection opened.")

        // &#x5728;&#x8FDE;&#x63A5;&#x6210;&#x529F;&#x540E;&#x53EF;&#x4EE5;&#x53D1;&#x9001;&#x6D88;&#x606F;&#x5230;&#x670D;&#x52A1;&#x5668;
        ws.Send("Hello server!")
    }

    private void OnWebSocketMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("Received message from server: " + e.Data)

        // &#x5904;&#x7406;&#x4ECE;&#x670D;&#x52A1;&#x5668;&#x63A5;&#x6536;&#x7684;&#x6D88;&#x606F;
        // &#x6839;&#x636E;&#x6D88;&#x606F;&#x5185;&#x5BB9;&#x6267;&#x884C;&#x76F8;&#x5E94;&#x7684;&#x903B;&#x8F91;
    }

    private void OnWebSocketError(object sender, ErrorEventArgs e)
    {
        Debug.LogError("WebSocket error: " + e.Message)
    }

    private void OnWebSocketClose(object sender, CloseEventArgs e)
    {
        Debug.Log("WebSocket connection closed with code: " + e.Code)
    }
}