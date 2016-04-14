using System;
using System.Diagnostics;

namespace Framework.Common.Helper
{
    public sealed class ActionHelper
    {
        public static TimeSpan StopwatchAction(Action action)
        {
            Stopwatch st = new Stopwatch();

            st.Start();

            action();

            st.Stop();

            return st.Elapsed;
        }
    }
}