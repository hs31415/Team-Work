using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoScene : MonoBehaviour
{
    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void InfoScene2()
    {
        SceneManager.LoadScene("InfoScene2");
    }
}
