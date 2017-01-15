using Core.Converter;
using Quiz.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Windows.UI.Popups;

namespace Quiz
{
    public class MainPageModel : INotifyPropertyChanged
    {
        // 試験クラス
        Exam exam;

        // 内部用の問題番号(0スタート)
        int i;

        // RadioButtonの選択保存用
        private AbcdEnum radioValue;

        public AbcdEnum RadioValue
        {
            set
            {
                radioValue = value;
                RaisePropertyChanged("RadioValue");
            }
            get
            {
                return radioValue;
            }
        }

        private List<string> answerList;

        public string Answer
        {
            get
            {
                return answerList[i];
            }
            set
            {
                answerList[i] = value;
            }
        }
        /// <summary>
        /// 問題数
        /// </summary>
        public int QuestionCount
        {
            get
            {
                return exam.Questions.Count;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var d = PropertyChanged;
            if(d != null)
            {
                d(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public MainPageModel()
        {
            exam = new Quiz.Exam("test.txt");
            i = 0;
            answerList = new List<string>();
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
        #region Nextボタン

        private void NextCommandExecute(object parameter)
        {
            i++;
            RaisePropertyChanged("QuestionNo");
            RaisePropertyChanged("Question");
            RaisePropertyChanged("BackCommand");
            RaisePropertyChanged("NextCommand");
            RaisePropertyChanged("GradingCommand");
        }

        /// <summary>
        /// 次へボタンの有効判定
        /// </summary>
        /// 現在の問題番号(QuestionNo)が総件数(QuestionCount)より小さい場合は有効
        /// <param name="parameter">未使用</param>
        /// <returns>true:ボタン有効, false:ボタン無効</returns>
        private bool NextCommandCanExecute(object parameter)
        {
            if(this.QuestionNo < (this.QuestionCount))
            {
                return true;
            }
            return false;
        }


        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (_nextCommand == null)
                    _nextCommand = new DelegateCommand
                    {
                        ExecuteHandler = NextCommandExecute,
                        CanExecuteHandler = NextCommandCanExecute,
                    };
                return _nextCommand;
            }
        }

        #endregion

        #region Backボタン
        private void BackCommandExecute(object parameter)
        {
            i--;
            RaisePropertyChanged("QuestionNo");
            RaisePropertyChanged("Question");
            RaisePropertyChanged("BackCommand");
            RaisePropertyChanged("NextCommand");
            RaisePropertyChanged("GradingCommand");
        }
        /// <summary>
        /// 前へボタンの有効判定
        /// </summary>
        /// 現在の問題番号(QuestionNo)が1より大きい場合は有効
        /// <param name="parameter">未使用</param>
        /// <returns>true:ボタン有効, false:ボタン無効</returns>
        private bool BackCommandCanExecute(object parameter)
        {
            if (1 < this.QuestionNo)
            {
                return true;
            }
            return false;
        }


        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                    _backCommand = new DelegateCommand
                    {
                        ExecuteHandler = BackCommandExecute,
                        CanExecuteHandler = BackCommandCanExecute,
                    };
                return _backCommand;
            }
        }
        #endregion

        #region Answerボタン
        private void AnswerCommandExecute(object parameter)
        {
            int index = -1;
            string answer = parameter.ToString();
            switch (answer) {
                case "A":
                    index = 0;
                    break;
                case "B":
                    index = 1;
                    break;
                case "C":
                    index = 2;
                    break;
                case "D":
                    index = 3;
                    break;
            }

            //回答内容を保存
            if (exam.Ansers.Count < this.QuestionNo)
            {
                exam.Ansers.Add(Question.Candidate[index]);
            }else
            {
                exam.Ansers[i] = Question.Candidate[index];
            }


            RaisePropertyChanged("RadioValue");

            if (this.QuestionNo == this.QuestionCount)
            {
                GradingCommandExecute(null);
            }
            else
            {
                NextCommandExecute(null);
            }


        }
        /// <summary>
        /// 前へボタンの有効判定
        /// </summary>
        /// 現在の問題番号(QuestionNo)が1より大きい場合は有効
        /// <param name="parameter">未使用</param>
        /// <returns>true:ボタン有効, false:ボタン無効</returns>
        private bool AnswerCommandCanExecute(object parameter)
        {
            return true;
        }


        private ICommand _answerCommand;
        public ICommand AnswerCommand
        {
            get
            {
                if (_answerCommand == null)
                    _answerCommand = new DelegateCommand
                    {
                        ExecuteHandler = AnswerCommandExecute,
                        CanExecuteHandler = AnswerCommandCanExecute,
                    };
                return _answerCommand;
            }
        }
        #endregion



        #region 採点ボタン
        private async void GradingCommandExecute(object parameter)
        {
            exam.Check();
            var dlg = new MessageDialog("点数は" + exam.Score, "採点結果");
            await dlg.ShowAsync();
        }
        /// <summary>
        /// 採点ボタンの有効判定
        /// </summary>
        /// 現在の問題番号(QuestionNo)が最後の問題の場合は有効
        /// <param name="parameter">未使用</param>
        /// <returns>true:ボタン有効, false:ボタン無効</returns>
        private bool GradingCommandCanExecute(object parameter)
        {
            if (this.QuestionNo == (this.QuestionCount))
            {
                return true;
            }
            return false;
        }


        private ICommand _gradingCommand;
        public ICommand GradingCommand
        {
            get
            {
                if (_gradingCommand == null)
                    _gradingCommand = new DelegateCommand
                    {
                        ExecuteHandler = GradingCommandExecute,
                        CanExecuteHandler = GradingCommandCanExecute,
                    };
                return _gradingCommand;
            }
        }
        #endregion
    }
}
