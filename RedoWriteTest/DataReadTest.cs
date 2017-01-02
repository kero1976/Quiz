using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Quiz.ReadWrite;
using Quiz.Core;
using System.Collections.Generic;


namespace RedoWriteTest
{
    [TestClass]
    public class DataReadTest
    {
        [TestMethod]
        public void データ読み込み()
        {
            var questions = DataRead.ReadQuestion("test.txt");
            Assert.AreEqual("1+1=", questions[0].Sentence);
        }

        [TestMethod]
        public void ERR1002テスト()
        {
            var e = Assert.ThrowsException<UserException>(() => DataRead.ReadQuestion("testERR1002.txt"));
            Assert.AreEqual("ファイル(testERR1002.txt)の3行目の書式が不正です。0=>abc", e.ToString());

        }
    }
}
