using BrInCalcTest.BO;
using BrInCalcTest.BO.Interface;
using BrInCalcTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrInsTestUnits
{
    [TestClass]
    public class FileContentValidatorUnitTest
    {
        public IProcessFile GetProcessFileInstances() => new ProcessFile();
        public IFileContentValidator GetFileContentValidator() => new FileContentValidator();

        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "apply 9" }, true)]
        [DataRow(new[] { "add a", "subtract 2", "multiply -3", "apply 9" }, false)]
        [DataTestMethod]
        public void TestValidate_SecondPartConvert_ToInt_Pass( string[] returnStrAry, bool expected)
        {
            var list = GetProcessFileInstances().SplitOperatorsandValues(returnStrAry);

            var isValid = GetFileContentValidator().ValidateAllSecondPartCanConvertToInt(list);
           
            Assert.AreEqual(expected,isValid);

        }


        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "apply 9" }, true)]
        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "multiply 9" }, false)]
        [DataTestMethod]
        public void TestValidate_IsApply_Exists(string[] returnStrAry, bool expected)
        {
            var list = GetProcessFileInstances().SplitOperatorsandValues(returnStrAry);
            var fd = new FileDetails {AllFileVariables = list};
            var isValid = GetFileContentValidator().ValidateIsApplyExists(fd);

            Assert.AreEqual(expected, isValid);

        }

        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "apply 9" }, true)]
        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "divide 9" }, true)]
        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "abc 9" }, false)]
        [DataTestMethod]
        public void TestValidate_ValidateOperators_Valid(string[] returnStrAry, bool expected)
        {
            var list = GetProcessFileInstances().SplitOperatorsandValues(returnStrAry);
            
            var isValid = GetFileContentValidator().ValidateOperatorsValid(list);

            Assert.AreEqual(expected, isValid);

        }

        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "apply 9" }, true)]
        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "divide 9" }, true)]
        [DataRow(new[] { "add 1"}, false)]
        [DataTestMethod]
        public void TestValidate_ValidateTotalLinesGrtThenTwo_Valid(string[] returnStrAry, bool expected)
        {
            var list = GetProcessFileInstances().SplitOperatorsandValues(returnStrAry);

            var isValid = GetFileContentValidator().ValidateTotalLinesGreaterThanTwo(list);

            Assert.AreEqual(expected, isValid);

        }

        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "apply 9" }, true)]
        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "divide 9" }, false)]
        [DataRow(new[] { "add 1", "subtract 2", "multiply -3", "apply 9","","","" }, true)]
        [DataRow(new[] { "add 1","apply0" }, false)]
        [DataRow(new[] { "apply 0" }, true)]
        [DataTestMethod]
        public void TestValidate_ValidateLastLineContains_Apply(string[] returnStrAry, bool expected)
        {
            
            var isValid = GetFileContentValidator().ValidateApplyShouldBeLast(returnStrAry);

            Assert.AreEqual(expected, isValid);

        }


    }
}
