using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogueManager.Controllers;

namespace CatalogueManager.Tests
{
    [TestClass]
    public class MathsTests
    {
        [TestMethod]
        public void TestCalcPower()
        {
            PrivateType privateTypeObject = new PrivateType(typeof(Maths));
            object obj = privateTypeObject.InvokeStatic("CalcPower", 2, 3);
            Assert.AreEqual(8, (int)obj);
        }
    }
}
