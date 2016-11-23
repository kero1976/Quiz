using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Quiz.ReadWrite
{
    /// <summary>
    /// Fileの読み込み
    /// </summary>
    public class File
    {
        /// <summary>
        /// 指定したファイル名のファイルを読み込み、stringで返す。
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>ファイルのすべての行を格納している文字列</returns>
        public static string ReadAllString(string fileName)
        {
            Uri filePath = null;
            try
            {
                filePath = new Uri("ms-appx:///" + fileName);
            }catch(Exception e)
            {
                
            }

            StorageFile file = null;
            try
            {
                file = read(filePath).Result;
            }
            catch (Exception e)
            {
                throw new UserException("ERR1001",new string[] { fileName },e);
            }

            string result = null;

            try
            {
                result = readtext(file).Result;
            }catch(Exception e)
            {
                
            }
            return result;

        }

        private static async Task<StorageFile> read(Uri filePath)
        {
            return await StorageFile.GetFileFromApplicationUriAsync(filePath); 
        }

        private static async Task<string> readtext(StorageFile file)
        {
            return await FileIO.ReadTextAsync(file);
        }


    }
}
