using Quiz.Core.Data;
using Quiz.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    /// <summary>
    /// 試験クラス。
    /// </summary>
    public class Exam
    {
        /// <summary>
        /// 点数
        /// </summary>
        private int score;

        /// <summary>
        /// 問題群
        /// </summary>
        private List<Question> questions;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// 問題文を読み込み、点数を初期化する。
        /// <param name="file">問題文</param>
        public Exam(string file)
        {
            this.score = 0;
            this.questions = DataRead.ReadQuestion(file);
        }

        public List<Question> Questions
        {
            get
            {
                return questions;
            }
        }
    }
}
