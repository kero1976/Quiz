using Quiz.Core.Data;
using System;
using System.Collections.Generic;
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
            var datas = CsvUtil.analyze(allString);

            // 問題オブジェクトに格納
            foreach(var data in datas)
            {
                Question question = new Question(
                    data["0"],
                    new List<string>()
                    {
                        data["2"],
                        data["3"],
                        data["4"],
                    },
                    data["1"]);
                result.Add(question);
            }
            return result;
        }
    }
}
