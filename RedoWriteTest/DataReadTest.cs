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
    }
}
