using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PictureFrameTests
    {
        [UnityTest]
        public void SetupTextures() {
            //Arrange
            Texture[] fiveTexures = new Texture[5];

            //Act

            //Assert
        }
            

        // A Test behaves as an ordinary method
        [Test]
        public void PictureFrameTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PictureFrameTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
