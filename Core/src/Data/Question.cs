using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Data
{
    /// <summary>
    /// 問題文クラス。
    /// </summary>
    /// 正解かの判定は文字列での比較を行う。
    /// TODO:パフォーマンスに問題が出るようなら、IDでの比較ができるように変更する。
    public class Question
    {
        /// <summary>
        /// 問題文
        /// </summary>
        private string sentence;

        /// <summary>
        /// 回答候補
        /// </summary>
        private List<string> candidate;

        /// <summary>
        /// 正解
        /// </summary>
        private string correct;

        /// <summary>
        /// コンストラクタ。最初にセットし、後からの変更は不可。
        /// </summary>
        /// <param name="sentence">問題文。</param>
        /// <param name="candidate">回答候補のリスト。</param>
        /// <param name="correct">正解。</param>
        public Question(string sentence, List<string> candidate, string correct)
        {
            this.sentence = sentence;
            this.candidate = candidate;
            this.correct = correct;
        }

        /// <summary>
        /// デバッグ用。
        /// </summary>
        /// TODO:後で無効にする。
        /// <returns></returns>
        public string DebugString()
        {
            StringBuilder buff = new StringBuilder();
            buff.AppendLine("問題：" + sentence);
            int i = 1;
            foreach(var answer in candidate)
            {
                buff.AppendLine(i + "：" + answer);
                i++;
            }
            buff.AppendLine("正解：" + correct);
            return buff.ToString();
        }

        /// <summary>
        /// 問題文。
        /// </summary>
        public string Sentence
        {
            get
            {
                return sentence;
            }
        }

        /// <summary>
        /// 回答候補。
        /// </summary>
        public List<string> Candidate
        {
            get
            {
                return candidate;
            }
        }

        /// <summary>
        /// 判定。
        /// </summary>
        /// <param name="answer">回答</param>
        /// <returns>true:正解, false:不正解</returns>
        public bool Decision(string answer)
        {
            return answer.Equals(this.correct);
        }
    }
}
