using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoScene : MonoBehaviour
{
    public void GoToInfoScene1()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GoToInfoScene2()
    {
        SceneManager.LoadScene("Info2");
    }
}
