using System;
using System.Diagnostics;
using Windows.ApplicationModel.Resources;
using System.Runtime.CompilerServices;
using System.Text;

namespace Quiz.Core
{
    /// <summary>
    /// カスタム例外クラス。
    /// </summary>
    public class UserException:Exception
    {
        /// <summary>
        /// リソースファイルからエラーメッセージ読み込み用。
        /// </summary>
        static ResourceLoader resourceLoader = ResourceLoader.GetForViewIndependentUse("Core/Resources");

        /// <summary>
        /// エラーが発生したソースファイル名の格納用。
        /// </summary>
        /// CallerMemberNameAttributeクラスを使用して取得する。
        string filePath;

        /// <summary>
        /// エラーが発生したメソッド名の格納用。
        /// </summary>
        /// CallerMemberNameAttributeクラスを使用して取得する。
        string methodName;

        /// <summary>
        /// エラーが発生したソースファイルの行数の格納用。
        /// </summary>
        /// CallerMemberNameAttributeクラスを使用して取得する。
        int sourceLineNumber;

        /// <summary>
        /// エラーメッセージにセットするパラメータ。
        /// </summary>
        string[] errParams;

        /// <summary>
        /// エラーコード。
        /// </summary>
        /// リソースファイルで管理。「ERR****」の形式で、****は数字4桁。
        string errCode;

        /// <summary>
        /// 内部の例外を格納。
        /// </summary>
        Exception innerException;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// 最初に必要な値をセットしたら、後から変更は不可。
        /// エラーパラメータは配列なので、ない場合と1つの場合は注意すること。
        /// <param name="errCode">エラーコード。</param>
        /// <param name="errParams">パラメータ。</param>
        /// <param name="innerException">内部例外。</param>
        /// <param name="filePath">ソースファイル名。CallerMemberNameAttributeクラスを使用して取得するためセット不要</param>
        /// <param name="methodName">メソッド名。CallerMemberNameAttributeクラスを使用して取得するためセット不要</param>
        /// <param name="sourceLineNumber">行数。CallerMemberNameAttributeクラスを使用して取得するためセット不要</param>
        public UserException(string errCode, string[] errParams, Exception innerException,[CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            this.filePath = filePath;
            this.methodName = methodName;
            this.errCode = errCode;
            this.sourceLineNumber = sourceLineNumber;
            this.errParams = errParams;
            this.innerException = innerException;
        }

        public UserException(string errCode, string errParam, Exception innerException, [CallerFilePath] string filePath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            this.filePath = filePath;
            this.methodName = methodName;
            this.errCode = errCode;
            this.sourceLineNumber = sourceLineNumber;
            this.errParams = new string[] { errParam };
            this.innerException = innerException;
        }


        public override string ToString()
        {
            string messsage = resourceLoader.GetString(errCode);
            return string.Format(messsage, errParams);
         }


        public string DebugString()
        {
            string time = String.Format("【{0}】",DateTime.Now);
            StringBuilder buff = new StringBuilder().AppendLine();
            buff.Append(time).AppendLine("エラーコード: " + errCode);
            buff.Append(time).AppendLine("メッセージ: " + ToString());
            buff.Append(time).AppendLine("メソッド名: " + methodName);
            buff.Append(time).AppendLine("ファイル名: " + filePath);
            buff.Append(time).AppendLine("ソース行: " + sourceLineNumber);
            buff.Append(time).AppendLine(innerException.ToString());


            return buff.ToString();
        }
    }
}
