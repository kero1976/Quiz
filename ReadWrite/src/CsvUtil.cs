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

        /// <summary>
        /// 1行のデータを列番号(0,1,...)と値のコレクションにして、それをListに格納して返す。
        /// </summary>
        /// <param name="allText">複数行のデータ</param>
        /// <returns>1行のデータを列番号(0,1,...)と値のコレクションにして、それをListに格納</returns>

        public static List<Dictionary<string,string>> analyze(string allText)
        {
            var parseResult = new List<Dictionary<string, string>>();

            // 複数行のデータを改行で分割し、行数分の配列に格納
            string[] records = allText.Split(DEFAULT_LINE_DELIMITER, StringSplitOptions.None);
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
