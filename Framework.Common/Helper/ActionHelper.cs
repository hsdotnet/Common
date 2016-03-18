using System;
using System.Diagnostics;

namespace Framework.Common.Helper
{
    public sealed class ActionHelper
    {
        public static TimeSpan StopwatchAction(Action action)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            action();

            watch.Stop();

            return watch.Elapsed;
        }

        public static void EatException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
            }
        }
        public static T EatException<T>(Func<T> action, T defaultValue = default(T))
        {
            try
            {
                return action();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}