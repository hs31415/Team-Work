using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
namespace Tests
{
    public class SceneTest
    {
        [Test]
        public void FindBaseObjectsInScene()
        {
            string scenePath = "Assets/Scenes/GameScene.unity";

            // 加载场景
            EditorSceneManager.OpenScene(scenePath);

            // 输出所有 GameObject
            var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                Debug.Log("GameObject name: " + obj.name);
            }

            // 断言场景是否有效
            Assert.IsTrue(EditorSceneManager.GetActiveScene().IsValid(), $"Failed to load scene: {scenePath}");

            // 从场景中寻找 blueBase 和 redBase
            GameObject blueBase = GameObject.Find("blueBase");
            GameObject redBase = GameObject.Find("redBase");

            // 断言物体是否存在
            Assert.IsNotNull(blueBase, "blueBase not found in scene!");
            Assert.IsNotNull(redBase, "redBase not found in scene!");


        }
    }
}
