namespace Framework.Common.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public enum LifeStyle
    {
        /// <summary>
        ///  Instances are created new every time
        /// </summary>
        Transient,
        /// <summary>
        /// Hold a weak reference to it's managed instance
        /// </summary>
        WeakReferenceRequest,
        /// <summary>
        /// Singleton
        /// </summary>
        Singleton,
        /// <summary>
        /// Keep one instance per thread
        /// </summary>
        InThread
    }
}