using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Quiz.ReadWrite;
using Quiz.Core;
using System.Collections.Generic;

namespace RedoWriteTest
{
    [TestClass]
    public class CsvUtilTest
    {
        [TestMethod]
        public void CSV分割()
        {
            string text = "1+1=,2,1,3,4\r\n2+2=,4,1,2,3";
            var actual = CsvUtil.analyze(text);
            List<Dictionary<string, string>> expected = new List<Dictionary<string, string>>();
            expected.Add(new Dictionary<string, string>()
            {
                {"0","1+1=" },
                {"1","2" },
                {"2","1" },
                {"3","3" },
                {"4","4" },
            });
            expected.Add(new Dictionary<string, string>()
            {
                {"0","2+2=" },
                {"1","4" },
                {"2","1" },
                {"3","2" },
                {"4","3" },
            });
            CollectionAssert.AreEqual(expected[0], actual[0],"0行目で失敗");
            CollectionAssert.AreEqual(expected[1], actual[1],"1行目で失敗");
        }
    }
}
