using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class SceneTest
    {
        [Test]
        public void LoadScene_Success()
        {
            string scenePath = "Assets/Scenes/GameScene.unity";

            // 加载场景
            SceneManager.LoadScene(scenePath);

            // 输出加载的场景名称
            Debug.Log("Loaded scene name: " + SceneManager.GetActiveScene().name);

            // 断言场景是否有效
            Assert.IsTrue(SceneManager.GetActiveScene().IsValid(), $"Failed to load scene: {scenePath}");
        }

        [Test]
        public void FindBaseObjectsInScene()
        {
            string scenePath = "Assets/Scenes/GameScene.unity";

            // 加载场景
            SceneManager.LoadScene(scenePath);

            Assert.IsTrue(SceneManager.GetActiveScene().IsValid(), $"Failed to load scene: {scenePath}");

            // 输出所有 GameObject
            var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                Debug.Log("GameObject name: " + obj.name);
            }

            // 断言场景是否有效
            Assert.IsTrue(SceneManager.GetActiveScene().IsValid(), $"Failed to load scene: {scenePath}");
        }

        [Test]
        public void OpenScene_Success()
        {
            string scenePath = "Assets/Scenes/StartScene.unity";

            // 加载场景
            SceneManager.LoadScene(scenePath);

            // 输出所有 GameObject
            var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                Debug.Log("GameObject name: " + obj.name);
            }

            // 断言场景是否有效
            Assert.IsTrue(SceneManager.GetActiveScene().IsValid(), $"Failed to load scene: {scenePath}");
        }
    }
}
