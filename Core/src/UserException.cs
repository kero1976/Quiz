using System;
using System.Diagnostics;
using Windows.ApplicationModel.Resources;
using System.Runtime.CompilerServices;
using System.Text;

namespace Quiz.Core
{
    public class UserException:Exception
    {
        static ResourceLoader resourceLoader = ResourceLoader.GetForViewIndependentUse("Core/Resources");


        string filePath;
        string methodName;
        int sourceLineNumber;
        string[] errParams;

        string errCode;
        Exception innerException;
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
