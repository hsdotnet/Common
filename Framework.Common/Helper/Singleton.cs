/**********************************************************************************
 * 
 * 单例模式：确保一个类只有一个实例，并提供一个全局访问点来访问这个唯一实例。
 * 
 * **********************************************************************************/

namespace Framework.Common.Helper
{
    /// <summary>
    /// 饿汉模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static readonly T instance = new T();

        private Singleton()
        {
        }

        public static T GetInstance()
        {
            return instance;
        }
    }
}