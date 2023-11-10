using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    public void SampleScene() {
        SceneManager.LoadScene("SampleScene");
    }
    public void InfoScene() {
        SceneManager.LoadScene("InfoScene");
    }
}