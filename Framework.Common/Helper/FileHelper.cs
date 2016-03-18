using System.IO;
using System.Text;

namespace Framework.Common.Helper
{
    public sealed class FileHelper
    {
        #region Directory
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ExistsDirectory(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            if (!ExistsDirectory(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        #endregion

        #region File
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ExistsFile(string path)
        {
            return File.Exists(path);
        }

        public static void CreateFile(string path)
        {
            if (ExistsFile(path)) { return; }

            File.Create(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        public static void WriteToFile(string path, string content, Encoding encoding)
        {
            using (StreamWriter sw = new StreamWriter(path, true, encoding))
            {
                sw.WriteLine(content);

                sw.Flush();
            }
        }
        #endregion
    }
}