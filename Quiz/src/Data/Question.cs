using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Data
{
    class Question
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
    }
}
