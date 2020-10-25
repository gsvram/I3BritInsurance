using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrInsTestUnits
{
    [TestClass]
    public class MockUnitTest
    {
        [DataRow("add 1\r\nsub 2\r\nmul -3\r\napply 9", new string[] { "add 1", "sub 2", "mul -3", "apply 9" }, 4)]
        [DataTestMethod]
        public void Mock_GetLines_CompareArrayLenght_Ok(string strContent, string[] returnStrAry, int totalArrary)
        {
            Mock<IProcessFile> mock = new Mock<IProcessFile>();
            mock.Setup(_ => _.GetLinesFromFile(strContent)).Returns(returnStrAry);
            Assert.AreEqual(totalArrary, mock.Object.GetLinesFromFile(strContent).Length);
        }

        [DataRow("add 1\r\nsub 2\r\nmul -3\r\napply 9", new string[] { "add 1", "sub 2", "mul -3", "apply 9" }, 4)]
        [DataTestMethod]
        public void Mock_GetLines_CompareArray_Ok(string strContent, string[] returnStrAry, int totalArrary)
        {
            Mock<IProcessFile> mock = new Mock<IProcessFile>();
            mock.Setup(_ => _.GetLinesFromFile(strContent)).Returns(returnStrAry);
            Assert.AreEqual(returnStrAry, mock.Object.GetLinesFromFile(strContent));
        }


        [DataRow("add 1\r\n\r\n \r\nsub 2\r\nmul -3\r\napply 9\r\n", new string[] { "add 1", "sub 2", "mul -3", "apply 9" }, 4)]
        [DataTestMethod]
        public void Mock_GetLines_RemoveEmptyArray_Ok(string strContent, string[] returnStrAry, int totalArrary)
        {
            Mock<IProcessFile> mock = new Mock<IProcessFile>();
            mock.Setup(_ => _.GetLinesFromFile(strContent)).Returns(returnStrAry);
            Assert.AreEqual(returnStrAry, mock.Object.GetLinesFromFile(strContent));
        }

    }
}
