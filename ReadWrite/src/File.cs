using Quiz.Core;
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
        /// インストールフォルダ内にある指定したファイル名のファイルを読み込み、stringで返す。
        /// </summary>
        /// <param name="fileName">インストールフォルダ内にあるファイルの名前</param>
        /// <returns>ファイルのすべての行を格納している文字列</returns>
        public static string ReadAllString(string fileName)
        {
            try
            {
                // 指定したファイル名のStorageFileを取得
                var file = read(new Uri("ms-appx:///" + fileName)).Result;

                // ファイルの内容を読み取り、テキストを返す
                return readtext(file).Result;
            }catch(Exception e)
            {
                throw new UserException("ERR1001", new string[] { fileName }, e);
            }
        }

        /// <summary>
        /// インストールフォルダ内にある指定したファイル名のファイルを読み込み、stringで返す。
        /// </summary>
        /// <param name="fileName">インストールフォルダ内にあるファイルの名前</param>
        /// <returns>ファイルのすべての行を格納している文字列</returns>
        public static List<string> ReadAllLine(string fileName)
        {
            try
            {
                // 指定したファイル名のStorageFileを取得
                var file = read(new Uri("ms-appx:///" + fileName)).Result;

                // ファイルの内容を読み取り、テキストを返す
                return readAllLine(file).Result.ToList();
            }
            catch (Exception e)
            {
                throw new UserException("ERR1001", new string[] { fileName }, e);
            }
        }

        private static async Task<StorageFile> read(Uri filePath)
        {
            return await StorageFile.GetFileFromApplicationUriAsync(filePath); 
        }

        private static async Task<string> readtext(StorageFile file)
        {
            return await FileIO.ReadTextAsync(file);
        }

        private static async Task<IList<string>> readAllLine(StorageFile file)
        {
            return await FileIO.ReadLinesAsync(file);
        }

    }
}
