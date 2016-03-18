using System;

namespace Framework.Common.Logger
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger(string name);

        ILogger CreateLogger(Type type);
    }
}