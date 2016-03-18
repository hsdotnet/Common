using System;

namespace Framework.Common.Logger
{
    public class DefaultLoggerFactory : ILoggerFactory
    {
        private readonly DefaultLogger _logger = new DefaultLogger();

        public ILogger CreateLogger(string name)
        {
            return this._logger;
        }

        public ILogger CreateLogger(Type type)
        {
            return this._logger;
        }
    }
}