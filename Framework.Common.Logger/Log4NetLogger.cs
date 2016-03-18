using System;

using log4net;

namespace Framework.Common.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public Log4NetLogger(ILog log)
        {
            _log = log;
        }

        public void Info(string title, string message)
        {
            this._log.InfoFormat("【标题】:{0} 【内容】:{1}", title, message);
        }

        public void Debug(string title, string message)
        {
            this._log.DebugFormat("【标题】:{0} 【内容】:{1}", title, message);
        }

        public void Error(string title, string message)
        {
            this._log.ErrorFormat("【标题】:{0} 【内容】:{1}", title, message);
        }

        public void Error(string title, Exception exception)
        {
            this._log.Error(title, exception);
        }
    }
}