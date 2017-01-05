using Quiz.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Diagnostics;
using Core.Converter;

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

        #region Checkdボタン
        //private void CheckCommandExecute(object parameter)
        //{
        //    Debug.WriteLine("チェックされました:" + parameter);
        //}

        //private bool CheckCommandCanExecute(object parameter)
        //{
        //    return true;
        //}


        //private ICommand _checkCommand;
        //public ICommand CheckCommand
        //{
        //    get
        //    {
        //        if (_backCommand == null)
        //            _backCommand = new DelegateCommand
        //            {
        //                ExecuteHandler = CheckCommandExecute,
        //                CanExecuteHandler = CheckCommandCanExecute,
        //            };
        //        return _checkCommand;
        //    }
        //}
        #endregion

        #region Answerボタン
        private void AnswerCommandExecute(object parameter)
        {
            Debug.WriteLine("回答が押されました" + parameter);
            RaisePropertyChanged("RadioValue");
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
    }
}
