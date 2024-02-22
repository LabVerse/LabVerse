using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SimpleTestScript
{
    [Test]
    public void SimpleTest()
    {
        // Use the Assert class to assign your conditions:
        Assert.AreEqual(1, 1);
        Assert.IsTrue(true);
        Assert.Greater(2, 1);
    }
}