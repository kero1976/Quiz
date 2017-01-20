using Core.Converter;
using Quiz.Core.Data;
using Quiz.ReadWrite;
using System.Collections.Generic;
using System.Diagnostics;

namespace Quiz
{
    /// <summary>
    /// 試験クラス。
    /// </summary>
    /// 問題を読み込み、回答結果を保持し、採点する。
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
        /// 回答群
        /// </summary>
        private List<AbcdEnum> answers = new List<AbcdEnum>();

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

        /// <summary>
        /// 問題群
        /// </summary>
        public List<Question> Questions
        {
            get
            {
                return questions;
            }
        }

        /// <summary>
        /// 回答群
        /// </summary>
        public List<AbcdEnum> Ansers
        {
            set
            {
                answers = value;
            }
            get
            {
                return answers;
            }
        }

        /// <summary>
        /// 採点
        /// </summary>
        public void Check()
        {
            score = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                int index = RadioButtonConverter.AbcdToInt(answers[i]);
                if (questions[i].Decision(questions[i].Candidate[index]))
                {
                    score++;
                }
            }
        }

        /// <summary>
        /// テスト結果
        /// </summary>
        public int Score
        {
            get
            {
                return score;
            }
        }

        public void DebugMessage()
        {
            for (int i = 0; i < Ansers.Count; i++)
            {
                string val = string.Format("[{0}]:{1}, ", i, Ansers[i]);
                Debug.Write(val);
            }
            Debug.WriteLine("");
        }
    }
}
