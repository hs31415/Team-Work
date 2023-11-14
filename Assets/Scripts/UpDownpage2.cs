using UnityEngine;
using UnityEngine.SceneManagement;

public class UpDownpage2 : MonoBehaviour
{
    public void InfoScene()
    {
        SceneManager.LoadScene("InfoScene");
    }

    public void NextScene()
    {
        SceneManager.LoadScene("NextScene");
    }
}
