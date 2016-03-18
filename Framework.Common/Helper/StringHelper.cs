using System;

namespace Framework.Common.Helper
{
    public sealed class StringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetServerPath()
        {
            string path = AppDomain.CurrentDomain.RelativeSearchPath;

            if (string.IsNullOrWhiteSpace(path)) { path = AppDomain.CurrentDomain.BaseDirectory; }

            if (!path.EndsWith("\\")) { path += "\\"; }

            return path;
        }
    }
}