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
                RaisePropertyChanged("RadioValue");
                return radioValue;
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
            RaisePropertyChanges(false);
            try
            {
                Debug.WriteLine("NEXTボタン" + i);
                if(i < exam.Ansers.Count)
                {
                    // 既に設定されている回答にラジオボタンをセットする。
                    RadioValue = exam.Ansers[i];
                }else
                {
                    RadioValue = AbcdEnum.A;
                }
                RaisePropertyChanged("BackCommand");
            }
            catch(Exception e)
            {
                
                Debug.WriteLine("NEXTボタンで例外発生");
            }

            exam.DebugMessage();
            RaisePropertyChanged("RadioValue");
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
            RaisePropertyChanges(true);


            try
            {
                Debug.WriteLine("BACKボタン" + i);
                var tmp = exam.Ansers[i];
                RaisePropertyChanged("NextCommand");
                RadioValue = tmp;
                Debug.WriteLine(RadioValue);
            }
            catch (Exception e)
            {
                Debug.WriteLine("BACKボタンで例外発生");
            }
            exam.DebugMessage();
            RaisePropertyChanged("RadioValue");
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
            RaisePropertyChanged("RadioValue");
            //回答内容を保存
            if (exam.Ansers.Count < this.QuestionNo)
            {
                exam.Ansers.Add((AbcdEnum)parameter);
            }else
            {
                exam.Ansers[i] = (AbcdEnum)parameter;
            }


            //RaisePropertyChanged("RadioValue");

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

        /// <summary>
        /// 画面更新処理
        /// </summary>
        /// ・問題番号
        /// ・問題(問題文、回答候補)
        /// ・戻るボタン
        /// ・次へボタン
        /// ・採点ボタン
        /// <param name="isBack"></param>
        private void RaisePropertyChanges(bool isBack)
        {
            RaisePropertyChanged("QuestionNo");
            RaisePropertyChanged("Question");
            if (isBack)
            {
                RaisePropertyChanged("BackCommand");
            }else
            {
                RaisePropertyChanged("NextCommand");
            }
            RaisePropertyChanged("GradingCommand");
        }
    }
}
