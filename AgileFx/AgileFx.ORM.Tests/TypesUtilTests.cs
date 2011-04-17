using AgileFx.ORM.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

namespace AgileFx.ORM.Tests.TypesUtilTests
{
    [TestClass()]
    public class TypesUtilTests
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetGenericArgumentForBaseType
        ///</summary>

        [TestMethod()]
        public void GetGenericArgumentForBaseType_Simple()
        {
            Type finalType = typeof(ArgForGetGenericArgumentForBaseType);
            Type specificBaseType = typeof(IEnumerable<>); 
            Type expected = typeof(int); 
            Type actual;
            actual = TypesUtil.GetGenericArgumentForBaseType(finalType, specificBaseType);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetGenericArgumentForBaseType_ExceptionOnNonGenericInput()
        {
            Type finalType = typeof(ArgForGetGenericArgumentForBaseType);
            Type specificBaseType = typeof(IEnumerable);
            Type expected = typeof(int);
            Type actual;
            actual = TypesUtil.GetGenericArgumentForBaseType(finalType, specificBaseType);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetGenericArgumentForBaseType_BaseClass1()
        {
            Type finalType = typeof(ArgForGetGenericArgumentForBaseType);
            Type specificBaseType = typeof(ArgForGetGenericArgumentForBaseTypeBase<>);
            Type expected = typeof(string);
            Type actual;
            actual = TypesUtil.GetGenericArgumentForBaseType(finalType, specificBaseType);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetGenericArgumentForBaseType_BaseClass2()
        {
            Type finalType = typeof(GenericArgForGetGenericArgumentForBaseType<long>);
            Type specificBaseType = typeof(ArgForGetGenericArgumentForBaseTypeBase<>);
            Type expected = typeof(long);
            Type actual;
            actual = TypesUtil.GetGenericArgumentForBaseType(finalType, specificBaseType);
            Assert.AreEqual(expected, actual);
        }
    }
}
