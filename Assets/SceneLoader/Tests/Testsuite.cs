using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Testsuite
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestsuiteSimplePasses()
        {
            // Use the Assert class to test conditions
            Assert.Pass("TesSuiteSimple Pass OK.");
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestsuiteWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            Assert.Pass("Unity Test with Enumerator Pass.");
            yield return null;
        }
    }
}
