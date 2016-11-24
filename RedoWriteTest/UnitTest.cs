using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Quiz.Core;
using Quiz.ReadWrite;
using System.Collections.Generic;

namespace RedoWriteTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void 正常系の読み込みstring()
        {
            string actual = File.ReadAllString("test.txt");
            string expected = "1+1=,2,1,3,4\r\n2+2=,4,1,2,3";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void 正常系の読み込みLine()
        {
            try
            {
                List<string> actual = File.ReadAllLine("test.txt");
                List<string> expected = new List<string>();
                expected.Add("1+1=,2,1,3,4");
                expected.Add("2+2=,4,1,2,3");

                CollectionAssert.AreEqual(expected, actual);

            }catch(UserException e)
            {
                Assert.Fail(e.DebugString());
            }

        }

        [TestMethod]
        public void 指定ファイル無し()
        {
            var e = Assert.ThrowsException<UserException>(() => File.ReadAllString("test2.txt"));
           Assert.AreEqual("ファイル(test2.txt)の読み込みに失敗しました。", e.ToString());
            
        }
    }
}
