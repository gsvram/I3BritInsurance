using System;
using BrInCalcTest.BO;
using BrInCalcTest.BO.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrInsTestUnits
{
    [TestClass]
    public class CalcUnitTest
    {

        public ICalculate GetProcessFileInstances() => new Calculate();


        [DataRow("Add",2,3,5)]
        [DataRow("subtract", 2, 3, 1)]
        [DataRow("multiply", 2, 3, 6)]
        [DataRow("divide", 2, 4, 2)]
        [DataTestMethod]
        public void OperatorTest_ByPassing_Values(string @operator,int input1,int input2,int expected)
        {

           var returnVal= GetProcessFileInstances().Operator(@operator, input1, input2);
           Assert.AreEqual(expected,returnVal);

        }
    }
}
