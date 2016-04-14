using System;

namespace Framework.Common.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EnsureHelper
    {
        /// <summary>
        /// 对象为空不通过
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        public static void NotNull<T>(T argument, string argumentName) where T : class
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName + "不能为空");
        }

        /// <summary>
        /// 参数为空不通过
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        public static void NotNullOrEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
                throw new ArgumentNullException(argument, argumentName + "不能为空");
        }

        /// <summary>
        /// 参数小于等于0不通过
        /// </summary>
        /// <param name="number"></param>
        /// <param name="argumentName"></param>
        public static void Positive(long number, string argumentName)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should be positive.");
        }

        /// <summary>
        /// 参数小于0不通过
        /// </summary>
        /// <param name="number"></param>
        /// <param name="argumentName"></param>
        public static void Nonnegative(long number, string argumentName)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should be non negative.");
        }

        /// <summary>
        /// Guid为空不通过
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="argumentName"></param>
        public static void NotEmptyGuid(Guid guid, string argumentName)
        {
            if (Guid.Empty == guid)
                throw new ArgumentException(argumentName, argumentName + " shoud be non-empty GUID.");
        }

        /// <summary>
        /// 两个数字不相等不通过
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="argumentName"></param>
        public static void Equal(long expected, long actual, string argumentName)
        {
            if (expected != actual)
                throw new ArgumentException(string.Format("{0} expected value: {1}, actual value: {2}", argumentName, expected, actual));
        }

        /// <summary>
        /// 两个bool类型参数不相等不通过
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="argumentName"></param>
        public static void Equal(bool expected, bool actual, string argumentName)
        {
            if (expected != actual)
                throw new ArgumentException(string.Format("{0} expected value: {1}, actual value: {2}", argumentName, expected, actual));
        }
    }
}