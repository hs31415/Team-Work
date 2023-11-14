using UnityEngine;
using UnityEngine.SceneManagement;

public class UpDownpage2 : MonoBehaviour
{
    public void GoToInfoScene1()
    {
        SceneManager.LoadScene("InfoScene");
    }

    public void GoToInfoScene2()
    {
        SceneManager.LoadScene("NextScene");
    }
}
