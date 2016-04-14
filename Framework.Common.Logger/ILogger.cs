using System;

namespace Framework.Common.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogger
    {
        void Info(string title, string message);

        void Debug(string title, string message);

        void Error(string title, string message);

        void Error(string title, Exception exception);
    }
}