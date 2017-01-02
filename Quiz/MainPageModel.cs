using Quiz.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace Quiz
{
    public class MainPageModel : INotifyPropertyChanged
    {
        // 試験クラス
        Exam exam;

        // 内部用の問題番号(0スタート)
        int i;

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
        private bool NextCommandCanExecute(object parameter)
        {
            if(this.i < (this.exam.Questions.Count - 1))
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
        private bool BackCommandCanExecute(object parameter)
        {
            if (this.i > 0)
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
    }
}
