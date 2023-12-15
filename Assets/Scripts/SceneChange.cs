using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jump : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void InfoScene()
    {
        SceneManager.LoadScene("InfoScene");
    }
    public void StartScene()
    {
        Debug.Log(123);
    }
}
