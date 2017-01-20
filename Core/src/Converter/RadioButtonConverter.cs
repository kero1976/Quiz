using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// WPFではWindows.UI.Xaml.Datではなく、System.Windows.Dataになる
using Windows.UI.Xaml.Data;

namespace Core.Converter
{
    public enum AbcdEnum{
        A,B,C,D,
    }

    /// <summary>
    /// ラジオボタン用バリューコンバータ
    /// </summary>
    /// A,B,C,Dのenumに変換する。
    public class RadioButtonConverter : IValueConverter
    {
        private AbcdEnum ConvertFromConverterParameter(object parameter)
        {
            string parameterString = parameter as string;
            return (AbcdEnum)Enum.Parse(typeof(AbcdEnum), parameterString);
        }
        /// <summary>
        /// enum→bool
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // XAMLに定義されたConverterParameterをenumに変換
            AbcdEnum parameterValue = ConvertFromConverterParameter(parameter);

            // ConverterParameterをバインディングソースの値が等しいか？
            return parameterValue.Equals(value);
        }

        /// <summary>
        /// bool→int
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // true→falseの変化は無視する
            if (!(bool)value)
            {
                return null;
            }
            // ConverterParameterをintに変換して返す
            return ConvertFromConverterParameter(parameter);
        }

        /// <summary>
        /// A,B,C,Dを0,1,2,3に変換する
        /// </summary>
        /// <param name="val">A,B,C,D</param>
        /// <returns>0,1,2,3</returns>
        public static int AbcdToInt(AbcdEnum val)
        {
            switch (val){
                case AbcdEnum.A:
                    return 0;
                case AbcdEnum.B:
                    return 1;
                case AbcdEnum.C:
                    return 2;
                case AbcdEnum.D:
                    return 3;
            }
            // ここに来ることはないはず
            return -1;
        }
    }
}
