using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Quiz.ReadWrite;
using Core;

namespace RedoWriteTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void 正常系の読み込み()
        {
            string actual = File.ReadAllString("test.txt");
            string expected = "test2";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void 指定ファイル無し()
        {
            var e = Assert.ThrowsException<UserException>(() => File.ReadAllString("test2.txt"));
            //Assert.AreEqual("ファイルの読み込みに失敗しました。", e.ToString());
            Assert.Fail(e.DebugString());
        }
    }
}
