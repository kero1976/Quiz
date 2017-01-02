using Quiz.Core;
using Quiz.Core.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ReadWrite
{
    public class DataRead
    {
        public static List<Question> ReadQuestion(string file)
        {
            List<Question> result = new List<Question>();
            // ファイルを読み込み、全文を取得する。
            string allString = File.ReadAllString(file);

            // CSV形式に分解
            var rows = CsvUtil.analyze(allString);

            // データ行数
            int rowNo = 1;

            // 問題オブジェクトに格納
            foreach(var row in rows)
            {
                Question question;
                try
                {
                    // 回答と回答候補。この後ランダムに並び替えてセットする。
                    var candidate = new List<string>()
                        {
                        row["1"],
                        row["2"],
                        row["3"],
                        row["4"],
                        };
                    question = new Question(
                        row["0"],
                        candidate.OrderBy(i => Guid.NewGuid()).ToList(),
                        row["1"]);
                    result.Add(question);
                    rowNo++;
                }
                catch(Exception e)
                {
                    string data = String.Join(", ", from v in row select v.Key + "=>" + v.Value);
                    throw new UserException("ERR1002", new string[] { file, rowNo.ToString(), data }, e);
                }
            }
            return result;
        }
    }
}
