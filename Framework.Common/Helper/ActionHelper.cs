using System;
using System.Diagnostics;

namespace Framework.Common.Helper
{
    public class ActionHelper
    {
        public static void EatException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
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

        public static TimeSpan StopwatchAction(Action action)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            action();

            sw.Stop();

            return sw.Elapsed;
        }
    }
}