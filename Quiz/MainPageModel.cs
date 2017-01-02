using Quiz.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public class MainPageModel
    {
        // 試験クラス
        Exam exam;

        // 内部用の問題番号(0スタート)
        int i;

        public MainPageModel()
        {
            exam = new Quiz.Exam("test.txt");
            i = 0;
        }
        public int QuestionNo
        {
            get
            {
                return i + 1;
            }
        }

        public Question Question
        {
            get
            {
                return exam.Questions[i];
            }
        }


        public void Next()
        {
            i++;
        }
    }
}
