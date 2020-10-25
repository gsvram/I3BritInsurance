using System;
using System.Linq;
using BrInCalcTest.BO;
using BrInCalcTest.BO.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrInsTestUnits
{
    [TestClass]
    public class ProccessUnitTest
    {
        public IProcessFile GetProcessFileInstances() => new ProcessFile();

        [DataRow("add 1\r\nsubtract 2\r\nmultiply -3\r\napply 9", new string[] { "add 1", "subtract 2", "multiply -3", "apply 9" }, 4)]
        [DataTestMethod]
        public void TestLines_CompareArrayLengthandArray_Pass(string strContent, string[] returnStrAry, int expectedLenght)
        {

            var lines = GetProcessFileInstances().GetLinesFromFile(strContent);
            Assert.AreEqual(returnStrAry.Length, lines.Length);
            CollectionAssert.AreEqual(returnStrAry, lines);

        }

        [DataRow("add 1\r\n\r\nsubtract 2\r\nmultiply -3\r\napply 9", new string[] { "add 1", "", "subtract 2", "multiply -3", "apply 9" }, 0)]
        [DataTestMethod]
        public void TestLines_CompareArrayLengthandArray_Fail(string strContent, string[] returnStrAry, int expectedLenght)
        {

            var lines = GetProcessFileInstances().GetLinesFromFile(strContent);
            Assert.AreNotEqual(returnStrAry.Length, lines.Length);
            CollectionAssert.AreNotEqual(returnStrAry, lines);

        }


        [DataRow("add 1\r\n\r\nsubtract 2\r\nmultiply -3\r\napply 9", true)]
        [DataTestMethod]
        public void TestLines_SplitOperatorsandValues_CheckSecondVariable_Is_Int_Pass(string strContent, bool expected)
        {

            var lines = GetProcessFileInstances().GetLinesFromFile(strContent);

            var listRwFileVariableses = GetProcessFileInstances().SplitOperatorsandValues(lines);
            var isAllValid = listRwFileVariableses.Any(x => x.IsSecondVariableInt);
            Assert.AreEqual(expected, isAllValid);
        }


        [DataRow("add 1\r\n\r\nsubtract 2\r\nmultiply -three\r\napply 9", false)]
        [DataTestMethod]
        public void TestLines_SplitOperatorsandValues_CheckSecondVariable_Is_NotInt_Fail(string strContent, bool expected)
        {

            var lines = GetProcessFileInstances().GetLinesFromFile(strContent);

            var listRwFileVariables = GetProcessFileInstances().SplitOperatorsandValues(lines);
            var isAllValid = listRwFileVariables.Any(x => x.IsSecondVariableInt);
            Assert.AreNotEqual(expected, isAllValid);
        }

    }
}
