using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Data
{
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

        public Question(string sentence, List<string> candidate, string correct)
        {
            this.sentence = sentence;
            this.candidate = candidate;
            this.correct = correct;
        }

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

        public string Sentence
        {
            get
            {
                return sentence;
            }
        }
    }
}
