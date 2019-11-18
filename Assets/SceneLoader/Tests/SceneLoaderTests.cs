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
        public void SceneLoader_HintStructure_Class_Create_and_Add_to_ListTest()
        {
            var hint = new Hint();
            hint.duration = 2.0f;
            hint.text = "TestHint";
            var hints = new Hints();
            hints.items.Add(hint);

            Assert.IsNotEmpty(hints.items);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator SceneLoader_FadOut_Will_Be_TriggerdTest()
        {
            var go = new GameObject("LevelTransitionGameObject");
            var anim = go.AddComponent<Animator>();
            var transition = go.AddComponent<LevelTransition>();
            
            anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/LevelTransitionController");
            
            anim.SetTrigger("FadeOut");    
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(anim.GetCurrentAnimatorStateInfo(0).IsName("Fade_out"));
        }
    }
}
