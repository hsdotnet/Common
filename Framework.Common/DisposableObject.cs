using System;

namespace Framework.Common
{
    public abstract class DisposableObject : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected abstract void Dispose(bool disposing);

        #region IDisposable 成员
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 析构函数
        /// </summary>
        ~DisposableObject()
        {
            this.Dispose(false);
        }
    }
}