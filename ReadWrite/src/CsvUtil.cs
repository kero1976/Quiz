using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ReadWrite
{
    public class CsvUtil
    {
        private const char DEFAULT_DELIMITER = ',';
        private static string[] DEFAULT_LINE_DELIMITER = new string[] { "\r\n" };

        public static List<Dictionary<string,string>> analyze(string allText)
        {
            var parseResult = new List<Dictionary<string, string>>();
            var records = allText.Split(DEFAULT_LINE_DELIMITER, StringSplitOptions.None);
            foreach(var record in records)
            {
                var fields = record.Split(DEFAULT_DELIMITER);
                var recordItem = new Dictionary<string, string>();
                var i = 0;
                foreach(var field in fields)
                {
                    recordItem.Add(i.ToString(), field);
                    i++;
                }
                parseResult.Add(recordItem);
            }
            return parseResult;
        }
    }
}
