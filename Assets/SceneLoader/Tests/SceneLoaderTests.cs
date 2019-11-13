using System;
using System.IO;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SceneLoadingSystem;

namespace Tests
{
    public class SceneLoaderTests
    {
        [UnityTest]
        public IEnumerator GameObject_WithHintmanager_Will_Create_a_Hint_Json_FileTest()
        {
            var go = new GameObject("HintGameObject");
            var hintManager = go.AddComponent<HintManager>();

            long ticks = DateTime.Now.Ticks;  // unique identifier with ticks
            var fn = $"testHint{ticks}.json";
            hintManager.Filename = fn;

            yield return new WaitForFixedUpdate();

            var path = $"{Application.dataPath}/Resources/{fn}";
            var fileExists = File.Exists(path);
            Assert.IsTrue(fileExists);
            if (fileExists)
            {
                File.Delete(path);
            }
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
