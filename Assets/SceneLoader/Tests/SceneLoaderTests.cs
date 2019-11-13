using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SceneLoadingSystem;
using System.IO;

namespace Tests
{
    public class SceneLoaderTests
    {
        [UnityTest]
        public IEnumerator GameObject_WithHintmanager_Will_Create_a_Hint_Json_File()
        {
            var go = new GameObject("HintGameObject");
            var hm = go.AddComponent<HintManager>();
            var fn = "testHint.json";

            hm.SetFileName(fn);

            yield return new WaitForFixedUpdate();

            string path = $"{Application.dataPath}/Resources/{fn}";

            Assert.IsTrue(File.Exists(path));
        }
        // A Test behaves as an ordinary method
        [Test]
        public void SceneLoaderTestsSimplePasses()
        {
            // Use the Assert class to test conditions
            Assert.Pass("Pass Suite.");
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator SceneLoaderTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            Assert.Pass("test with enumerator.");
            yield return null;
        }
    }
}
